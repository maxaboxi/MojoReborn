import { Typography, Box, Stack, Button } from '@mui/material';
import { Add } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useBlogPostsQuery } from '../hooks/useBlogPostsQuery';
import { BlogCard } from '../components/BlogCard';
import { BlogSubscribeButton } from '../components/BlogSubscribeButton';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import { useAuth } from '@features/auth/providers/useAuth';
import { savePostLoginRedirect } from '@features/auth/utils/postLoginRedirect';
import './BlogList.css';

export const BlogList = () => {
  const { blogPageId, blogPageUrl, blogModuleGuid, menuLoading, menuError } = useBlogPageContext();
  const {
    data,
    isLoading: loadingPosts,
    error,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
  } = useBlogPostsQuery(blogPageId);
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();
  const normalizedBlogPath = blogPageUrl ?? '/blog';
  const pageUrlQuery = blogPageUrl ? `?pageUrl=${encodeURIComponent(blogPageUrl)}` : '';
  const posts = data?.pages.flatMap((page) => page.blogPosts ?? []) ?? [];

  if (menuLoading || loadingPosts) {
    return <LoadingState className="blog-list-loading" minHeight={200} />;
  }

  if (menuError) {
    return <StatusMessage>{menuError}</StatusMessage>;
  }

  if (blogPageId == null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the blog page context. Please refresh the page or contact an administrator.
      </StatusMessage>
    );
  }

  if (error) {
    return <StatusMessage>{error.message}</StatusMessage>;
  }

  return (
    <>
      <Box className="blog-list-header">
        <Box>
          <Typography variant="h3" component="h1" className="blog-list-title">
            Blog Posts
          </Typography>
          <Typography variant="subtitle1" color="text.secondary" className="blog-list-subtitle">
            Discover our latest articles and insights
          </Typography>
        </Box>
        <Stack direction="row" spacing={2}>
          <BlogSubscribeButton pageId={blogPageId} moduleGuid={blogModuleGuid} />
          <Button
            variant="contained"
            startIcon={<Add />}
            onClick={() => {
              const target = `/blog/create${pageUrlQuery}`;
              if (isAuthenticated) {
                navigate(target);
                return;
              }
              savePostLoginRedirect(target);
              navigate(`/auth/login?redirect=${encodeURIComponent(target)}`);
            }}
            size="large"
          >
            Create New Post
          </Button>
        </Stack>
      </Box>

      {posts.length === 0 ? (
        <StatusMessage severity="info">
          No posts found. The blog is empty. Check back soon for new content!
        </StatusMessage>
      ) : (
        <>
          <Stack spacing={3}>
            {posts.map((post) => (
              <BlogCard key={post.blogPostGuid} post={post} basePath={normalizedBlogPath} />
            ))}
          </Stack>

          {hasNextPage && (
            <Box className="blog-list-load-more">
              <Button
                variant="outlined"
                onClick={() => fetchNextPage()}
                disabled={isFetchingNextPage}
              >
                {isFetchingNextPage ? 'Loading...' : 'Load More Posts'}
              </Button>
            </Box>
          )}
        </>
      )}
    </>
  );
};
