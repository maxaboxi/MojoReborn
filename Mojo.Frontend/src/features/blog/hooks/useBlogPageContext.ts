import { useMemo } from 'react';
import { useLocation, useSearchParams } from 'react-router-dom';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { findBlogMenuItem, isBlogMenuItem } from '../utils/findBlogPageId';
import { findMenuItemById, normalizeMenuPath } from '@shared/utils/menuUtils';

export const useBlogPageContext = () => {
  const location = useLocation();
  const [searchParams] = useSearchParams();
  const { menuItems, loading, error } = useMenuQuery();
  const overridePath = searchParams.get('pageUrl');
  const overridePageId = searchParams.get('pageId');
  const numericPageId = overridePageId ? Number.parseInt(overridePageId, 10) : undefined;

  const blogPage = useMemo(() => {
    if (typeof numericPageId === 'number' && !Number.isNaN(numericPageId)) {
      const matchById = findMenuItemById(menuItems, numericPageId);
      if (matchById && isBlogMenuItem(matchById)) {
        return matchById;
      }
    }

    const targetPath = overridePath ?? location.pathname;
    return findBlogMenuItem(menuItems, targetPath);
  }, [location.pathname, menuItems, numericPageId, overridePath]);

  return {
    blogPageId: blogPage?.id ?? null,
    blogPageUrl: blogPage ? normalizeMenuPath(blogPage.url) : null,
    blogPageTitle: blogPage?.title ?? null,
    blogModuleGuid: blogPage?.moduleGuid ?? null,
    menuLoading: loading,
    menuError: error,
  } as const;
};
