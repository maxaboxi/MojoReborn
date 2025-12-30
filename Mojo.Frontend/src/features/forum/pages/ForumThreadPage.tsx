import { useState, type FormEvent } from 'react';
import { Box, Typography, Stack, Button, Chip, Divider } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import PersonIcon from '@mui/icons-material/Person';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import ForumIcon from '@mui/icons-material/Forum';
import EditIcon from '@mui/icons-material/Edit';
import { useNavigate } from 'react-router-dom';
import { useForumThreadQuery } from '../hooks/useForumThreadQuery';
import { useForumPageContext } from '../hooks/useForumPageContext';
import { useForumIdentifiers } from '../hooks/useForumIdentifiers';
import { LoadingState, StatusMessage } from '@shared/ui';
import type { ForumViewMode } from '../types/forum.types';
import { ForumPostCard } from '../components/ForumPostCard';
import { ForumViewToggle } from '../components/ForumViewToggle';
import { useAuth } from '@features/auth/providers/useAuth';
import { useEditThreadMutation } from '../hooks/useEditThreadMutation';
import { ThreadSubjectDialog } from '../components/ThreadSubjectDialog';
import { ForumPostTree } from '../components/ForumPostTree';
import { THREAD_POSTS_PAGE_SIZE } from '../constants';
import './ForumThreadPage.css';

interface ForumThreadPageProps {
  forumId?: number | null;
  threadId?: number | null;
}

const formatTimestamp = (value: string) =>
  new Date(value).toLocaleString(undefined, {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

export const ForumThreadPage = ({ forumId: overrideForumId, threadId: overrideThreadId }: ForumThreadPageProps = {}) => {
  const navigate = useNavigate();
  const { forumId: derivedForumId, threadId: derivedThreadId, currentPath } = useForumIdentifiers();
  const forumId = overrideForumId ?? derivedForumId;
  const threadId = overrideThreadId ?? derivedThreadId;
  const { forumPageId, forumPageUrl, forumPageTitle, menuLoading, menuError } = useForumPageContext();
  const { user } = useAuth();
  const [viewMode, setViewMode] = useState<ForumViewMode>('classic');
  const [postPaginationError, setPostPaginationError] = useState<string | null>(null);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [editSubject, setEditSubject] = useState('');
  const [editError, setEditError] = useState<string | null>(null);
  const editThreadMutation = useEditThreadMutation();
  const {
    data,
    isLoading,
    error,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
    refetch: refetchThread,
  } = useForumThreadQuery({ pageId: forumPageId, forumId, threadId, amount: THREAD_POSTS_PAGE_SIZE });
  const thread = data?.pages[0];
  const posts = data?.pages.flatMap((page) => page.forumPosts ?? []) ?? [];

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

  const handleOpenEditDialog = () => {
    if (!thread) {
      return;
    }
    setEditSubject(thread.subject);
    setEditError(null);
    setEditDialogOpen(true);
  };

  const handleCloseEditDialog = () => {
    if (editThreadMutation.isPending) {
      return;
    }
    setEditDialogOpen(false);
    setEditError(null);
  };

  const handleEditThreadSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (forumPageId == null || forumId == null || threadId == null || !thread) {
      setEditError('Unable to determine the forum context. Please return to the listing and try again.');
      return;
    }

    const trimmedSubject = editSubject.trim();
    if (!trimmedSubject) {
      setEditError('Thread subject is required.');
      return;
    }

    setEditError(null);

    try {
      await editThreadMutation.mutateAsync({
        pageId: forumPageId,
        forumId,
        threadId,
        subject: trimmedSubject,
      });

      setEditDialogOpen(false);
      await refetchThread();
    } catch (err) {
      setEditError(err instanceof Error ? err.message : 'Failed to update this thread.');
    }
  };

  const handleLoadMorePosts = async () => {
    if (!hasNextPage || isFetchingNextPage) {
      return;
    }
    setPostPaginationError(null);
    try {
      await fetchNextPage();
    } catch (err) {
      setPostPaginationError(
        err instanceof Error ? err.message : 'Failed to load additional replies.'
      );
    }
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

  const canManageThread = Boolean(user?.legacyId != null && thread.userId === user.legacyId);

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
          <Stack
            direction={{ xs: 'column', sm: 'row' }}
            gap={1}
            alignItems="flex-start"
            flexWrap="wrap"
          >
            <ForumViewToggle value={viewMode} onChange={setViewMode} />
            <Button startIcon={<ArrowBackIcon />} onClick={() => navigate(buildListUrl())} size="small">
              Back to threads
            </Button>
            {canManageThread && (
              <>
                <Button
                  variant="outlined"
                  startIcon={<EditIcon />}
                  size="small"
                  onClick={handleOpenEditDialog}
                >
                  Edit subject
                </Button>
              </>
            )}
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
        <ForumPostTree posts={posts} />
      )}

      {postPaginationError && (
        <Typography variant="body2" color="error" textAlign="center">
          {postPaginationError}
        </Typography>
      )}

      {hasNextPage && (
        <Box className="forum-thread-load-more">
          <Button
            variant="outlined"
            onClick={handleLoadMorePosts}
            disabled={isFetchingNextPage}
          >
            {isFetchingNextPage ? 'Loading more replies…' : 'Load more replies'}
          </Button>
        </Box>
      )}

      <ThreadSubjectDialog
        open={editDialogOpen}
        title="Edit thread subject"
        subject={editSubject}
        onSubjectChange={setEditSubject}
        onClose={handleCloseEditDialog}
        onSubmit={handleEditThreadSubmit}
        isSubmitting={editThreadMutation.isPending}
        error={editError}
        submitLabel="Save changes"
        cancelLabel="Cancel"
      />
    </Box>
  );
};
