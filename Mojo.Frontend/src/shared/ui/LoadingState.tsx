import { Box, CircularProgress, Typography } from '@mui/material';
import type { ReactNode } from 'react';

interface LoadingStateProps {
  message?: string;
  minHeight?: number | string;
  size?: number;
  className?: string;
  children?: ReactNode;
}

export const LoadingState = ({
  message,
  minHeight = 400,
  size = 60,
  className,
  children,
}: LoadingStateProps) => (
  <Box
    className={className}
    sx={{
      minHeight,
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'center',
      gap: 2,
      textAlign: 'center',
    }}
  >
    <CircularProgress size={size} />
    {message && (
      <Typography variant="body2" color="text.secondary">
        {message}
      </Typography>
    )}
    {children}
  </Box>
);
