import { useQuery } from '@tanstack/react-query';
import { menuApi } from '../api/menuApi';
import type { PageMenuItem } from '../types/menu.types';

export const MENU_QUERY_KEY = ['menu'];

export const useMenuQuery = () => {
  const query = useQuery<PageMenuItem[], Error>({
    queryKey: MENU_QUERY_KEY,
    queryFn: menuApi.getMenu,
    staleTime: 5 * 60 * 1000,
  });

  return {
    ...query,
    menuItems: query.data ?? [],
    loading: query.isLoading,
    error: query.error?.message ?? null,
  } as const;
};
