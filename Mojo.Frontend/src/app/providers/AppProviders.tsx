import { useState, type ReactNode } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ThemeProvider } from '@shared/theme/ThemeProvider';
import { AppRouter } from '@app/router/AppRouter';
import { AuthProvider } from '@features/auth/providers/AuthProvider';

interface AppProvidersProps {
  children?: ReactNode;
}

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
    <ThemeProvider>
      <QueryClientProvider client={queryClient}>
        <AuthProvider>{children ?? <AppRouter />}</AuthProvider>
      </QueryClientProvider>
    </ThemeProvider>
  );
};
