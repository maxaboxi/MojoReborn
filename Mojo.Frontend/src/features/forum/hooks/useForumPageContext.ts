import { useMemo } from 'react';
import { useLocation, useSearchParams } from 'react-router-dom';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { findMenuItemById, normalizeMenuPath } from '@shared/utils/menuUtils';
import { findForumMenuItem, isForumMenuItem } from '../utils/findForumPage';

export const useForumPageContext = () => {
  const location = useLocation();
  const [searchParams] = useSearchParams();
  const { menuItems, loading, error } = useMenuQuery();
  const overridePath = searchParams.get('pageUrl');
  const overridePageId = searchParams.get('pageId');
  const numericPageId = overridePageId ? Number.parseInt(overridePageId, 10) : undefined;

  const forumPage = useMemo(() => {
    if (typeof numericPageId === 'number' && !Number.isNaN(numericPageId)) {
      const matchById = findMenuItemById(menuItems, numericPageId);
      if (matchById && isForumMenuItem(matchById)) {
        return matchById;
      }
    }

    const targetPath = overridePath ?? location.pathname;
    return findForumMenuItem(menuItems, targetPath);
  }, [location.pathname, menuItems, numericPageId, overridePath]);

  return {
    forumPageId: forumPage?.id ?? null,
    forumPageUrl: forumPage ? normalizeMenuPath(forumPage.url) : null,
    forumPageTitle: forumPage?.title ?? null,
    menuLoading: loading,
    menuError: error,
  } as const;
};
