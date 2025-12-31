import { createContext } from 'react';
import type { HubConnection } from '@microsoft/signalr';
import type { NotificationSavedMessage } from '../types/notification.types';

export type NotificationConnectionStatus =
  | 'idle'
  | 'connecting'
  | 'connected'
  | 'reconnecting'
  | 'disconnected'
  | 'error';

export type NotificationsContextValue = {
  connection: HubConnection | null;
  status: NotificationConnectionStatus;
  notifications: NotificationSavedMessage[];
  lastNotification: NotificationSavedMessage | null;
  error: Error | null;
};

export const NotificationsContext = createContext<NotificationsContextValue | undefined>(undefined);
