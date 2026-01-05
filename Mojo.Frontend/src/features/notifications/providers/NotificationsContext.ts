import { createContext } from 'react';
import type { HubConnection } from '@microsoft/signalr';
import type { NotificationDto } from '../types/notification.types';

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
  notifications: NotificationDto[];
  unreadCount: number;
  isLoading: boolean;
  error: Error | null;
  markAsRead: (notificationId: string) => void;
  refetch: () => void;
};

export const NotificationsContext = createContext<NotificationsContextValue | undefined>(undefined);
