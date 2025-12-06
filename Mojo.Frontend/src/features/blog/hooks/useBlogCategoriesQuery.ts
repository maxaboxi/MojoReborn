import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CategoryDto } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogCategoriesQuery = (pageId?: number | null) =>
  useQuery<CategoryDto[], Error>({
    queryKey: blogQueryKeys.categories(pageId),
    queryFn: () => blogApi.getCategories(pageId as number),
    enabled: typeof pageId === 'number',
    staleTime: 5 * 60 * 1000,
  });
