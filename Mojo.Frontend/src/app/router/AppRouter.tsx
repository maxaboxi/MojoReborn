import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { rootRoutes } from './routes';

const router = createBrowserRouter(rootRoutes);

export const AppRouter = () => <RouterProvider router={router} />;
