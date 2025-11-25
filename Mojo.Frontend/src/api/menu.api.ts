import apiClient from './axios.client';
import type { PageMenuItem } from '../types/menu.types';

export const menuApi = {
  /**
   * Get navigation menu items
   */
  getMenu: async (): Promise<PageMenuItem[]> => {
    const response = await apiClient.get<PageMenuItem[]>('/core/menu');
    return response.data;
  },
};
