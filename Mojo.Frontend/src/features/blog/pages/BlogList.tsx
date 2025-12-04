import { Typography, Box, Stack, Button } from '@mui/material';
import { Add } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useBlogPostsQuery } from '../hooks/useBlogPostsQuery';
import { BlogCard } from '../components/BlogCard';
import { LoadingState, StatusMessage } from '@shared/ui';
import './BlogList.css';

export const BlogList = () => {
  const {
    data: posts = [],
    isLoading: loading,
    error,
  } = useBlogPostsQuery();
  const navigate = useNavigate();

  if (loading) {
    return <LoadingState className="blog-list-loading" minHeight={200} />;
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
          onClick={() => navigate('/blog/create')}
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
