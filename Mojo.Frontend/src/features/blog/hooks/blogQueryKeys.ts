const BLOG_ROOT = ['blog'] as const;

export const blogQueryKeys = {
  all: BLOG_ROOT,
  posts: () => [...BLOG_ROOT, 'posts'] as const,
  post: (id?: string) => [...BLOG_ROOT, 'post', id ?? ''] as const,
  categories: () => [...BLOG_ROOT, 'categories'] as const,
};
