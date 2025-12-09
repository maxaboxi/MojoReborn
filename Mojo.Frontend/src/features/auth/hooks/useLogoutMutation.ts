import { useMutation, useQueryClient } from '@tanstack/react-query';
import { authApi } from '../api/authApi';
import { authQueryKeys } from './authQueryKeys';

export const useLogoutMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: authApi.logout,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: authQueryKeys.currentUser() });
    },
  });
};
