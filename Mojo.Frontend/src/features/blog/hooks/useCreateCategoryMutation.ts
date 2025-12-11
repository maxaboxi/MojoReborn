import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CategoryMutationResponse, CreateCategoryRequest } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useCreateCategoryMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CategoryMutationResponse, Error, CreateCategoryRequest>({
    mutationFn: blogApi.createCategory,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.categories(variables.pageId) });
    },
  });
};
