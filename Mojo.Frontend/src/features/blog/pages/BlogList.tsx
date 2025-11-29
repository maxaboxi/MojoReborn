import { Container, Typography, CircularProgress, Alert, Box, Stack, Button } from '@mui/material';
import { Add } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { useBlogPosts } from '../hooks/useBlogPosts';
import { BlogCard } from '../components/BlogCard';
import './BlogList.css';

export const BlogList = () => {
  const { posts, loading, error } = useBlogPosts();
  const navigate = useNavigate();

  if (loading) {
    return (
      <Box className="blog-list-loading">
        <CircularProgress size={60} />
      </Box>
    );
  }

  if (error) {
    return (
      <Container maxWidth="md">
        <Alert severity="error">{error}</Alert>
      </Container>
    );
  }

  if (posts.length === 0) {
    return (
      <Container maxWidth="md">
        <Alert severity="info">
          No posts found. The blog is empty. Check back soon for new content!
        </Alert>
      </Container>
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
