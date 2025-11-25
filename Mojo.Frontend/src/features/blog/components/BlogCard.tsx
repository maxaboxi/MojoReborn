import { Card, CardContent, Typography, Chip, Box } from '@mui/material';
import { CalendarToday, Person, Chat } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import type { BlogPost } from '../../../types/blog.types';
import './BlogCard.css';

interface BlogCardProps {
  post: BlogPost;
}

export const BlogCard = ({ post }: BlogCardProps) => {
  const navigate = useNavigate();

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  const truncateContent = (content: string, maxLength: number = 200) => {
    const stripped = content.replace(/<[^>]*>/g, '');
    return stripped.length > maxLength ? stripped.substring(0, maxLength) + '...' : stripped;
  };

  const handleClick = () => {
    navigate(`/blog/post/${post.blogPostGuid}`);
  };

  return (
    <Card className="blog-card" onClick={handleClick}>
      <CardContent>
        <Box className="blog-card-date">
          <CalendarToday fontSize="small" />
          <Typography variant="caption" color="text.secondary">
            {formatDate(post.createdAt)}
          </Typography>
        </Box>

        <Typography variant="h5" component="h2" className="blog-card-title">
          {post.title}
        </Typography>

        {post.categories && post.categories.length > 0 && (
          <Box className="blog-card-categories">
            {post.categories.map((category, index) => (
              <Chip key={index} label={category} size="small" />
            ))}
          </Box>
        )}

        <Typography variant="body2" color="text.secondary" className="blog-card-content">
          {truncateContent(post.content)}
        </Typography>

        <Box className="blog-card-footer">
          <Box className="blog-card-meta-item">
            <Person fontSize="small" />
            <Typography variant="caption">{post.author}</Typography>
          </Box>
          <Box className="blog-card-meta-item">
            <Chat fontSize="small" />
            <Typography variant="caption">{post.commentCount}</Typography>
          </Box>
        </Box>
      </CardContent>
    </Card>
  );
};
