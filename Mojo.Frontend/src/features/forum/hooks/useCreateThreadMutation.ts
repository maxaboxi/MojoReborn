import { useMutation, useQueryClient } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { CreateThreadRequest, CreateThreadResponse } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

export const useCreateThreadMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CreateThreadResponse, Error, CreateThreadRequest>({
    mutationFn: forumApi.createThread,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: forumQueryKeys.all });
    },
  });
};
