import type { PageMenuItem } from '@shared/types/menu.types';
import { findMenuItemByPath } from '@shared/utils/menuUtils';
import { BLOG_FEATURE_NAME } from '../constants';

export const isBlogMenuItem = (item: PageMenuItem): boolean => {
  if (item.featureName === BLOG_FEATURE_NAME) {
    return true;
  }

  const title = item.title?.toLowerCase() ?? '';
  const url = item.url?.toLowerCase() ?? '';
  return title.includes('blog') || url.includes('blog');
};

export const findBlogMenuItem = (menuItems: PageMenuItem[], pathname: string): PageMenuItem | null => {
  const primaryMatch = findMenuItemByPath(menuItems, pathname, isBlogMenuItem);
  if (primaryMatch) {
    return primaryMatch;
  }

  const stack: PageMenuItem[] = [...menuItems];
  while (stack.length > 0) {
    const current = stack.shift();
    if (!current) {
      continue;
    }

    if (isBlogMenuItem(current)) {
      return current;
    }

    if (current.children && current.children.length > 0) {
      stack.unshift(...current.children);
    }
  }

  return null;
};

export const findBlogPageId = (menuItems: PageMenuItem[], pathname: string): number | null =>
  findBlogMenuItem(menuItems, pathname)?.id ?? null;
