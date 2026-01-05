import { useQuery } from '@tanstack/react-query';
import { notificationsApi } from '../api/notificationsApi';
import { notificationsQueryKeys } from './notificationsQueryKeys';
import { useAuth } from '@features/auth/providers/useAuth';
import type { GetNotificationsResponse } from '../types/notification.types';

export const useNotificationsQuery = () => {
  const { isAuthenticated } = useAuth();

  return useQuery<GetNotificationsResponse, Error>({
    queryKey: notificationsQueryKeys.list(),
    queryFn: notificationsApi.getNotifications,
    enabled: isAuthenticated,
    staleTime: 2 * 60 * 1000, // 2 minutes
  });
};
