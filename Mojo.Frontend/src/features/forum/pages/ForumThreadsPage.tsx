import { useState, type FormEvent } from 'react';
import { Box, Typography, Card, CardContent, Stack, Chip, Button, Tooltip } from '@mui/material';
import ForumIcon from '@mui/icons-material/Forum';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';
import VisibilityIcon from '@mui/icons-material/Visibility';
import LockIcon from '@mui/icons-material/Lock';
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import AddCommentIcon from '@mui/icons-material/AddComment';
import { useLocation, useNavigate } from 'react-router-dom';
import { useForumPageContext } from '../hooks/useForumPageContext';
import { useForumThreadsQuery } from '../hooks/useForumThreadsQuery';
import { LoadingState, StatusMessage } from '@shared/ui';
import type { ForumThreadSummary, GetThreadsResponseDto } from '../types/forum.types';
import { useAuth } from '@features/auth/providers/useAuth';
import { useCreateThreadMutation } from '../hooks/useCreateThreadMutation';
import { useForumIdentifiers } from '../hooks/useForumIdentifiers';
import { ThreadSubjectDialog } from '../components/ThreadSubjectDialog';
import './ForumThreadsPage.css';

const formatDate = (value?: string | null) =>
  value ? new Date(value).toLocaleString() : 'No activity yet';

