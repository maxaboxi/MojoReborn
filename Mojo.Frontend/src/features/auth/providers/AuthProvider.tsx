import { useMemo, useCallback, type ReactNode } from 'react';
import type { CurrentUser } from '@shared/types/auth.types';
import { useCurrentUserQuery } from '../hooks/useCurrentUserQuery';
import { AuthContext } from './AuthContext';

export type AuthContextValue = {
  user: CurrentUser | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  isFetching: boolean;
  error: unknown;
  refetchUser: () => Promise<CurrentUser | null>;
  hasRole: (role: string) => boolean;
};

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const { data, error, isLoading, isFetching, refetch } = useCurrentUserQuery();

  const user = useMemo<CurrentUser | null>(() => {
    if (!data) {
      return null;
    }
    return {
      ...data,
      roles: Array.isArray(data.roles) ? data.roles : [],
    };
  }, [data]);

  const normalizedRoles = useMemo(
    () => (user?.roles ?? []).map((role) => role.toLowerCase()),
    [user?.roles]
  );

  const refetchUser = useCallback(async () => {
    const result = await refetch();
    return result.data ?? null;
  }, [refetch]);

  const hasRole = useCallback(
    (role: string) => {
      if (!role) {
        return false;
      }
      return normalizedRoles.includes(role.trim().toLowerCase());
    },
    [normalizedRoles]
  );

  const value = useMemo<AuthContextValue>(
    () => ({
      user,
      isAuthenticated: Boolean(user?.id),
      isLoading,
      isFetching,
      error,
      refetchUser,
      hasRole,
    }),
    [error, hasRole, isFetching, isLoading, refetchUser, user]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};
