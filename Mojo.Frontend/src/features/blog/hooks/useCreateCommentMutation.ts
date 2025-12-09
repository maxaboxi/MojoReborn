import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CreateCommentRequest, CreateCommentResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useCreateCommentMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CreateCommentResponse, Error, CreateCommentRequest>({
    mutationFn: blogApi.createComment,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({
        queryKey: blogQueryKeys.post(variables.blogPostId, variables.pageId),
      });
    },
  });
};
