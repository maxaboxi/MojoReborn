import { Card, CardContent, Box, Typography, Alert, Stack } from '@mui/material';
import type { BlogComment } from '../types/blog.types';

interface BlogCommentsListProps {
  comments: BlogComment[];
  commentCount: number;
  formatDate: (isoDate: string) => string;
}

export const BlogCommentsList = ({ comments, commentCount, formatDate }: BlogCommentsListProps) => {
  return (
    <Card elevation={2} className="blog-comments-card">
      <CardContent>
        <Box className="blog-comments-header">
          <Typography variant="h4" component="h3">
            Comments ({commentCount})
          </Typography>
          <Typography variant="body2" color="text.secondary">
            Join the conversation
          </Typography>
        </Box>

        {comments.length === 0 ? (
          <Alert severity="info">No comments yet. Be the first to share your thoughts.</Alert>
        ) : (
          <Stack spacing={2} className="blog-comments-list">
            {comments.map((comment) => (
              <Box key={comment.id} className="blog-comment-item">
                <Box className="blog-comment-item-header">
                  <Typography variant="subtitle1" fontWeight={600}>
                    {comment.title || 'Comment'}
                  </Typography>
                  <Typography variant="caption" color="text.secondary">
                    {formatDate(comment.createdAt)}
                  </Typography>
                </Box>
                <Typography variant="body2" color="text.secondary" className="blog-comment-author">
                  {comment.author}
                </Typography>
                <Typography variant="body1">
                  {comment.content}
                </Typography>
              </Box>
            ))}
          </Stack>
        )}
      </CardContent>
    </Card>
  );
};
