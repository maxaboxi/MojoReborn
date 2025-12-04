import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { GetCategoriesResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useBlogCategoriesQuery = () =>
  useQuery<GetCategoriesResponse[], Error>({
    queryKey: blogQueryKeys.categories(),
    queryFn: blogApi.getCategories,
    staleTime: 5 * 60 * 1000,
  });
