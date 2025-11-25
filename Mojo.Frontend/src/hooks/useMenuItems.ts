import { useState, useEffect } from 'react';
import { menuApi } from '../api/menu.api';
import type { PageMenuItem } from '../types/menu.types';

export const useMenuItems = () => {
  const [menuItems, setMenuItems] = useState<PageMenuItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMenu = async () => {
      try {
        setLoading(true);
        setError(null);
        const data = await menuApi.getMenu();
        setMenuItems(data);
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch menu');
        setMenuItems([]);
      } finally {
        setLoading(false);
      }
    };

    fetchMenu();
  }, []);

  return { menuItems, loading, error };
};
