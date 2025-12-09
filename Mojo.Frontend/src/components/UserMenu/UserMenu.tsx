import { useState } from 'react';
import { IconButton, Menu, MenuItem, ListItemIcon, Divider, Avatar, Button } from '@mui/material';
import { Person, Settings, Logout } from '@mui/icons-material';
import { useLocation, useNavigate } from 'react-router-dom';
import { useAuth } from '@features/auth/providers/AuthProvider';
import { useLogoutMutation } from '@features/auth/hooks/useLogoutMutation';
import { savePostLoginRedirect } from '@features/auth/utils/postLoginRedirect';
import './UserMenu.css';

export const UserMenu = () => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();
  const location = useLocation();
  const { user, isAuthenticated } = useAuth();
  const logoutMutation = useLogoutMutation();
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleMenuItemClick = (path: string) => {
    navigate(path);
    handleClose();
  };

  const handleLogout = async () => {
    await logoutMutation.mutateAsync();
    handleClose();
    navigate('/', { replace: true });
  };

  const handleSignInClick = () => {
    const redirectPath = `${location.pathname}${location.search}`;
    savePostLoginRedirect(redirectPath);
    navigate(`/auth/login?redirect=${encodeURIComponent(redirectPath)}`);
  };

  if (!isAuthenticated) {
    return (
      <Button variant="outlined" size="small" onClick={handleSignInClick} className="user-menu-signin">
        Sign in
      </Button>
    );
  }

  const avatarContent = user?.avatarUrl ? (
    <Avatar src={user.avatarUrl} sx={{ width: 32, height: 32 }} />
  ) : (
    <Avatar sx={{ width: 32, height: 32 }}>
      {user?.displayName?.[0]?.toUpperCase() ?? user?.email?.[0]?.toUpperCase() ?? 'U'}
    </Avatar>
  );

  return (
    <>
      <IconButton
        onClick={handleClick}
        size="small"
        className="user-menu-button"
      >
        {avatarContent}
      </IconButton>

      <Menu
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        onClick={handleClose}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        <MenuItem onClick={() => handleMenuItemClick('/profile')}>
          <ListItemIcon>
            <Person fontSize="small" />
          </ListItemIcon>
          Profile
        </MenuItem>
        <MenuItem onClick={() => handleMenuItemClick('/account')}>
          <ListItemIcon>
            <Settings fontSize="small" />
          </ListItemIcon>
          Settings
        </MenuItem>
        <Divider />
        <MenuItem onClick={handleLogout} disabled={logoutMutation.isPending}>
          <ListItemIcon>
            <Logout fontSize="small" />
          </ListItemIcon>
          {logoutMutation.isPending ? 'Signing out…' : 'Sign Out'}
        </MenuItem>
      </Menu>
    </>
  );
};
