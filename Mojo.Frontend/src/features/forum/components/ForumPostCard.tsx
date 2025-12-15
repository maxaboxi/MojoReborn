import { Paper, Box, Typography, Chip, Stack } from '@mui/material';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import PersonIcon from '@mui/icons-material/Person';
import TrendingUpIcon from '@mui/icons-material/TrendingUp';
import type { ForumPost } from '../types/forum.types';
import './ForumPostCard.css';

interface ForumPostCardProps {
  post: ForumPost;
  depth?: number;
  variant?: 'classic' | 'nested';
}

const formatTimestamp = (value: string) =>
  new Date(value).toLocaleString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

export const ForumPostCard = ({ post, depth = 0, variant = 'classic' }: ForumPostCardProps) => {
  const elevation = variant === 'classic' ? 2 : 1;

  return (
    <Paper
      elevation={elevation}
      className={`forum-post-card forum-post-card-${variant}`}
      sx={{
        borderLeft: depth > 0 ? '4px solid var(--mui-palette-primary-light)' : undefined,
        ml: depth > 0 ? { xs: 1, sm: depth * 3 } : 0,
      }}
    >
      <Stack direction={{ xs: 'column', sm: 'row' }} justifyContent="space-between" gap={1} className="forum-post-meta">
        <Stack direction="row" spacing={2} alignItems="center">
          <Box className="forum-post-meta-item">
            <PersonIcon fontSize="small" />
            <Typography variant="body2" fontWeight={600}>
              {post.userName || 'Anonymous'}
            </Typography>
          </Box>
          <Box className="forum-post-meta-item">
            <CalendarTodayIcon fontSize="small" />
            <Typography variant="body2" color="text.secondary">
              {formatTimestamp(post.createdAt)}
            </Typography>
          </Box>
        </Stack>
        <Chip
          icon={<TrendingUpIcon fontSize="small" />}
          label={`${post.points} ${post.points === 1 ? 'point' : 'points'}`}
          variant="outlined"
          color="secondary"
          size="small"
        />
      </Stack>

      <Box
        className="forum-post-content"
        dangerouslySetInnerHTML={{ __html: post.content }}
      />
    </Paper>
  );
};
