import { Alert, Container } from '@mui/material';
import type { ReactNode } from 'react';
import type { AlertColor } from '@mui/material/Alert';

type MaxWidthOption = 'xs' | 'sm' | 'md' | 'lg' | 'xl' | false;

interface StatusMessageProps {
  severity?: AlertColor;
  maxWidth?: MaxWidthOption;
  className?: string;
  children?: ReactNode;
  message?: string;
  action?: ReactNode;
}

export const StatusMessage = ({
  severity = 'error',
  maxWidth = 'md',
  className,
  children,
  message = 'Something went wrong.',
  action,
}: StatusMessageProps) => (
  <Container maxWidth={maxWidth} className={className} sx={{ py: 2 }}>
    <Alert severity={severity} action={action}>
      {children ?? message}
    </Alert>
  </Container>
);
