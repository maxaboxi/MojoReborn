import { useMutation, useQueryClient } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { DeleteThreadRequest, DeleteThreadResponse } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

export const useDeleteThreadMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<DeleteThreadResponse, Error, DeleteThreadRequest>({
    mutationFn: forumApi.deleteThread,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: forumQueryKeys.all });
    },
  });
};
