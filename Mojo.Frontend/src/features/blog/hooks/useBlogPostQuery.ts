import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { BlogPost } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogPostQuery = (id?: string, pageId?: number | null) =>
  useQuery<BlogPost, Error>({
    queryKey: blogQueryKeys.post(id, pageId),
    queryFn: () => blogApi.getPost(id as string, pageId as number),
    enabled: Boolean(id && typeof pageId === 'number'),
  });
