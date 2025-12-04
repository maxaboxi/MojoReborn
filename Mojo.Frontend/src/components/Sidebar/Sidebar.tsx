import { Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Toolbar, Box, Divider, useMediaQuery, useTheme } from '@mui/material';
import { Article, Forum, Settings, Layers } from '@mui/icons-material';
import { useNavigate, useLocation } from 'react-router-dom';
import { NavMenuItem } from '../NavMenuItem/NavMenuItem';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import './Sidebar.css';

const DRAWER_WIDTH = 260;

const staticMenuItems = [
  { text: 'Blog', icon: <Article />, path: '/blog' },
  { text: 'Forum', icon: <Forum />, path: '/forum' },
  { text: 'Admin', icon: <Settings />, path: '/admin' },
];

interface SidebarProps {
  mobileOpen: boolean;
  onClose: () => void;
}

export const Sidebar = ({ mobileOpen, onClose }: SidebarProps) => {
  const navigate = useNavigate();
  const location = useLocation();
  const { menuItems, loading } = useMenuQuery();
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('md'));

  const handleNavigation = (path: string) => {
    navigate(path);
    if (isMobile) {
      onClose();
    }
  };

  const drawerContent = (
    <>
      <Toolbar>
        <Box className="sidebar-brand">
          <Layers className="sidebar-brand-icon" />
          <span className="sidebar-brand-text">MojoReborn</span>
        </Box>
      </Toolbar>
      
      <List>
        {staticMenuItems.map((item) => (
          <ListItem key={item.path} disablePadding>
            <ListItemButton
              selected={location.pathname === item.path}
              onClick={() => handleNavigation(item.path)}
            >
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      {!loading && menuItems.length > 0 && (
        <>
          <Divider />
          <List>
            {menuItems.map((item) => (
              <NavMenuItem key={item.id} item={item} onNavigate={onClose} />
            ))}
          </List>
        </>
      )}
    </>
  );

  return (
    <Box component="nav" sx={{ width: { md: DRAWER_WIDTH }, flexShrink: { md: 0 } }}>
      {/* Mobile drawer */}
      <Drawer
        variant="temporary"
        open={mobileOpen}
        onClose={onClose}
        ModalProps={{
          keepMounted: true, // Better mobile performance
        }}
        sx={{
          display: { xs: 'block', md: 'none' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: DRAWER_WIDTH },
        }}
      >
        {drawerContent}
      </Drawer>
      
      {/* Desktop drawer */}
      <Drawer
        variant="permanent"
        sx={{
          display: { xs: 'none', md: 'block' },
          '& .MuiDrawer-paper': { boxSizing: 'border-box', width: DRAWER_WIDTH },
        }}
        open
      >
        {drawerContent}
      </Drawer>
    </Box>
  );
};
