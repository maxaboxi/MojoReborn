import { useState, type ReactNode } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ThemeProvider } from '@shared/theme/ThemeProvider';
import { AppRouter } from '@app/router/AppRouter';
import { AuthProvider } from '@features/auth/providers/AuthProvider';
import { useAuth } from '@features/auth/providers/useAuth';
import { NotificationsProvider } from '@features/notifications/providers/NotificationsProvider';
import { ErrorBoundary } from '@shared/ui';

interface AppProvidersProps {
  children?: ReactNode;
}

const AuthGatedNotifications = ({ children }: { children: ReactNode }) => {
  const { isAuthenticated } = useAuth();

  if (!isAuthenticated) return <>{children}</>;
  return <NotificationsProvider>{children}</NotificationsProvider>;
};

export const AppProviders = ({ children }: AppProvidersProps) => {
  const [queryClient] = useState(
    () =>
      new QueryClient({
        defaultOptions: {
          queries: {
            staleTime: 60 * 1000,
            refetchOnWindowFocus: false,
            retry: 1,
          },
          mutations: {
            retry: 0,
          },
        },
      })
  );

  return (
    <ErrorBoundary>
      <ThemeProvider>
        <QueryClientProvider client={queryClient}>
          <AuthProvider>
            <AuthGatedNotifications>{children ?? <AppRouter />}</AuthGatedNotifications>
          </AuthProvider>
        </QueryClientProvider>
      </ThemeProvider>
    </ErrorBoundary>
  );
};
