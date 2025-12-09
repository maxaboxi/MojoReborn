import { createContext, useContext, useMemo, useCallback, type ReactNode } from 'react';
import type { CurrentUser } from '@shared/types/auth.types';
import { useCurrentUserQuery } from '../hooks/useCurrentUserQuery';

export type AuthContextValue = {
  user: CurrentUser | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  isFetching: boolean;
  error: unknown;
  refetchUser: () => Promise<CurrentUser | null>;
};

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const { data, error, isLoading, isFetching, refetch } = useCurrentUserQuery();
  const user = data ?? null;

  const refetchUser = useCallback(async () => {
    const result = await refetch();
    return result.data ?? null;
  }, [refetch]);

  const value = useMemo<AuthContextValue>(
    () => ({
      user,
      isAuthenticated: Boolean(user?.id),
      isLoading,
      isFetching,
      error,
      refetchUser,
    }),
    [error, isFetching, isLoading, refetchUser, user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
