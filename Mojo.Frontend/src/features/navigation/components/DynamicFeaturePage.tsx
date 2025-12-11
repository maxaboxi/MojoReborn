import { useLocation } from 'react-router-dom';
import { useMemo } from 'react';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { findMenuItemByPath } from '@shared/utils/menuUtils';
import { LoadingState, StatusMessage } from '@shared/ui';
import { BlogList } from '@features/blog/pages/BlogList';
import type { PageMenuItem } from '@shared/types/menu.types';
import { isBlogMenuItem } from '@features/blog/utils/findBlogPageId';

const BLOG_FEATURE_NAME = 'BlogFeatureName';

const renderFeaturePage = (page: PageMenuItem) => {
  if (isBlogMenuItem(page)) {
    return <BlogList />;
  }

  switch (page.featureName) {
    case BLOG_FEATURE_NAME:
      return <BlogList />;
    case 'NewsletterSignUpFeatureName':
    case 'ContactFormFeatureName':
    case 'ForumsFeatureName':
    case 'ImageGalleryFeatureName':
    case 'PollFeatureName':
    case 'SharedFilesFeatureName':
    case 'SurveyFeatureName':
      return (
        <StatusMessage severity="info">
          The {page.title} feature is coming soon.
        </StatusMessage>
      );
    default:
      return (
        <StatusMessage severity="info">
          This page is not yet supported in the new frontend.
        </StatusMessage>
      );
  }
};

export const DynamicFeaturePage = () => {
  const location = useLocation();
  const { menuItems, loading, error } = useMenuQuery();

  const matchedPage = useMemo(
    () => findMenuItemByPath(menuItems, location.pathname),
    [location.pathname, menuItems]
  );

  if (loading) {
    return <LoadingState message="Loading page..." minHeight={200} />;
  }

  if (error) {
    return <StatusMessage>{error}</StatusMessage>;
  }

  if (!matchedPage) {
    return (
      <StatusMessage severity="warning">
        We couldn\'t find a page for this address. Please check the CMS configuration.
      </StatusMessage>
    );
  }

  return renderFeaturePage(matchedPage);
};
