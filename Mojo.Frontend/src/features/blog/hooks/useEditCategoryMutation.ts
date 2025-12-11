import { useMutation, useQueryClient } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { CategoryMutationResponse, EditCategoryRequest } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';

export const useEditCategoryMutation = () => {
  const queryClient = useQueryClient();

  return useMutation<CategoryMutationResponse, Error, EditCategoryRequest>({
    mutationFn: blogApi.updateCategory,
    onSuccess: (_response, variables) => {
      queryClient.invalidateQueries({ queryKey: blogQueryKeys.categories(variables.pageId) });
    },
  });
};
