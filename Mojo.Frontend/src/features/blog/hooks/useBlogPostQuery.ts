import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { BlogPostDetail } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';
import { BLOG_COMMENTS_PAGE_SIZE } from '../constants';

export const useBlogPostQuery = (id?: string, pageId?: number | null) =>
  useQuery<BlogPostDetail, Error>({
    queryKey: blogQueryKeys.post(id, pageId),
    queryFn: () =>
      blogApi.getPost({
        id: id as string,
        pageId: pageId as number,
        amount: BLOG_COMMENTS_PAGE_SIZE,
      }),
    enabled: Boolean(id && typeof pageId === 'number'),
  });
