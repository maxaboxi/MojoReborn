import apiClient from '@shared/api/axiosClient';
import type {
  GetNotificationsResponse,
  MarkNotificationReadRequest,
} from '../types/notification.types';

export const notificationsApi = {
  getNotifications: async (): Promise<GetNotificationsResponse> => {
    const response = await apiClient.get<GetNotificationsResponse>('/notifications');
    return response.data;
  },
  markAsRead: async (request: MarkNotificationReadRequest): Promise<void> => {
    await apiClient.post('/notifications', request);
  },
};
