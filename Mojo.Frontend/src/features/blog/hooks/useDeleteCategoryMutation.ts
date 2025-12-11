import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CategoryMutationResponse, DeleteCategoryRequest } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useDeleteCategoryMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CategoryMutationResponse, Error, DeleteCategoryRequest>({
    mutationFn: blogApi.deleteCategory,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.categories(variables.pageId) });
    },
  });
};
