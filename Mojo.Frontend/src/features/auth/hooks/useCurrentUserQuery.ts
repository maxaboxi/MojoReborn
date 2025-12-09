import { useQuery } from '@tanstack/react-query';
import type { AxiosError } from 'axios';
import { authApi } from '../api/authApi';
import { authQueryKeys } from './authQueryKeys';
import type { CurrentUser } from '@shared/types/auth.types';

export const useCurrentUserQuery = () =>
  useQuery<CurrentUser | null, AxiosError>({
    queryKey: authQueryKeys.currentUser(),
    queryFn: async () => {
      try {
        return await authApi.getCurrentUser();
      } catch (error) {
        const axiosError = error as AxiosError;
        if (axiosError.response?.status === 401) {
          return null;
        }
        throw axiosError;
      }
    },
    staleTime: 0,
    refetchOnWindowFocus: false,
  });
