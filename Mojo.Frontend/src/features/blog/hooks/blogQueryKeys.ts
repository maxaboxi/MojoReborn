const BLOG_ROOT = ['blog'] as const;
const unresolved = 'unresolved';

export const blogQueryKeys = {
  all: BLOG_ROOT,
  posts: (pageId?: number | null) => [...BLOG_ROOT, 'posts', pageId ?? unresolved] as const,
  post: (id?: string, pageId?: number | null) => [...BLOG_ROOT, 'post', id ?? '', pageId ?? unresolved] as const,
  categories: (pageId?: number | null) => [...BLOG_ROOT, 'categories', pageId ?? unresolved] as const,
  subscriptions: () => [...BLOG_ROOT, 'subscriptions'] as const,
};
