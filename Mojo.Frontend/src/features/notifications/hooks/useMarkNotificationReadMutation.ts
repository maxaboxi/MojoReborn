import { useMutation, useQueryClient } from '@tanstack/react-query';
import { notificationsApi } from '../api/notificationsApi';
import { notificationsQueryKeys } from './notificationsQueryKeys';
import type { MarkNotificationReadRequest } from '../types/notification.types';

export const useMarkNotificationReadMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<void, Error, MarkNotificationReadRequest>({
    mutationFn: notificationsApi.markAsRead,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: notificationsQueryKeys.list() });
    },
  });
};
