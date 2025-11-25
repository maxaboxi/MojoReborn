import { List, ListItem, ListItemButton, ListItemIcon, ListItemText, Collapse } from '@mui/material';
import { ExpandLess, ExpandMore, Article } from '@mui/icons-material';
import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import type { PageMenuItem } from '../../types/menu.types';
import './NavMenuItem.css';

interface NavMenuItemProps {
  item: PageMenuItem;
  depth?: number;
  onNavigate?: () => void;
}

export const NavMenuItem = ({ item, depth = 0, onNavigate }: NavMenuItemProps) => {
  const navigate = useNavigate();
  const location = useLocation();
  const [open, setOpen] = useState(false);
  const hasChildren = item.children && item.children.length > 0;

  const handleClick = () => {
    if (hasChildren) {
      setOpen(!open);
    } else {
      navigate(item.url);
      onNavigate?.();
    }
  };

  const isActive = location.pathname === item.url;

  return (
    <>
      <ListItem disablePadding sx={{ pl: depth * 2 }}>
        <ListItemButton
          selected={isActive}
          onClick={handleClick}
        >
          <ListItemIcon>
            <Article />
          </ListItemIcon>
          <ListItemText primary={item.title} />
          {hasChildren && (open ? <ExpandLess /> : <ExpandMore />)}
        </ListItemButton>
      </ListItem>
      
      {hasChildren && (
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            {item.children.map((child) => (
              <NavMenuItem key={child.id} item={child} depth={depth + 1} onNavigate={onNavigate} />
            ))}
          </List>
        </Collapse>
      )}
    </>
  );
};
