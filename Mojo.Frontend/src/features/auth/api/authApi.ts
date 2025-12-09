import apiClient from '@shared/api/axiosClient';
import type { CurrentUser } from '@shared/types/auth.types';

export const authApi = {
  getCurrentUser: async (): Promise<CurrentUser> => {
    const response = await apiClient.get<CurrentUser>('/auth/user');
    return response.data;
  },
  logout: async (): Promise<void> => {
    await apiClient.post('/auth/logout', {});
  },
};
