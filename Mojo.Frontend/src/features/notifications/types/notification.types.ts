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
  isRead: boolean;
  createdAt: string;
};

export type GetNotificationsResponse = {
  notifications: NotificationDto[];
};

export type MarkNotificationReadRequest = {
  notificationId: string;
};
