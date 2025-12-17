import { useMutation, useQueryClient } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { EditThreadRequest, EditThreadResponse } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

export const useEditThreadMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<EditThreadResponse, Error, EditThreadRequest>({
    mutationFn: forumApi.editThread,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: forumQueryKeys.all });
    },
  });
};
