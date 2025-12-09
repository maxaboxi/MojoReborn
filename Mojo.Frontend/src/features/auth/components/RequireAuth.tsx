import type { ReactNode } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { Stack, Typography, Button } from '@mui/material';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useAuth } from '../providers/AuthProvider';
import { savePostLoginRedirect } from '../utils/postLoginRedirect';

interface RequireAuthProps {
  children: ReactNode;
  message?: string;
}

export const RequireAuth = ({ children, message }: RequireAuthProps) => {
  const { isAuthenticated, isLoading } = useAuth();
  const location = useLocation();
  const navigate = useNavigate();

  if (isLoading) {
    return <LoadingState minHeight={200} />;
  }

  if (!isAuthenticated) {
    const currentPath = `${location.pathname}${location.search}`;
    const loginPath = `/auth/login?redirect=${encodeURIComponent(currentPath)}`;

    const handleLogin = () => {
      savePostLoginRedirect(currentPath);
      navigate(loginPath, { replace: true });
    };

    return (
      <StatusMessage severity="warning">
        <Stack spacing={2} alignItems="flex-start">
          <Typography>{message ?? 'You need to sign in to continue.'}</Typography>
          <Button variant="contained" color="primary" onClick={handleLogin}>
            Go to Sign In
          </Button>
        </Stack>
      </StatusMessage>
    );
  }

  return <>{children}</>;
};
