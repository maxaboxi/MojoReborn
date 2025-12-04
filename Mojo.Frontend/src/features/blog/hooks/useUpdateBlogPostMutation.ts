import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { EditPostRequest, EditPostResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useUpdateBlogPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<EditPostResponse, Error, EditPostRequest>({
    mutationFn: blogApi.updatePost,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.posts() });
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.post(variables.blogPostId) });
    },
  });
};
