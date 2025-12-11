import type { RouteObject } from 'react-router-dom';
import { BlogList } from '../pages/BlogList';
import { BlogPostDetail } from '../pages/BlogPostDetail';
import { CreateBlogPost } from '../pages/CreateBlogPost';
import { EditBlogPost } from '../pages/EditBlogPost';
import { ManageCategoriesPage } from '../pages/ManageCategoriesPage';
import { RequireAuth } from '@features/auth/components/RequireAuth';

export const blogRoutes: RouteObject[] = [
  { path: 'blog', element: <BlogList /> },
  { path: 'blog/post/:id', element: <BlogPostDetail /> },
  {
    path: 'blog/create',
    element: (
      <RequireAuth message="Sign in to create a blog post.">
        <CreateBlogPost />
      </RequireAuth>
    ),
  },
  {
    path: 'blog/edit/:id',
    element: (
      <RequireAuth message="Sign in to edit this blog post.">
        <EditBlogPost />
      </RequireAuth>
    ),
  },
  {
    path: 'blog/categories',
    element: (
      <RequireAuth message="Sign in to manage blog categories.">
        <ManageCategoriesPage />
      </RequireAuth>
    ),
  },
];
