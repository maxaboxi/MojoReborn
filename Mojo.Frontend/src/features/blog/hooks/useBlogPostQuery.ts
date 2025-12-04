import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { BlogPost } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogPostQuery = (id?: string) =>
  useQuery<BlogPost, Error>({
    queryKey: blogQueryKeys.post(id),
    queryFn: () => blogApi.getPost(id as string),
    enabled: Boolean(id),
  });
