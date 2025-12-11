import type { PageMenuItem } from '@shared/types/menu.types';

export const normalizeMenuPath = (path?: string | null): string => {
  if (!path) {
    return '/';
  }

  let normalized = path.trim();
  if (!normalized.startsWith('/')) {
    normalized = `/${normalized}`;
  }

  if (normalized.length > 1 && normalized.endsWith('/')) {
    normalized = normalized.slice(0, -1);
  }

  return normalized;
};

const isRootPath = (path: string) => path === '/';

export const isPathMatch = (menuPath: string, currentPath: string): boolean => {
  const normalizedMenuPath = normalizeMenuPath(menuPath);
  const normalizedCurrentPath = normalizeMenuPath(currentPath);

  if (isRootPath(normalizedMenuPath)) {
    return normalizedCurrentPath === '/';
  }

  return (
    normalizedCurrentPath === normalizedMenuPath ||
    normalizedCurrentPath.startsWith(`${normalizedMenuPath}/`)
  );
};

export const findMenuItemByPath = (
  items: PageMenuItem[],
  pathname: string,
  predicate?: (item: PageMenuItem) => boolean
): PageMenuItem | null => {
  const normalizedPath = normalizeMenuPath(pathname);
  let bestMatch: { item: PageMenuItem; score: number } | null = null;
  const queue: PageMenuItem[] = [...items];

  while (queue.length > 0) {
    const current = queue.shift();
    if (!current) {
      continue;
    }

    if (current.children && current.children.length > 0) {
      queue.unshift(...current.children);
    }

    if (predicate && !predicate(current)) {
      continue;
    }

    const currentPath = normalizeMenuPath(current.url);
    if (isPathMatch(currentPath, normalizedPath)) {
      const score = isRootPath(currentPath) ? 1 : currentPath.length;
      if (!bestMatch || score > bestMatch.score) {
        bestMatch = { item: current, score };
      }
    }
  }

  return bestMatch?.item ?? null;
};

export const findMenuItemById = (items: PageMenuItem[], id?: number | null): PageMenuItem | null => {
  if (typeof id !== 'number') {
    return null;
  }

  const queue: PageMenuItem[] = [...items];
  while (queue.length > 0) {
    const current = queue.shift();
    if (!current) {
      continue;
    }

    if (current.id === id) {
      return current;
    }

    if (current.children && current.children.length > 0) {
      queue.unshift(...current.children);
    }
  }

  return null;
};
