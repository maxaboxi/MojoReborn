import type { RouteObject } from 'react-router-dom';
import { ForumFeaturePage } from '../pages/ForumFeaturePage';

export const forumRoutes: RouteObject[] = [
  { path: 'forum', element: <ForumFeaturePage /> },
  { path: 'forum/:forumId', element: <ForumFeaturePage /> },
  { path: 'forum/:forumId/thread/:threadId', element: <ForumFeaturePage /> },
  { path: 'forum/thread/:threadId', element: <ForumFeaturePage /> },
];
