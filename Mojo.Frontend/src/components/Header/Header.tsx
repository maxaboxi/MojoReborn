import { AppBar, Toolbar, IconButton, Box, useMediaQuery, useTheme } from '@mui/material';
import { Mail, Brightness4, Brightness7, Menu } from '@mui/icons-material';
import { useTheme as useAppTheme } from '@shared/theme/useTheme';
import { NotificationsDropdown } from '@features/notifications/components/NotificationsDropdown';
import { UserMenu } from '../UserMenu/UserMenu';
import './Header.css';

interface HeaderProps {
  onMenuClick: () => void;
}

export const Header = ({ onMenuClick }: HeaderProps) => {
  const { mode, toggleTheme } = useAppTheme();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  return (
    <AppBar
      position="fixed"
      className="app-header"
      color="default"
      elevation={1}
    >
      <Toolbar className="header-toolbar">
        {isMobile && (
          <IconButton
            color="inherit"
            edge="start"
            onClick={onMenuClick}
            className="menu-button"
          >
            <Menu />
          </IconButton>
        )}
        
        <Box className="header-spacer" />
        
        <Box className="header-actions">
          <NotificationsDropdown />

          <IconButton color="inherit" size="small">
            <Mail />
          </IconButton>

          <IconButton onClick={toggleTheme} color="inherit" size="small">
            {mode === 'dark' ? <Brightness7 /> : <Brightness4 />}
          </IconButton>

          <UserMenu />
        </Box>
      </Toolbar>
    </AppBar>
  );
};
