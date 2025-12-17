import { useMemo, useState } from 'react';
import type { FormEvent } from 'react';
import { useSearchParams, useNavigate } from 'react-router-dom';
import {
  Box,
  Typography,
  Card,
  CardContent,
  Stack,
  TextField,
  Button,
  Alert,
} from '@mui/material';
import LockResetIcon from '@mui/icons-material/LockReset';
import { buildApiUrl } from '@shared/config/env';
import './LegacyMigrationPage.css';

const ERROR_MESSAGES: Record<string, string> = {
  password_mismatch: 'That password did not match our records. Please try again.',
  account_too_old: 'This account cannot be migrated automatically. Please contact support.',
  creation_failed: 'We could not create your new account. Please try again later.',
  role_migration_failed: 'We could not migrate your previous roles. Please reach out to support.',
};

export const LegacyMigrationPage = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [password, setPassword] = useState('');
  const [formError, setFormError] = useState<string | null>(null);
  const [isSubmitting, setSubmitting] = useState(false);

  const errorParam = searchParams.get('error');

  const errorMessage = useMemo(() => {
    if (formError) {
      return formError;
    }
    if (!errorParam) {
      return null;
    }
    return ERROR_MESSAGES[errorParam] ?? 'Unable to migrate this account. Please try again.';
  }, [errorParam, formError]);

  const handleSubmit = (event: FormEvent<HTMLFormElement>) => {
    if (!password.trim()) {
      event.preventDefault();
      setFormError('Please enter your previous password.');
      return;
    }

    setFormError(null);
    setSubmitting(true);
  };

  const handleCancel = () => {
    setFormError(null);
    navigate('/auth/login');
  };

  return (
    <Box className="legacy-migration-page">
      <Card className="legacy-migration-card" elevation={4}>
        <CardContent>
          <form
            method="post"
            action={buildApiUrl('/auth/migrate-legacy')}
            onSubmit={handleSubmit}
          >
            <Stack spacing={3}>
              <Box>
                <LockResetIcon color="primary" fontSize="large" />
                <Typography variant="h4" component="h1" sx={{ mt: 2 }}>
                  Verify your legacy account
                </Typography>
                <Typography variant="body1" color="text.secondary" sx={{ mt: 1 }}>
                  Enter the password you used on the classic Mojo portal. This lets us migrate your profile and permissions.
                </Typography>
              </Box>

              {errorMessage && <Alert severity="error">{errorMessage}</Alert>}

              <TextField
                label="Legacy password"
                type="password"
                name="oldPassword"
                value={password}
                onChange={(event) => setPassword(event.target.value)}
                required
                autoFocus
              />

              <Stack direction="row" spacing={2} justifyContent="flex-end">
                <Button onClick={handleCancel} disabled={isSubmitting} variant="outlined">
                  Cancel
                </Button>
                <Button type="submit" variant="contained" disabled={isSubmitting || !password.trim()}>
                  {isSubmitting ? 'Verifying…' : 'Verify password'}
                </Button>
              </Stack>
            </Stack>
          </form>
        </CardContent>
      </Card>
    </Box>
  );
};
