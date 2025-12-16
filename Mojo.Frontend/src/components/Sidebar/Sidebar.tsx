import { Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Toolbar, Box, Divider, useMediaQuery, useTheme } from '@mui/material';
import { Settings, Layers, Category } from '@mui/icons-material';
import { useNavigate, useLocation } from 'react-router-dom';
import { NavMenuItem } from '../NavMenuItem/NavMenuItem';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { useAuth } from '@features/auth/providers/useAuth';
import './Sidebar.css';

const DRAWER_WIDTH = 260;

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
  const { hasRole } = useAuth();
  const isAdmin = hasRole('admin');

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
        {loading && (
          <ListItem>
            <ListItemText primary="Loading navigation..." />
          </ListItem>
        )}
        {!loading && menuItems.length === 0 && (
          <ListItem>
            <ListItemText primary="No navigation available" secondary="Please check back later." />
          </ListItem>
        )}
        {!loading &&
          menuItems.map((item) => <NavMenuItem key={item.id} item={item} onNavigate={onClose} />)}
      </List>

      {isAdmin && (
        <>
          <Divider />
          <List>
            <ListItem disablePadding>
              <ListItemButton
                selected={location.pathname.startsWith('/admin')}
                onClick={() => handleNavigation('/admin')}
              >
                <ListItemIcon>
                  <Settings />
                </ListItemIcon>
                <ListItemText primary="Admin" />
              </ListItemButton>
            </ListItem>
            <ListItem disablePadding>
              <ListItemButton
                selected={location.pathname === '/blog/categories'}
                onClick={() => handleNavigation('/blog/categories')}
              >
                <ListItemIcon>
                  <Category />
                </ListItemIcon>
                <ListItemText primary="Manage Categories" />
              </ListItemButton>
            </ListItem>
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
