import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { DeleteCommentRequest, DeleteCommentResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useDeleteCommentMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<DeleteCommentResponse, Error, DeleteCommentRequest>({
    mutationFn: blogApi.deleteComment,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({
        queryKey: blogQueryKeys.post(variables.blogPostId, variables.pageId),
      });
    },
  });
};
