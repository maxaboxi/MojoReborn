import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { EditCommentRequest, EditCommentResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useEditCommentMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<EditCommentResponse, Error, EditCommentRequest>({
    mutationFn: blogApi.editComment,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.post(variables.blogPostId) });
    },
  });
};
