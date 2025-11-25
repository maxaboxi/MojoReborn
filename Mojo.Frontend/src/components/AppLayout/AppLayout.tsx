import { Box, Toolbar } from '@mui/material';
import { Outlet } from 'react-router-dom';
import { useState } from 'react';
import { Sidebar } from '../Sidebar/Sidebar';
import { Header } from '../Header/Header';
import './AppLayout.css';

export const AppLayout = () => {
  const [mobileOpen, setMobileOpen] = useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  return (
    <Box className="app-layout">
      <Header onMenuClick={handleDrawerToggle} />
      <Sidebar mobileOpen={mobileOpen} onClose={handleDrawerToggle} />
      <Box
        component="main"
        className="main-content"
        sx={{
          flexGrow: 1,
          p: { xs: 2, sm: 3 },
          maxWidth: { xs: '100%', md: '80%'},
          margin: 'auto',
          minHeight: '100vh',
        }}
      >
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
};
