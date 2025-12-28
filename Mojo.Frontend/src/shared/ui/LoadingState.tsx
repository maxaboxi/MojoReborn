import { Box, CircularProgress, Typography } from '@mui/material';
import type { ReactNode } from 'react';
import './LoadingState.css';

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
    className={['loading-state', className].filter(Boolean).join(' ')}
    style={{ minHeight }}
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
