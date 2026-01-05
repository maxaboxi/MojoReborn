import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { SubscribeToBlogRequest, SubscribeToBlogResponse } from '@shared/types/subscription.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useSubscribeToBlogMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<SubscribeToBlogResponse, Error, SubscribeToBlogRequest>({
    mutationFn: blogApi.subscribe,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.subscriptions() });
    },
  });
};
