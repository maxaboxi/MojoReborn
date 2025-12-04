import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { DeletePostResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useDeleteBlogPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<DeletePostResponse, Error, string>({
    mutationFn: blogApi.deletePost,
    onSuccess: (_response, blogPostId) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.posts() });
      queryClient.removeQueries({ queryKey: blogQueryKeys.post(blogPostId) });
    },
  });
};
