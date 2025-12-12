import apiClient from './axiosClient';
import type { PageMenuItem } from '../types/menu.types';

export const menuApi = {
  getMenu: async (): Promise<PageMenuItem[]> => {
    const response = await apiClient.get<PageMenuItem[]>('/site/menu');
    return response.data;
  },
};
