import { useParams, useNavigate, useLocation } from 'react-router-dom';
import { useMemo, useState } from 'react';
import type { SyntheticEvent } from 'react';
import {
  Typography,
  Box,
  Chip,
  Alert,
  Button,
  Card,
  CardContent,
  Divider,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  Snackbar,
} from '@mui/material';
import type { AlertColor } from '@mui/material';
import type { SnackbarCloseReason } from '@mui/material/Snackbar';
import { CalendarToday, Person, Chat, ArrowBack, Share, Edit, Delete } from '@mui/icons-material';
import { useBlogPostQuery } from '../hooks/useBlogPostQuery';
import { useDeleteBlogPostMutation } from '../hooks/useDeleteBlogPostMutation';
import { useCreateCommentMutation } from '../hooks/useCreateCommentMutation';
import { useEditCommentMutation } from '../hooks/useEditCommentMutation';
import { useDeleteCommentMutation } from '../hooks/useDeleteCommentMutation';
import type { CommentFormValues } from '../components/CommentForm';
import { BlogCommentsList } from '../components/BlogCommentsList';
import { BlogCommentFormPanel } from '../components/BlogCommentFormPanel';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import { useAuth } from '@features/auth/providers/AuthProvider';
import { savePostLoginRedirect } from '@features/auth/utils/postLoginRedirect';
import './BlogPostDetail.css';


