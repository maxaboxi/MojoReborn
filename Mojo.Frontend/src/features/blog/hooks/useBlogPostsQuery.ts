import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { BlogPost } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogPostsQuery = () =>
  useQuery<BlogPost[], Error>({
    queryKey: blogQueryKeys.posts(),
    queryFn: blogApi.getPosts,
    staleTime: 30 * 1000,
  });
