import { useEffect, useMemo, useState, type FormEvent } from 'react';
import {
  Box,
  Typography,
  Stack,
  Button,
  Chip,
  Divider,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  DialogContentText,
  TextField,
  Alert,
} from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import PersonIcon from '@mui/icons-material/Person';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import ForumIcon from '@mui/icons-material/Forum';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import { useNavigate } from 'react-router-dom';
import { useForumThreadQuery } from '../hooks/useForumThreadQuery';
import { useForumPageContext } from '../hooks/useForumPageContext';
import { useForumIdentifiers } from '../hooks/useForumIdentifiers';
import { LoadingState, StatusMessage } from '@shared/ui';
import type { ForumPost, ForumViewMode } from '../types/forum.types';
import { ForumPostCard } from '../components/ForumPostCard';
import { ForumViewToggle } from '../components/ForumViewToggle';
import { forumApi } from '../api/forumApi';
import { useAuth } from '@features/auth/providers/useAuth';
import { useEditThreadMutation } from '../hooks/useEditThreadMutation';
import { useDeleteThreadMutation } from '../hooks/useDeleteThreadMutation';
import './ForumThreadPage.css';

interface ForumThreadPageProps {
  forumId?: number | null;
  threadId?: number | null;
}

type ForumPostNode = ForumPost & { replies: ForumPostNode[] };