export const BlogPostDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const location = useLocation();
  const { user, isAuthenticated } = useAuth();
  const { blogPageId, menuLoading, menuError } = useBlogPageContext();
  const {
    data: post,
    isLoading: loading,
    error: postError,
    refetch,
  } = useBlogPostQuery(id, blogPageId);
  const postErrorMessage = postError?.message ?? null;

  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [deleteError, setDeleteError] = useState<string | null>(null);
  const [commentError, setCommentError] = useState<string | null>(null);
  const [commentSuccess, setCommentSuccess] = useState<string | null>(null);
  const [commentFormKey, setCommentFormKey] = useState(0);
  const [toastState, setToastState] = useState<{
    key: number;
    message: string;
    severity: AlertColor;
  } | null>(null);
  const deleteMutation = useDeleteBlogPostMutation();
  const commentMutation = useCreateCommentMutation();
  const editCommentMutation = useEditCommentMutation();
  const deleteCommentMutation = useDeleteCommentMutation();
  const commentFormInitialData = useMemo(
    () => ({
      author: user?.email ?? '',
      title: '',
      content: '',
    }),
    [commentFormKey, user?.email]
  );

  const showToast = (message: string, severity: AlertColor = 'info') => {
    setToastState({
      key: Date.now(),
      message,
      severity,
    });
  };

  const redirectToLogin = () => {
    const target = `${location.pathname}${location.search}`;
    savePostLoginRedirect(target);
    navigate(`/auth/login?redirect=${encodeURIComponent(target)}`);
  };

  const handleToastClose = (
    _event?: SyntheticEvent | Event,
    reason?: SnackbarCloseReason
  ) => {
    if (reason === 'clickaway') {
      return;
    }
    setToastState(null);
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  };

  const handleDeleteClick = () => {
    setDeleteDialogOpen(true);
    setDeleteError(null);
  };

  const handleDeleteCancel = () => {
    setDeleteDialogOpen(false);
    setDeleteError(null);
  };

  const handleDeleteConfirm = async () => {
    if (!id) return;
    if (blogPageId == null) {
      setDeleteError('Unable to determine blog page context. Please refresh and try again.');
      return;
    }

    setDeleteError(null);

    try {
      const response = await deleteMutation.mutateAsync({
        pageId: blogPageId,
        blogPostId: id,
      });

      if (response.isSuccess) {
        navigate('/blog');
      } else {
        setDeleteError(response.message || 'Failed to delete post');
      }
    } catch (err) {
      setDeleteError(err instanceof Error ? err.message : 'An error occurred while deleting the post');
    }
  };

  const handleCommentSubmit = async (values: CommentFormValues) => {
    if (!id) {
      showToast('Unable to determine blog post context.', 'error');
      return;
    }

    if (!blogPageId) {
      const message = 'Unable to determine blog page context. Please try again later.';
      setCommentError(message);
      showToast(message, 'error');
      return;
    }

    if (!isAuthenticated || !user?.id) {
      const message = 'Please sign in to post a comment.';
      setCommentError(message);
      showToast(message, 'warning');
      redirectToLogin();
      return;
    }

    setCommentError(null);
    setCommentSuccess(null);

    try {
      const response = await commentMutation.mutateAsync({
        pageId: blogPageId,
        blogPostId: id,
        author: values.author,
        title: values.title,
        content: values.content,
      });

      if (response.isSuccess) {
        setCommentSuccess('Comment posted successfully.');
        setCommentFormKey((prev) => prev + 1);
        refetch();
        showToast('Comment posted successfully.', 'success');
      } else {
        const message = response.message || 'Failed to post comment.';
        setCommentError(message);
        showToast(message, 'error');
      }
    } catch (err) {
      const message = err instanceof Error ? err.message : 'An error occurred while posting the comment.';
      setCommentError(message);
      showToast(message, 'error');
    }
  };

  const handleCommentEdit = async (commentId: string, values: CommentFormValues) => {
    if (!id) {
      const error = new Error('Missing blog post context.');
      showToast(error.message, 'error');
      throw error;
    }

    if (!isAuthenticated) {
      const error = new Error('Sign in to edit comments.');
      showToast(error.message, 'warning');
      redirectToLogin();
      throw error;
    }

    if (blogPageId == null) {
      const error = new Error('Missing blog page context.');
      showToast(error.message, 'error');
      throw error;
    }

    try {
      const response = await editCommentMutation.mutateAsync({
        pageId: blogPageId,
        blogPostId: id,
        blogCommentId: commentId,
        title: values.title,
        content: values.content,
      });

      if (!response.isSuccess) {
        throw new Error(response.message || 'Failed to update comment.');
      }

      await refetch();
      showToast('Comment updated successfully.', 'success');
    } catch (err) {
      const error = err instanceof Error ? err : new Error('Failed to update comment.');
      showToast(error.message, 'error');
      throw error;
    }
  };

  const handleCommentDelete = async (commentId: string) => {
    if (!id) {
      const error = new Error('Missing blog post context.');
      showToast(error.message, 'error');
      throw error;
    }

    if (!isAuthenticated) {
      const error = new Error('Sign in to delete comments.');
      showToast(error.message, 'warning');
      redirectToLogin();
      throw error;
    }

    if (blogPageId == null) {
      const error = new Error('Missing blog page context.');
      showToast(error.message, 'error');
      throw error;
    }

    try {
      const response = await deleteCommentMutation.mutateAsync({
        pageId: blogPageId,
        blogPostId: id,
        blogCommentId: commentId,
      });

      if (!response.isSuccess) {
        throw new Error(response.message || 'Failed to delete comment.');
      }

      await refetch();
      showToast('Comment deleted successfully.', 'success');
    } catch (err) {
      const error = err instanceof Error ? err : new Error('Failed to delete comment.');
      showToast(error.message, 'error');
      throw error;
    }
  };

  if (menuLoading || loading) {
    return <LoadingState className="blog-post-loading" minHeight={200} />;
  }

  if (menuError) {
    return <StatusMessage>{menuError}</StatusMessage>;
  }

  if (blogPageId == null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the blog page context. Please refresh the page and try again.
      </StatusMessage>
    );
  }

  if (postErrorMessage || !post) {
    return <StatusMessage>{postErrorMessage || 'Post not found'}</StatusMessage>;
  }

  const comments = post.comments ?? [];
  const commentCount = post.commentCount ?? comments.length;

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
                {commentCount} {commentCount === 1 ? 'Comment' : 'Comments'}
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

      <BlogCommentsList
        comments={comments}
        commentCount={commentCount}
        formatDate={formatDate}
        onEditComment={handleCommentEdit}
        onDeleteComment={handleCommentDelete}
        currentUserId={user?.id}
      />

      <BlogCommentFormPanel
        menuLoading={menuLoading}
        menuError={menuError}
        blogPageId={blogPageId}
        commentFormKey={commentFormKey}
        initialData={commentFormInitialData}
        onSubmit={handleCommentSubmit}
        isSubmitting={commentMutation.isPending}
        error={commentError}
        successMessage={commentSuccess}
        isAuthenticated={isAuthenticated}
        onRequireAuth={redirectToLogin}
        showIdentityFields
        identityReadOnly={isAuthenticated}
      />

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
          {isAuthenticated && user?.email && post.author &&
            post.author.toLowerCase() === user.email.toLowerCase() && (
              <>
                <Button
                  variant="contained"
                  startIcon={<Edit />}
                  onClick={() => navigate(`/blog/edit/${id}`)}
                  size="large"
                >
                  Edit Post
                </Button>
                <Button
                  variant="outlined"
                  color="error"
                  startIcon={<Delete />}
                  onClick={handleDeleteClick}
                  size="large"
                >
                  Delete
                </Button>
              </>
            )}
          <Button variant="outlined" startIcon={<Share />} size="large">
            Share
          </Button>
        </Box>
      </Box>

      <Dialog
        open={deleteDialogOpen}
        onClose={handleDeleteCancel}
        aria-labelledby="delete-dialog-title"
        aria-describedby="delete-dialog-description"
      >
        <DialogTitle id="delete-dialog-title">
          Delete Blog Post?
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="delete-dialog-description">
            Are you sure you want to delete "{post?.title}"? This action cannot be undone.
          </DialogContentText>
          {deleteError && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {deleteError}
            </Alert>
          )}
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDeleteCancel} disabled={deleteMutation.isPending}>
            Cancel
          </Button>
          <Button 
            onClick={handleDeleteConfirm} 
            color="error" 
            variant="contained"
            disabled={deleteMutation.isPending}
            autoFocus
          >
            {deleteMutation.isPending ? 'Deleting...' : 'Delete'}
          </Button>
        </DialogActions>
      </Dialog>

      <Snackbar
        key={toastState?.key}
        open={Boolean(toastState)}
        autoHideDuration={5000}
        onClose={handleToastClose}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        {toastState ? (
          <Alert
            onClose={handleToastClose}
            severity={toastState.severity}
            variant="filled"
            sx={{ width: '100%' }}
          >
            {toastState.message}
          </Alert>
        ) : undefined}
      </Snackbar>
    </>
  );
};
