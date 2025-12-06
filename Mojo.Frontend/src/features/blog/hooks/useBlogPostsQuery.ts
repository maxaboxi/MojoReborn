import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { BlogPost } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogPostsQuery = (pageId?: number | null) =>
  useQuery<BlogPost[], Error>({
    queryKey: blogQueryKeys.posts(pageId),
    queryFn: () => blogApi.getPosts(pageId as number),
    enabled: typeof pageId === 'number',
    staleTime: 30 * 1000,
  });
