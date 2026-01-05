import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { UnsubscribeFromBlogRequest, UnsubscribeFromBlogResponse } from '@shared/types/subscription.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useUnsubscribeFromBlogMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<UnsubscribeFromBlogResponse, Error, UnsubscribeFromBlogRequest>({
    mutationFn: blogApi.unsubscribe,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.subscriptions() });
    },
  });
};
