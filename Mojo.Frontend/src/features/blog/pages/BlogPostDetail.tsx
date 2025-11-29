import { useParams, useNavigate } from 'react-router-dom';
import { 
  Container, 
  Typography, 
  Box, 
  Chip, 
  CircularProgress, 
  Alert, 
  Button, 
  Card, 
  CardContent,
  Divider 
} from '@mui/material';
import { CalendarToday, Person, Chat, ArrowBack, Share, Edit } from '@mui/icons-material';
import { useBlogPost } from '../hooks/useBlogPost';
import './BlogPostDetail.css';

export const BlogPostDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { post, loading, error } = useBlogPost(id || '');

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  if (loading) {
    return (
      <Box className="blog-post-loading">
        <CircularProgress size={60} />
      </Box>
    );
  }

  if (error || !post) {
    return (
      <Container maxWidth="md">
        <Alert severity="error">{error || 'Post not found'}</Alert>
      </Container>
    );
  }

  return (
    <>
      <Box className="blog-post-breadcrumb">
        <Typography variant="body2" color="text.secondary">
          Home / Blog / {post.title}
        </Typography>
      </Box>

      <Card elevation={3} className="blog-post-card">
        <CardContent className="blog-post-content">
          <Typography variant="h3" component="h1" className="blog-post-title">
            {post.title}
          </Typography>

          <Box className="blog-post-meta">
            <Box className="blog-post-meta-item">
              <Person color="primary" />
              <Typography variant="body2" fontWeight="medium">
                {post.author}
              </Typography>
            </Box>
            <Box className="blog-post-meta-item">
              <CalendarToday color="primary" />
              <Typography variant="body2">{formatDate(post.createdAt)}</Typography>
            </Box>
            <Box className="blog-post-meta-item">
              <Chat color="primary" />
              <Typography variant="body2">
                {post.commentCount} {post.commentCount === 1 ? 'Comment' : 'Comments'}
              </Typography>
            </Box>
          </Box>

          {post.categories && post.categories.length > 0 && (
            <Box className="blog-post-categories">
              {post.categories.map((category, index) => (
                <Chip key={index} label={category} color="primary" variant="outlined" />
              ))}
            </Box>
          )}

          <Divider className="blog-post-divider" />

          <Typography variant="h5" component="h2" className="blog-post-subtitle">
            {post.subTitle}
          </Typography>

          <Divider className="blog-post-divider" />

          <Box 
            className="blog-post-body"
            dangerouslySetInnerHTML={{ __html: post.content }}
          />
        </CardContent>
      </Card>

      <Box className="blog-post-actions">
        <Button
          variant="outlined"
          startIcon={<ArrowBack />}
          onClick={() => navigate('/blog')}
          size="large"
        >
          Back to Blog
        </Button>
        <Box sx={{ display: 'flex', gap: 2 }}>
          <Button
            variant="contained"
            startIcon={<Edit />}
            onClick={() => navigate(`/blog/edit/${id}`)}
            size="large"
          >
            Edit Post
          </Button>
          <Button variant="outlined" startIcon={<Share />} size="large">
            Share
          </Button>
        </Box>
      </Box>
    </>
  );
};
