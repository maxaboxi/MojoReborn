import type { RouteObject } from 'react-router-dom';
import { AppLayout } from '@components/AppLayout/AppLayout';
import { blogRoutes } from '@features/blog/routes';
import { HomePage } from '@features/home/pages/HomePage';
import { AuthLayout } from '@features/auth/layouts/AuthLayout';
import { AuthLoginPage } from '@features/auth/pages/AuthLoginPage';
import { LegacyMigrationPage } from '@features/auth/pages/LegacyMigrationPage';
import { DynamicFeaturePage } from '@features/navigation/components/DynamicFeaturePage';
import { forumRoutes } from '@features/forum/routes';

const comingSoon = (label: string) => <div>{label} - Coming Soon</div>;

export const rootRoutes: RouteObject[] = [
  {
    path: '/',
    element: <AppLayout />,
    children: [
      { index: true, element: <HomePage /> },
      ...blogRoutes,
      ...forumRoutes,
      { path: 'admin', element: comingSoon('Admin') },
      { path: '*', element: <DynamicFeaturePage /> },
    ],
  },
  {
    path: '/auth',
    element: <AuthLayout />,
    children: [
      { path: 'login', element: <AuthLoginPage /> },
      { path: 'migrate-legacy', element: <LegacyMigrationPage /> },
    ],
  },
];
