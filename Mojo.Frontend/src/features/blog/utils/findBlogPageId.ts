import type { PageMenuItem } from '@shared/types/menu.types';

const matchesBlog = (item: PageMenuItem): boolean => {
  const title = item.title?.toLowerCase() ?? '';
  const url = item.url?.toLowerCase() ?? '';
  return title.includes('blog') || url.includes('blog');
};

export const findBlogPageId = (menuItems: PageMenuItem[]): number | null => {
  const stack: PageMenuItem[] = [...menuItems];

  while (stack.length > 0) {
    const current = stack.shift();
    if (!current) {
      continue;
    }

    if (matchesBlog(current)) {
      return current.id;
    }

    if (current.children && current.children.length > 0) {
      stack.unshift(...current.children);
    }
  }

  return null;
};
