import { useParams, useNavigate } from 'react-router-dom';
import { useMemo, useState } from 'react';
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
} from '@mui/material';
import { CalendarToday, Person, Chat, ArrowBack, Share, Edit, Delete } from '@mui/icons-material';
import { useBlogPostQuery } from '../hooks/useBlogPostQuery';
import { useDeleteBlogPostMutation } from '../hooks/useDeleteBlogPostMutation';
import { useCreateCommentMutation } from '../hooks/useCreateCommentMutation';
import type { CommentFormValues } from '../components/CommentForm';
import { BlogCommentsList } from '../components/BlogCommentsList';
import { BlogCommentFormPanel } from '../components/BlogCommentFormPanel';
import { useMenuQuery } from '@shared/hooks/useMenuQuery';
import { LoadingState, StatusMessage } from '@shared/ui';
import { findBlogPageId } from '../utils/findBlogPageId';
import './BlogPostDetail.css';


export const BlogPostDetail = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const {
    data: post,
    isLoading: loading,
    error: postError,
    refetch,
  } = useBlogPostQuery(id);
  const postErrorMessage = postError?.message ?? null;
  const { menuItems, loading: menuLoading, error: menuError } = useMenuQuery();
  const blogPageId = useMemo(() => findBlogPageId(menuItems), [menuItems]);

  const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
  const [deleteError, setDeleteError] = useState<string | null>(null);
  const [commentError, setCommentError] = useState<string | null>(null);
  const [commentSuccess, setCommentSuccess] = useState<string | null>(null);
  const [commentFormKey, setCommentFormKey] = useState(0);
  const deleteMutation = useDeleteBlogPostMutation();
  const commentMutation = useCreateCommentMutation();
  const commentFormInitialData = useMemo(
    () => ({
      userName: '',
      userEmail: '',
      title: '',
      content: '',
    }),
    [commentFormKey]
  );

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

    setDeleteError(null);

    try {
      const response = await deleteMutation.mutateAsync(id);

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
      return;
    }

    if (!blogPageId) {
      setCommentError('Unable to determine blog page context. Please try again later.');
      return;
    }

    setCommentError(null);
    setCommentSuccess(null);

    try {
      const response = await commentMutation.mutateAsync({
        pageId: blogPageId,
        blogPostId: id,
        userId: '00000000-0000-0000-0000-000000000000',
        userName: values.userName,
        userEmail: values.userEmail,
        title: values.title,
        content: values.content,
      });

      if (response.isSuccess) {
        setCommentSuccess('Comment posted successfully.');
        setCommentFormKey((prev) => prev + 1);
        refetch();
      } else {
        setCommentError(response.message || 'Failed to post comment.');
      }
    } catch (err) {
      setCommentError(err instanceof Error ? err.message : 'An error occurred while posting the comment.');
    }
  };

  if (loading) {
    return <LoadingState className="blog-post-loading" minHeight={200} />;
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
    </>
  );
};
