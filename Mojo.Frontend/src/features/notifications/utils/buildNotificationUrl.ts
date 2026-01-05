import { BLOG_FEATURE_NAME } from '@features/blog/constants';
import type { NotificationDto } from '../types/notification.types';
import { FORUM_FEATURE_NAME } from '@features/forum/utils/findForumPage';

/**
 * Builds the full navigation URL for a notification based on its feature type.
 * Uses entityGuid or entityId depending on what the module supports.
 */
export const buildNotificationUrl = (notification: NotificationDto): string => {
  const { featureName, url, entityGuid, entityId } = notification;

  switch (featureName) {
    case BLOG_FEATURE_NAME:
      // Blog uses entityGuid (the blogPostGuid)
      if (entityGuid) {
        return `/blog/post/${entityGuid}`;
      }
      break;

    case FORUM_FEATURE_NAME:
      // Forum uses entityId (thread id)
      if (entityId) {
        return `/forum/thread/${entityId}`;
      }
      break;
  }

  // Fallback: use url field if available
  if (url) {
    if (url.startsWith('/')) {
      return url;
    }
    return `/${url}`;
  }

  return '/';
};
