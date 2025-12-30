import { AppBar, Toolbar, IconButton, Box, Badge, useMediaQuery, useTheme } from '@mui/material';
import { Notifications, Mail, Brightness4, Brightness7, Menu } from '@mui/icons-material';
import { useTheme as useAppTheme } from '@shared/theme/useTheme';
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
          <IconButton color="inherit" size="small">
            <Badge badgeContent={3} color="error">
              <Notifications />
            </Badge>
          </IconButton>

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
