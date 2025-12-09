import { Typography, Box, Stack, Button } from '@mui/material';
import { Add } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useBlogPostsQuery } from '../hooks/useBlogPostsQuery';
import { BlogCard } from '../components/BlogCard';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import { useAuth } from '@features/auth/providers/AuthProvider';
import { savePostLoginRedirect } from '@features/auth/utils/postLoginRedirect';
import './BlogList.css';

export const BlogList = () => {
  const { blogPageId, menuLoading, menuError } = useBlogPageContext();
  const {
    data: posts = [],
    isLoading: loadingPosts,
    error,
  } = useBlogPostsQuery(blogPageId);
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();

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

  if (posts.length === 0) {
    return (
      <StatusMessage severity="info">
        No posts found. The blog is empty. Check back soon for new content!
      </StatusMessage>
    );
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
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={() => {
            if (isAuthenticated) {
              navigate('/blog/create');
              return;
            }
            const target = '/blog/create';
            savePostLoginRedirect(target);
            navigate(`/auth/login?redirect=${encodeURIComponent(target)}`);
          }}
          size="large"
        >
          Create New Post
        </Button>
      </Box>

      <Stack spacing={3}>
        {posts.map((post) => (
          <BlogCard key={post.blogPostGuid} post={post} />
        ))}
      </Stack>
    </>
  );
};
