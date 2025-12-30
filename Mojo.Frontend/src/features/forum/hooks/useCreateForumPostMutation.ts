import { useMutation, useQueryClient } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { CreateForumPostRequest, CreateForumPostResponse } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

export const useCreateForumPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CreateForumPostResponse, Error, CreateForumPostRequest>({
    mutationFn: forumApi.createPost,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: forumQueryKeys.all });
    },
  });
};
