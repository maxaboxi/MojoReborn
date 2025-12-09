import { useEffect, useMemo, useState } from 'react';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Stack,
  Button,
  Alert,
  TextField,
  Divider,
  Chip,
} from '@mui/material';
import GoogleIcon from '@mui/icons-material/Google';
import FacebookIcon from '@mui/icons-material/Facebook';
import MicrosoftIcon from '@mui/icons-material/Microsoft';
import SecurityIcon from '@mui/icons-material/Security';
import { useSearchParams } from 'react-router-dom';
import { buildApiUrl } from '@shared/config/env';
import { savePostLoginRedirect } from '../utils/postLoginRedirect';
import './AuthLoginPage.css';

const ERROR_MESSAGES: Record<string, string> = {
  session_expired: 'Your session expired. Please start the sign-in process again.',
  email_not_found: 'We could not read your email address from the provider.',
  creation_failed: 'Account creation failed. Please contact support.',
  linking_failed: 'We could not link your external login. Please try again.',
  account_too_old: 'This account cannot be migrated automatically. Please reach out to support.',
  password_mismatch: 'The password did not match our records. Please try again.',
  role_migration_failed: 'We could not migrate your site roles. Please contact support.',
};

const AUTH_PROVIDERS = [
  { id: 'Google', label: 'Continue with Google', icon: <GoogleIcon /> },
  { id: 'Microsoft', label: 'Continue with Microsoft', icon: <MicrosoftIcon /> },
  { id: 'Facebook', label: 'Continue with Facebook', icon: <FacebookIcon /> },
];

const buildExternalLoginUrl = (provider: string) => buildApiUrl(`/auth/external-login/${provider}`);
const buildDevLoginUrl = (email: string) => `${buildApiUrl('/auth/dev-login')}?email=${encodeURIComponent(email)}`;

export const AuthLoginPage = () => {
  const [searchParams] = useSearchParams();
  const [devEmail, setDevEmail] = useState('dev@user.com');
  const redirectParam = searchParams.get('redirect');
  const errorParam = searchParams.get('error');

  useEffect(() => {
    if (redirectParam) {
      savePostLoginRedirect(redirectParam);
    }
  }, [redirectParam]);

  const errorMessage = useMemo(() => {
    if (!errorParam) {
      return null;
    }
    return ERROR_MESSAGES[errorParam] ?? 'Unable to complete sign-in. Please try again.';
  }, [errorParam]);

  const handleProviderClick = (provider: string) => {
    if (redirectParam) {
      savePostLoginRedirect(redirectParam);
    }
    window.location.href = buildExternalLoginUrl(provider);
  };

  const handleDevLogin = () => {
    savePostLoginRedirect(redirectParam ?? '/');
    window.location.href = buildDevLoginUrl(devEmail.trim());
  };

  return (
    <Box className="auth-login-page">
      <Card className="auth-login-card" elevation={4}>
        <CardContent>
          <Stack spacing={3}>
            <Box>
              <Chip icon={<SecurityIcon />} label="Mojo Reborn" color="primary" variant="outlined" />
              <Typography variant="h4" component="h1" sx={{ mt: 2 }}>
                Sign in to continue
              </Typography>
              <Typography variant="body1" color="text.secondary" sx={{ mt: 1 }}>
                Use one of the trusted providers below. We will migrate legacy accounts the first time you sign in.
              </Typography>
            </Box>

            {errorMessage && <Alert severity="error">{errorMessage}</Alert>}

            <Stack spacing={2}>
              {AUTH_PROVIDERS.map((provider) => (
                <Button
                  key={provider.id}
                  variant="outlined"
                  size="large"
                  startIcon={provider.icon}
                  onClick={() => handleProviderClick(provider.id)}
                  className="auth-provider-button"
                >
                  {provider.label}
                </Button>
              ))}
            </Stack>

            <Alert severity="info">
              If we find a legacy account with this email, we will ask for your old password to confirm ownership during the first sign in.
            </Alert>

            {import.meta.env.DEV && (
              <>
                <Divider>Development shortcut</Divider>
                <Stack spacing={2}>
                  <TextField
                    label="Dev email"
                    type="email"
                    value={devEmail}
                    onChange={(event) => setDevEmail(event.target.value)}
                    size="small"
                  />
                  <Button variant="contained" color="secondary" onClick={handleDevLogin}>
                    Use temporary dev login
                  </Button>
                </Stack>
              </>
            )}
          </Stack>
        </CardContent>
      </Card>
    </Box>
  );
};
