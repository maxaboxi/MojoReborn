import { useMemo, useState } from 'react';
import {
  Box,
  Typography,
  Stack,
  Button,
  Chip,
  Divider,
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import PersonIcon from '@mui/icons-material/Person';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import ForumIcon from '@mui/icons-material/Forum';
import { useNavigate } from 'react-router-dom';
import { useForumThreadQuery } from '../hooks/useForumThreadQuery';
import { useForumPageContext } from '../hooks/useForumPageContext';
import { useForumIdentifiers } from '../hooks/useForumIdentifiers';
import { LoadingState, StatusMessage } from '@shared/ui';
import type { ForumPost, ForumViewMode } from '../types/forum.types';
import { ForumPostCard } from '../components/ForumPostCard';
import { ForumViewToggle } from '../components/ForumViewToggle';
import './ForumThreadPage.css';

interface ForumThreadPageProps {
  forumId?: number | null;
  threadId?: number | null;
}

type ForumPostNode = ForumPost & { replies: ForumPostNode[] };

const buildPostTree = (posts: ForumPost[]): ForumPostNode[] => {
  const sorted = [...posts].sort((a, b) => a.threadSequence - b.threadSequence);
  const lookup = new Map<string, ForumPostNode>();
  const roots: ForumPostNode[] = [];

  sorted.forEach((post) => {
    lookup.set(post.postGuid.toLowerCase(), { ...post, replies: [] });
  });

  sorted.forEach((post) => {
    const node = lookup.get(post.postGuid.toLowerCase());
    if (!node) {
      return;
    }

    const parentKey = post.replyToPostId?.toLowerCase();
    if (parentKey) {
      const parentNode = lookup.get(parentKey);
      if (parentNode) {
        parentNode.replies.push(node);
        return;
      }
    }

    roots.push(node);
  });

  return roots;
};

const formatTimestamp = (value: string) =>
  new Date(value).toLocaleString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

const ForumNestedPost = ({ node, depth }: { node: ForumPostNode; depth: number }) => (
  <Box className="forum-nested-post">
    <ForumPostCard post={node} depth={depth} variant="nested" />
    {node.replies.length > 0 && (
      <Box className="forum-nested-children">
        {node.replies.map((child) => (
          <ForumNestedPost key={child.postGuid} node={child} depth={depth + 1} />
        ))}
      </Box>
    )}
  </Box>
);

export const ForumThreadPage = ({ forumId: overrideForumId, threadId: overrideThreadId }: ForumThreadPageProps = {}) => {
  const navigate = useNavigate();
  const { forumId: derivedForumId, threadId: derivedThreadId, currentPath } = useForumIdentifiers();
  const forumId = overrideForumId ?? derivedForumId;
  const threadId = overrideThreadId ?? derivedThreadId;
  const { forumPageId, forumPageUrl, forumPageTitle, menuLoading, menuError } = useForumPageContext();
  const [viewMode, setViewMode] = useState<ForumViewMode>('classic');
  const {
    data: thread,
    isLoading,
    error,
  } = useForumThreadQuery({ pageId: forumPageId, forumId, threadId });
  const posts = useMemo(() => thread?.forumPosts ?? [], [thread]);
  const nestedPosts = useMemo(() => buildPostTree(posts), [posts]);

  const listPath = forumPageUrl ?? (currentPath?.split('/thread')[0] ?? '/forum');
  const buildListUrl = () => {
    const params = new URLSearchParams();
    if (forumId != null) {
      params.set('forumId', String(forumId));
    }
    if (forumPageUrl) {
      params.set('pageUrl', forumPageUrl);
    }
    if (forumPageId != null) {
      params.set('pageId', String(forumPageId));
    }
    return params.size > 0 ? `${listPath}?${params.toString()}` : listPath;
  };

  if (menuLoading || isLoading) {
    return <LoadingState message="Loading thread..." minHeight={240} />;
  }

  if (menuError) {
    return <StatusMessage>{menuError}</StatusMessage>;
  }

  if (forumPageId == null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the forum module for this page. Confirm the CMS navigation entry is configured for the Forums feature.
      </StatusMessage>
    );
  }

  if (forumId == null || threadId == null) {
    return (
      <StatusMessage severity="info">
        We need both a forumId and threadId to display this conversation. Use the forum listing to pick a thread.
      </StatusMessage>
    );
  }

  if (error) {
    return <StatusMessage>{error.message}</StatusMessage>;
  }

  if (!thread) {
    return <StatusMessage>Thread not found.</StatusMessage>;
  }

  return (
    <Box className="forum-thread-page">
      <Box className="forum-thread-hero">
        <Stack direction={{ xs: 'column', md: 'row' }} justifyContent="space-between" alignItems="flex-start" gap={2}>
          <Box>
            <Chip icon={<ForumIcon />} label={forumPageTitle ?? 'Forum thread'} size="small" color="primary" variant="outlined" />
            <Typography variant="h3" component="h1" className="forum-thread-title">
              {thread.subject}
            </Typography>
            <Stack direction="row" spacing={2} className="forum-thread-meta">
              <span>
                <PersonIcon fontSize="small" /> {thread.userName}
              </span>
              <span>
                <CalendarTodayIcon fontSize="small" /> {formatTimestamp(posts[0]?.createdAt ?? new Date().toISOString())}
              </span>
            </Stack>
          </Box>
          <Stack direction={{ xs: 'column', sm: 'row' }} gap={1} alignItems="flex-start">
            <ForumViewToggle value={viewMode} onChange={setViewMode} />
            <Button startIcon={<ArrowBackIcon />} onClick={() => navigate(buildListUrl())} size="small">
              Back to threads
            </Button>
          </Stack>
        </Stack>
      </Box>

      <Divider className="forum-thread-divider" />

      {viewMode === 'classic' ? (
        <Stack spacing={2} className="forum-thread-classic">
          {posts.map((post) => (
            <ForumPostCard key={post.postGuid} post={post} />
          ))}
        </Stack>
      ) : (
        <Box className="forum-thread-nested">
          {nestedPosts.map((node) => (
            <ForumNestedPost key={node.postGuid} node={node} depth={0} />
          ))}
        </Box>
      )}
    </Box>
  );
};
