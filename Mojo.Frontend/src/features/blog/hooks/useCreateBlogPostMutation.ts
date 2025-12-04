import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CreatePostRequest, CreatePostResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useCreateBlogPostMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CreatePostResponse, Error, CreatePostRequest>({
    mutationFn: blogApi.createPost,
    onSuccess: (response) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.posts() });
      if (response.blogPostId) {
        queryClient.invalidateQueries({ queryKey: blogQueryKeys.post(response.blogPostId) });
      }
    },
  });
};
