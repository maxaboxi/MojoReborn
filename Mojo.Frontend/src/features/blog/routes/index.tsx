import type { RouteObject } from 'react-router-dom';
import { BlogList } from '../pages/BlogList';
import { BlogPostDetail } from '../pages/BlogPostDetail';
import { CreateBlogPost } from '../pages/CreateBlogPost';
import { EditBlogPost } from '../pages/EditBlogPost';

export const blogRoutes: RouteObject[] = [
  { path: 'blog', element: <BlogList /> },
  { path: 'blog/post/:id', element: <BlogPostDetail /> },
  { path: 'blog/create', element: <CreateBlogPost /> },
  { path: 'blog/edit/:id', element: <EditBlogPost /> },
];
