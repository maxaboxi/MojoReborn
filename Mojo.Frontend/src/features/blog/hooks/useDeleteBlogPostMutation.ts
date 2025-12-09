import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { DeletePostRequest, DeletePostResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useDeleteBlogPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<DeletePostResponse, Error, DeletePostRequest>({
    mutationFn: blogApi.deletePost,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.posts(variables.pageId) });
      queryClient.removeQueries({
        queryKey: blogQueryKeys.post(variables.blogPostId, variables.pageId),
      });
    },
  });
};
