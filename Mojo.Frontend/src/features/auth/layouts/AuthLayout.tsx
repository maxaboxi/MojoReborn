import { Outlet } from 'react-router-dom';
import { Box } from '@mui/material';
import { PostLoginRedirectListener } from '../components/PostLoginRedirectListener';
import './AuthLayout.css';

export const AuthLayout = () => (
  <Box className="auth-layout">
    <PostLoginRedirectListener />
    <Outlet />
  </Box>
);
