import { useEffect, useMemo, useRef, useState, type ReactNode } from 'react';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { useAuth } from '@features/auth/providers/useAuth';
import { buildHubUrl } from '@shared/config/env';
import type { NotificationSavedMessage } from '../types/notification.types';
import { NotificationsContext, type NotificationsContextValue } from './NotificationsContext';

const NOTIFICATIONS_HUB_PATH = '/hubs/notifications';
const NOTIFICATION_EVENT = 'Notification';
const RECONNECT_DELAYS_MS = [0, 2000, 10_000, 30_000];
const NOTIFICATION_BUFFER_LIMIT = 25;
const notificationsHubUrl = buildHubUrl(NOTIFICATIONS_HUB_PATH);

export const NotificationsProvider = ({ children }: { children: ReactNode }) => {
  const { isAuthenticated, user } = useAuth();
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [status, setStatus] = useState<'idle' | 'connecting' | 'connected' | 'reconnecting' | 'disconnected' | 'error'>(
    'idle'
  );
  const [notifications, setNotifications] = useState<NotificationSavedMessage[]>([]);
  const [lastNotification, setLastNotification] = useState<NotificationSavedMessage | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const connectionRef = useRef<HubConnection | null>(null);
  const userIdRef = useRef<string | null>(null);

  useEffect(() => {
    userIdRef.current = user?.id ?? null;
  }, [user?.id]);

  useEffect(() => {
    let isActive = true;

    const stopConnection = async () => {
      const currentConnection = connectionRef.current;
      if (!currentConnection) {
        if (isActive) {
          setConnection(null);
          setStatus('idle');
        }
        return;
      }

      connectionRef.current = null;
      try {
        await currentConnection.stop();
      } catch (stopError) {
        console.error('Failed to stop notifications hub', stopError);
      } finally {
        if (isActive) {
          setConnection(null);
          setStatus('idle');
        }
      }
    };

    if (!isAuthenticated) {
      setNotifications([]);
      setLastNotification(null);
      setError(null);
      setStatus('idle');
      void stopConnection();
      return () => {
        isActive = false;
      };
    }

    const startConnection = async () => {
      setStatus('connecting');
      setError(null);

      const newConnection = new HubConnectionBuilder()
        .withUrl(notificationsHubUrl, { withCredentials: true })
        .withAutomaticReconnect(RECONNECT_DELAYS_MS)
        .configureLogging(LogLevel.Information)
        .build();

      connectionRef.current = newConnection;

      newConnection.onclose(() => {
        if (isActive) {
          setStatus('disconnected');
        }
      });

      newConnection.onreconnecting(() => {
        if (isActive) {
          setStatus('reconnecting');
        }
      });

      const applyNotification = (payload: NotificationSavedMessage) => {
        if (userIdRef.current && payload.userId !== userIdRef.current) {
          return;
        }

        setLastNotification(payload);
        setNotifications((prev) => {
          const next = [...prev, payload];
          if (next.length > NOTIFICATION_BUFFER_LIMIT) {
            next.splice(0, next.length - NOTIFICATION_BUFFER_LIMIT);
          }
          return next;
        });
      };

      newConnection.onreconnected(() => {
        if (isActive) {
          setStatus('connected');
        }
      });

      newConnection.on(NOTIFICATION_EVENT, (payload: NotificationSavedMessage) => {
        if (!isActive) {
          return;
        }

        applyNotification(payload);
      });

      try {
        await newConnection.start();
        if (!isActive) {
          await newConnection.stop();
          return;
        }
        setConnection(newConnection);
        setStatus('connected');
      } catch (startError) {
        if (!isActive) {
          return;
        }
        console.error('Failed to connect to notifications hub', startError);
        setError(startError as Error);
        setStatus('error');
      }
    };

    void startConnection();

    return () => {
      isActive = false;
      void stopConnection();
    };
  }, [isAuthenticated]);

  const value = useMemo<NotificationsContextValue>(
    () => ({ connection, status, notifications, lastNotification, error }),
    [connection, error, lastNotification, notifications, status]
  );

  return <NotificationsContext.Provider value={value}>{children}</NotificationsContext.Provider>;
};
