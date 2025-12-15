import type { PageMenuItem } from '@shared/types/menu.types';
import { findMenuItemByPath } from '@shared/utils/menuUtils';

export const FORUM_FEATURE_NAME = 'ForumsFeatureName';

export const isForumMenuItem = (item: PageMenuItem): boolean => {
  if (item.featureName === FORUM_FEATURE_NAME) {
    return true;
  }

  const title = item.title?.toLowerCase() ?? '';
  const url = item.url?.toLowerCase() ?? '';
  return title.includes('forum') || url.includes('forum');
};

export const findForumMenuItem = (
  menuItems: PageMenuItem[],
  pathname: string
): PageMenuItem | null => {
  const primaryMatch = findMenuItemByPath(menuItems, pathname, isForumMenuItem);
  if (primaryMatch) {
    return primaryMatch;
  }

  const queue: PageMenuItem[] = [...menuItems];
  while (queue.length > 0) {
    const current = queue.shift();
    if (!current) {
      continue;
    }

    if (isForumMenuItem(current)) {
      return current;
    }

    if (current.children && current.children.length > 0) {
      queue.unshift(...current.children);
    }
  }

  return null;
};

export const findForumPageId = (menuItems: PageMenuItem[], pathname: string): number | null =>
  findForumMenuItem(menuItems, pathname)?.id ?? null;