const THREAD_POSTS_PAGE_SIZE = 50;

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
  const { user } = useAuth();
  const [viewMode, setViewMode] = useState<ForumViewMode>('classic');
  const [posts, setPosts] = useState<ForumPost[]>([]);
  const [hasMorePosts, setHasMorePosts] = useState(true);
  const [isFetchingMorePosts, setIsFetchingMorePosts] = useState(false);
  const [postPaginationError, setPostPaginationError] = useState<string | null>(null);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [editSubject, setEditSubject] = useState('');
  const [editError, setEditError] = useState<string | null>(null);
  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [deleteError, setDeleteError] = useState<string | null>(null);
  const editThreadMutation = useEditThreadMutation();
  const deleteThreadMutation = useDeleteThreadMutation();
  const {
    data: thread,
    isLoading,
    error,
    refetch: refetchThread,
  } = useForumThreadQuery({ pageId: forumPageId, forumId, threadId, amount: THREAD_POSTS_PAGE_SIZE });

  useEffect(() => {
    const initialPosts = thread?.forumPosts ?? [];
    setPosts(initialPosts);
    setPostPaginationError(null);
    setHasMorePosts(initialPosts.length === THREAD_POSTS_PAGE_SIZE);
  }, [thread]);

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
      const response = await editThreadMutation.mutateAsync({
        pageId: forumPageId,
        forumId,
        threadId,
        subject: trimmedSubject,
      });

      if (response.isSuccess) {
        setEditDialogOpen(false);
        await refetchThread();
      } else {
        setEditError(response.message ?? 'Failed to update this thread.');
      }
    } catch (err) {
      setEditError(err instanceof Error ? err.message : 'Failed to update this thread.');
    }
  };

  const handleOpenDeleteDialog = () => {
    setDeleteError(null);
    setDeleteDialogOpen(true);
  };

  const handleCloseDeleteDialog = () => {
    if (deleteThreadMutation.isPending) {
      return;
    }
    setDeleteDialogOpen(false);
    setDeleteError(null);
  };

  const handleDeleteThreadConfirm = async () => {
    if (forumPageId == null || forumId == null || threadId == null) {
      setDeleteError('Unable to determine the forum context.');
      return;
    }

    setDeleteError(null);

    try {
      const response = await deleteThreadMutation.mutateAsync({
        pageId: forumPageId,
        forumId,
        threadId,
      });

      if (response.isSuccess) {
        setDeleteDialogOpen(false);
        navigate(buildListUrl());
      } else {
        setDeleteError(response.message ?? 'Failed to delete this thread.');
      }
    } catch (err) {
      setDeleteError(err instanceof Error ? err.message : 'Failed to delete this thread.');
    }
  };

  const handleLoadMorePosts = async () => {
    if (isFetchingMorePosts || !forumPageId || !forumId || !threadId) {
      return;
    }

    const lastSequence = posts[posts.length - 1]?.threadSequence ?? 0;
    setIsFetchingMorePosts(true);
    setPostPaginationError(null);
    try {
      const response = await forumApi.getThread({
        pageId: forumPageId,
        forumId,
        threadId,
        amount: THREAD_POSTS_PAGE_SIZE,
        lastThreadSequence: lastSequence,
      });

      setPosts((prev) => {
        const filtered = response.forumPosts.filter(
          (post) => !prev.some((existing) => existing.postGuid === post.postGuid)
        );
        return [...prev, ...filtered];
      });
      if (response.forumPosts.length < THREAD_POSTS_PAGE_SIZE) {
        setHasMorePosts(false);
      }
    } catch (err) {
      setPostPaginationError(
        err instanceof Error ? err.message : 'Failed to load additional replies.'
      );
    } finally {
      setIsFetchingMorePosts(false);
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
                <Button
                  variant="outlined"
                  color="error"
                  startIcon={<DeleteIcon />}
                  size="small"
                  onClick={handleOpenDeleteDialog}
                >
                  Delete thread
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
        <Box className="forum-thread-nested">
          {nestedPosts.map((node) => (
            <ForumNestedPost key={node.postGuid} node={node} depth={0} />
          ))}
        </Box>
      )}

      {postPaginationError && (
        <Typography variant="body2" color="error" textAlign="center">
          {postPaginationError}
        </Typography>
      )}

      {hasMorePosts && (
        <Box sx={{ textAlign: 'center', mt: 2 }}>
          <Button
            variant="outlined"
            onClick={handleLoadMorePosts}
            disabled={isFetchingMorePosts}
          >
            {isFetchingMorePosts ? 'Loading more replies…' : 'Load more replies'}
          </Button>
        </Box>
      )}

      <Dialog open={editDialogOpen} onClose={handleCloseEditDialog} fullWidth maxWidth="sm">
        <Box component="form" onSubmit={handleEditThreadSubmit} sx={{ width: '100%' }}>
          <DialogTitle>Edit thread subject</DialogTitle>
          <DialogContent sx={{ pt: 1 }}>
            <TextField
              label="Thread subject"
              value={editSubject}
              onChange={(event) => setEditSubject(event.target.value)}
              fullWidth
              disabled={editThreadMutation.isPending}
              autoFocus
            />
            {editError && (
              <Alert severity="error" sx={{ mt: 2 }}>
                {editError}
              </Alert>
            )}
          </DialogContent>
          <DialogActions>
            <Button onClick={handleCloseEditDialog} disabled={editThreadMutation.isPending}>
              Cancel
            </Button>
            <Button type="submit" variant="contained" disabled={editThreadMutation.isPending}>
              {editThreadMutation.isPending ? 'Saving…' : 'Save changes'}
            </Button>
          </DialogActions>
        </Box>
      </Dialog>

      <Dialog open={deleteDialogOpen} onClose={handleCloseDeleteDialog}>
        <DialogTitle>Delete this thread?</DialogTitle>
        <DialogContent>
          <DialogContentText>
            Are you sure you want to delete "{thread.subject}"? This action cannot be undone.
          </DialogContentText>
          {deleteError && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {deleteError}
            </Alert>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDeleteDialog} disabled={deleteThreadMutation.isPending}>
            Cancel
          </Button>
          <Button
            onClick={handleDeleteThreadConfirm}
            color="error"
            variant="contained"
            disabled={deleteThreadMutation.isPending}
          >
            {deleteThreadMutation.isPending ? 'Deleting…' : 'Delete'}
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};
