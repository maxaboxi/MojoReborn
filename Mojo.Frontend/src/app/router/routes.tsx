import { Navigate } from 'react-router-dom';
import type { RouteObject } from 'react-router-dom';
import { AppLayout } from '@components/AppLayout/AppLayout';
import { blogRoutes } from '@features/blog/routes';

const comingSoon = (label: string) => <div>{label} - Coming Soon</div>;

export const rootRoutes: RouteObject[] = [
  {
    path: '/',
    element: <AppLayout />,
    children: [
      { index: true, element: <Navigate to="/blog" replace /> },
      ...blogRoutes,
      { path: 'forum', element: comingSoon('Forum') },
      { path: 'admin', element: comingSoon('Admin') },
      { path: '*', element: <Navigate to="/blog" replace /> },
    ],
  },
];
