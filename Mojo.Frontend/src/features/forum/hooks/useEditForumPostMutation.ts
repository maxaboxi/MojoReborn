import { useMutation, useQueryClient } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { EditForumPostRequest, EditForumPostResponse } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

export const useEditForumPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<EditForumPostResponse, Error, EditForumPostRequest>({
    mutationFn: forumApi.editPost,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: forumQueryKeys.all });
    },
  });
};