export const ForumThreadsPage = () => {
  const navigate = useNavigate();
  const location = useLocation();
  const { forumPageId, forumPageUrl, forumPageTitle, menuLoading, menuError } = useForumPageContext();
  const { forumId: contextualForumId } = useForumIdentifiers();
  const { user, isAuthenticated } = useAuth();
  const {
    data,
    isLoading: threadsLoading,
    error,
    fetchNextPage,
    hasNextPage,
    isFetchingNextPage,
  } = useForumThreadsQuery({ pageId: forumPageId });
  const createThreadMutation = useCreateThreadMutation();
  const threads: ForumThreadSummary[] =
    data?.pages.flatMap((page: GetThreadsResponseDto) => page.threads) ?? [];
  const normalizedListPath = forumPageUrl ?? location.pathname ?? '/forum';
  const [createDialogOpen, setCreateDialogOpen] = useState(false);
  const [newThreadSubject, setNewThreadSubject] = useState('');
  const [createError, setCreateError] = useState<string | null>(null);
  const resolvedForumId = contextualForumId ?? (threads[0]?.forumId ?? null);
  const canStartThread = Boolean(
    isAuthenticated &&
      user?.legacyId != null &&
      forumPageId != null &&
      resolvedForumId != null
  );

  const buildThreadUrl = (targetThreadId: number, targetForumId: number) => {
    const basePath = normalizedListPath.endsWith('/')
      ? normalizedListPath.slice(0, -1)
      : normalizedListPath;
    const threadPath = `${basePath}/thread/${targetThreadId}`;
    const params = new URLSearchParams();
    params.set('forumId', String(targetForumId));
    if (forumPageUrl) {
      params.set('pageUrl', forumPageUrl);
    }
    if (forumPageId != null) {
      params.set('pageId', String(forumPageId));
    }

    return params.size > 0 ? `${threadPath}?${params.toString()}` : threadPath;
  };

  const handleThreadNavigation = (thread: ForumThreadSummary) => {
    navigate(buildThreadUrl(thread.id, thread.forumId));
  };

  const handleOpenCreateDialog = () => {
    setCreateError(null);
    setNewThreadSubject('');
    setCreateDialogOpen(true);
  };

  const handleCloseCreateDialog = () => {
    if (createThreadMutation.isPending) {
      return;
    }
    setCreateDialogOpen(false);
    setCreateError(null);
    setNewThreadSubject('');
  };

  const handleCreateThreadSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (forumPageId == null) {
      setCreateError('Unable to determine the forum page context.');
      return;
    }

    const targetForumId = resolvedForumId;
    if (targetForumId == null) {
      setCreateError('Select a forum before creating a new thread.');
      return;
    }

    const trimmedSubject = newThreadSubject.trim();
    if (!trimmedSubject) {
      setCreateError('Thread subject is required.');
      return;
    }

    setCreateError(null);

    try {
      const response = await createThreadMutation.mutateAsync({
        pageId: forumPageId,
        forumId: targetForumId,
        subject: trimmedSubject,
      });

      if (response.isSuccess && response.threadId) {
        setCreateDialogOpen(false);
        setNewThreadSubject('');
        navigate(buildThreadUrl(response.threadId, targetForumId));
      } else {
        setCreateError(response.message ?? 'Failed to create thread.');
      }
    } catch (err) {
      setCreateError(err instanceof Error ? err.message : 'Failed to create thread.');
    }
  };

  const showEmptyState = threads.length === 0;

  if (menuLoading || threadsLoading) {
    return <LoadingState message="Loading forum threads..." minHeight={240} />;
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

  if (error) {
    return <StatusMessage>{error.message}</StatusMessage>;
  }

  return (
    <Box className="forum-threads-page">
      <Box className="forum-threads-hero">
        <Stack
          direction={{ xs: 'column', md: 'row' }}
          justifyContent="space-between"
          alignItems={{ xs: 'flex-start', md: 'center' }}
          gap={2}
        >
          <Box>
            <Chip icon={<ForumIcon />} label="Community Forum" color="primary" variant="outlined" size="small" />
            <Typography variant="h3" component="h1">
              {forumPageTitle ?? 'Forum threads'}
            </Typography>
            <Typography variant="body1" color="text.secondary">
              Choose a thread to dive into the discussion. New replies bubble to the top as soon as they are posted.
            </Typography>
          </Box>
          {canStartThread && (
            <Button
              variant="contained"
              size="large"
              startIcon={<AddCommentIcon />}
              onClick={handleOpenCreateDialog}
            >
              Start a thread
            </Button>
          )}
        </Stack>
      </Box>

      {showEmptyState ? (
        <StatusMessage severity="info">
          This forum does not have any threads yet. Be the first to start a conversation!
        </StatusMessage>
      ) : (
        <>
          <Stack spacing={2} className="forum-thread-list">
            {threads.map((thread) => {
              const lastUpdated = formatDate(thread.mostRecentPostDate);
              return (
                <Card key={thread.id} className="forum-thread-card" variant="outlined">
                  <CardContent>
                    <Box className="forum-thread-card-header">
                      <Box>
                        <Typography variant="h5" component="h2" className="forum-thread-title">
                          {thread.subject}
                        </Typography>
                        <Typography variant="body2" color="text.secondary">
                          Started by {thread.startedByUserName} on {formatDate(thread.createdAt)}
                        </Typography>
                      </Box>
                      <Stack direction="row" spacing={1} alignItems="center">
                        {thread.isLocked && (
                          <Chip icon={<LockIcon fontSize="small" />} label="Locked" color="error" size="small" />
                        )}
                        <Button
                          variant="contained"
                          size="small"
                          endIcon={<ArrowForwardIcon />}
                          onClick={() => handleThreadNavigation(thread)}
                        >
                          Open thread
                        </Button>
                      </Stack>
                    </Box>

                    <Stack direction={{ xs: 'column', sm: 'row' }} spacing={2} className="forum-thread-stats">
                      <Tooltip title="Replies">
                        <Box className="forum-thread-stat">
                          <ChatBubbleOutlineIcon />
                          <Typography variant="body2">{thread.totalReplies} replies</Typography>
                        </Box>
                      </Tooltip>
                      <Tooltip title="Views">
                        <Box className="forum-thread-stat">
                          <VisibilityIcon />
                          <Typography variant="body2">{thread.totalViews} views</Typography>
                        </Box>
                      </Tooltip>
                      <Tooltip title="Most recent activity">
                        <Box className="forum-thread-stat">
                          <Typography variant="body2" color="text.secondary">
                            Last activity {lastUpdated}
                          </Typography>
                        </Box>
                      </Tooltip>
                    </Stack>
                  </CardContent>
                </Card>
              );
            })}
          </Stack>

          {hasNextPage && (
            <Box className="forum-threads-load-more">
              <Button
                variant="outlined"
                size="large"
                onClick={() => {
                  void fetchNextPage();
                }}
                disabled={isFetchingNextPage}
              >
                {isFetchingNextPage ? 'Loading more threads…' : 'Load more threads'}
              </Button>
            </Box>
          )}
        </>
      )}

      <ThreadSubjectDialog
        open={createDialogOpen}
        title="Create a new thread"
        helperText="Provide a concise subject to kick off the conversation."
        subject={newThreadSubject}
        onSubjectChange={setNewThreadSubject}
        onClose={handleCloseCreateDialog}
        onSubmit={handleCreateThreadSubmit}
        isSubmitting={createThreadMutation.isPending}
        error={createError}
        submitLabel="Create thread"
        cancelLabel="Cancel"
      />
    </Box>
  );
};
