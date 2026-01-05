// Real-time notification from SignalR
export type NotificationSavedMessage = {
  userId: string;
  message: string;
  targetUrl: string;
  featureName: string;
};

// Persisted notification from API
export type NotificationDto = {
  notificationId: string;
  message: string;
  url: string;
  featureName: string;
  isRead: boolean;
  createdAt: string;
  entityGuid: string | null;
  entityId: number | null;
};

export type GetNotificationsResponse = {
  notifications: NotificationDto[];
};

export type MarkNotificationReadRequest = {
  notificationId: string;
};
