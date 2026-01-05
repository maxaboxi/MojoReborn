import { useEffect, useMemo, useRef, useState, useCallback, type ReactNode } from 'react';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { useQueryClient } from '@tanstack/react-query';
import { useAuth } from '@features/auth/providers/useAuth';
import { buildHubUrl } from '@shared/config/env';
import { useNotificationsQuery } from '../hooks/useNotificationsQuery';
import { useMarkNotificationReadMutation } from '../hooks/useMarkNotificationReadMutation';
import { notificationsQueryKeys } from '../hooks/notificationsQueryKeys';
import { NotificationsContext, type NotificationsContextValue } from './NotificationsContext';

const NOTIFICATIONS_HUB_PATH = '/hubs/notifications';
const NOTIFICATION_EVENT = 'Notification';
const RECONNECT_DELAYS_MS = [0, 2000, 10_000, 30_000];
const notificationsHubUrl = buildHubUrl(NOTIFICATIONS_HUB_PATH);

export const NotificationsProvider = ({ children }: { children: ReactNode }) => {
  const { isAuthenticated } = useAuth();
  const queryClient = useQueryClient();
  const [connection, setConnection] = useState<HubConnection | null>(null);
  const [status, setStatus] = useState<'idle' | 'connecting' | 'connected' | 'reconnecting' | 'disconnected' | 'error'>(
    'idle'
  );
  const [connectionError, setConnectionError] = useState<Error | null>(null);
  const connectionRef = useRef<HubConnection | null>(null);

  const { data, isLoading, error: queryError, refetch } = useNotificationsQuery();
  const markReadMutation = useMarkNotificationReadMutation();

  const notifications = useMemo(() => data?.notifications ?? [], [data?.notifications]);
  const unreadCount = useMemo(() => notifications.filter((n) => !n.isRead).length, [notifications]);

  const markAsRead = useCallback(
    (notificationId: string) => {
      const notification = notifications.find((n) => n.notificationId === notificationId);
      if (notification && !notification.isRead) {
        markReadMutation.mutate({ notificationId });
      }
    },
    [notifications, markReadMutation]
  );

  const handleRefetch = useCallback(() => {
    void refetch();
  }, [refetch]);

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
      setConnectionError(null);
      setStatus('idle');
      void stopConnection();
      return () => {
        isActive = false;
      };
    }

    const startConnection = async () => {
      setStatus('connecting');
      setConnectionError(null);

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

      newConnection.onreconnected(() => {
        if (isActive) {
          setStatus('connected');
          // Refetch notifications after reconnection
          void queryClient.invalidateQueries({ queryKey: notificationsQueryKeys.list() });
        }
      });

      newConnection.on(NOTIFICATION_EVENT, () => {
        if (!isActive) {
          return;
        }
        // Refetch notifications when a new one arrives via SignalR
        void queryClient.invalidateQueries({ queryKey: notificationsQueryKeys.list() });
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
        setConnectionError(startError as Error);
        setStatus('error');
      }
    };

    void startConnection();

    return () => {
      isActive = false;
      void stopConnection();
    };
  }, [isAuthenticated, queryClient]);

  const value = useMemo<NotificationsContextValue>(
    () => ({
      connection,
      status,
      notifications,
      unreadCount,
      isLoading,
      error: connectionError ?? queryError ?? null,
      markAsRead,
      refetch: handleRefetch,
    }),
    [connection, status, notifications, unreadCount, isLoading, connectionError, queryError, markAsRead, handleRefetch]
  );

  return <NotificationsContext.Provider value={value}>{children}</NotificationsContext.Provider>;
};
