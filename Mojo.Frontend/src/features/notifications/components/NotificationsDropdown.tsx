import { useState, useCallback } from 'react';
import {
  IconButton,
  Badge,
  Popover,
  List,
  ListItem,
  ListItemText,
  Typography,
  Box,
  Divider,
  CircularProgress,
} from '@mui/material';
import { Notifications as NotificationsIcon } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useNotifications } from '../providers/useNotifications';
import type { NotificationDto } from '../types/notification.types';
import './NotificationsDropdown.css';

const formatRelativeTime = (dateString: string): string => {
  const date = new Date(dateString);
  const now = new Date();
  const diffMs = now.getTime() - date.getTime();
  const diffMins = Math.floor(diffMs / 60000);
  const diffHours = Math.floor(diffMs / 3600000);
  const diffDays = Math.floor(diffMs / 86400000);

  if (diffMins < 1) return 'Just now';
  if (diffMins < 60) return `${diffMins}m ago`;
  if (diffHours < 24) return `${diffHours}h ago`;
  if (diffDays < 7) return `${diffDays}d ago`;
  return date.toLocaleDateString();
};

export const NotificationsDropdown = () => {
  const navigate = useNavigate();
  const { notifications, unreadCount, isLoading, markAsRead } = useNotifications();
  const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);

  const handleOpen = useCallback((event: React.MouseEvent<HTMLButtonElement>) => {
    setAnchorEl(event.currentTarget);
  }, []);

  const handleClose = useCallback(() => {
    setAnchorEl(null);
  }, []);

  const handleNotificationHover = useCallback(
    (notification: NotificationDto) => {
      if (!notification.isRead) {
        markAsRead(notification.notificationId);
      }
    },
    [markAsRead]
  );

  const handleNotificationClick = useCallback(
    (notification: NotificationDto) => {
      if (!notification.isRead) {
        markAsRead(notification.notificationId);
      }
      handleClose();
      if (notification.url) {
        navigate(notification.url);
      }
    },
    [markAsRead, handleClose, navigate]
  );

  const open = Boolean(anchorEl);

  return (
    <>
      <IconButton color="inherit" size="small" onClick={handleOpen}>
        <Badge badgeContent={unreadCount} color="error">
          <NotificationsIcon />
        </Badge>
      </IconButton>

      <Popover
        open={open}
        anchorEl={anchorEl}
        onClose={handleClose}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'right',
        }}
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        slotProps={{
          paper: {
            className: 'notifications-dropdown-paper',
          },
        }}
      >
        <Box className="notifications-dropdown-header">
          <Typography variant="h6">Notifications</Typography>
          {unreadCount > 0 && (
            <Typography variant="caption" color="text.secondary">
              {unreadCount} unread
            </Typography>
          )}
        </Box>

        <Divider />

        {isLoading ? (
          <Box className="notifications-dropdown-loading">
            <CircularProgress size={24} />
          </Box>
        ) : notifications.length === 0 ? (
          <Box className="notifications-dropdown-empty">
            <Typography variant="body2" color="text.secondary">
              No notifications yet
            </Typography>
          </Box>
        ) : (
          <List className="notifications-dropdown-list" disablePadding>
            {notifications.map((notification) => (
              <ListItem
                key={notification.notificationId}
                className={`notifications-dropdown-item ${!notification.isRead ? 'unread' : ''}`}
                onMouseEnter={() => handleNotificationHover(notification)}
                onClick={() => handleNotificationClick(notification)}
              >
                <ListItemText
                  primary={notification.message}
                  secondary={formatRelativeTime(notification.createdAt)}
                  primaryTypographyProps={{
                    variant: 'body2',
                    fontWeight: notification.isRead ? 'normal' : 'medium',
                  }}
                  secondaryTypographyProps={{
                    variant: 'caption',
                  }}
                />
              </ListItem>
            ))}
          </List>
        )}
      </Popover>
    </>
  );
};
