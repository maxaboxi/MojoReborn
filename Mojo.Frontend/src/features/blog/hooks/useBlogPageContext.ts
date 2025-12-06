import { useMemo } from 'react';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { findBlogPageId } from '../utils/findBlogPageId';

export const useBlogPageContext = () => {
  const { menuItems, loading, error } = useMenuQuery();
  const blogPageId = useMemo(() => findBlogPageId(menuItems), [menuItems]);

  return {
    blogPageId,
    menuLoading: loading,
    menuError: error,
  } as const;
};
