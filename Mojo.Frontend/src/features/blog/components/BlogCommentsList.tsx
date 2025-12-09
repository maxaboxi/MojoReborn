import { useState } from 'react';
import {
  Card,
  CardContent,
  Box,
  Typography,
  Alert,
  Stack,
  IconButton,
  Tooltip,
  Divider,
} from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import type { BlogComment } from '../types/blog.types';
import { CommentForm } from './CommentForm';
import type { CommentFormValues } from './CommentForm';

interface BlogCommentsListProps {
  comments: BlogComment[];
  commentCount: number;
  formatDate: (isoDate: string) => string;
  onEditComment: (commentId: string, values: CommentFormValues) => Promise<void>;
  onDeleteComment: (commentId: string) => Promise<void>;
  currentUserId?: string | null;
}

export const BlogCommentsList = ({
  comments,
  commentCount,
  formatDate,
  onEditComment,
  onDeleteComment,
  currentUserId,
}: BlogCommentsListProps) => {
  const [editingCommentId, setEditingCommentId] = useState<string | null>(null);
  const [submittingEditId, setSubmittingEditId] = useState<string | null>(null);
  const [deletingCommentId, setDeletingCommentId] = useState<string | null>(null);
  const [editError, setEditError] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const handleStartEdit = (commentId: string) => {
    setEditError(null);
    setDeleteError(null);
    setEditingCommentId((current) => (current === commentId ? null : commentId));
  };

  const handleEditSubmit = async (commentId: string, values: CommentFormValues) => {
    try {
      setSubmittingEditId(commentId);
      setEditError(null);
      await onEditComment(commentId, values);
      setEditingCommentId(null);
    } catch (error) {
      setEditError(error instanceof Error ? error.message : 'Failed to update comment.');
    } finally {
      setSubmittingEditId(null);
    }
  };

  const handleDelete = async (commentId: string) => {
    const confirmed = window.confirm('Delete this comment? This cannot be undone.');
    if (!confirmed) {
      return;
    }

    try {
      setDeletingCommentId(commentId);
      setDeleteError(null);
      await onDeleteComment(commentId);
    } catch (error) {
      setDeleteError(error instanceof Error ? error.message : 'Failed to delete comment.');
    } finally {
      setDeletingCommentId(null);
    }
  };

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

        {deleteError && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {deleteError}
          </Alert>
        )}

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
                <Typography variant="body1">{comment.content}</Typography>

                {currentUserId && comment.userGuid &&
                  comment.userGuid.toLowerCase() === currentUserId.toLowerCase() && (
                    <Box className="blog-comment-actions">
                      <Tooltip title="Edit comment">
                        <IconButton size="small" onClick={() => handleStartEdit(comment.id)}>
                          <EditIcon fontSize="inherit" />
                        </IconButton>
                      </Tooltip>
                      <Tooltip title="Delete comment">
                        <span>
                          <IconButton
                            size="small"
                            onClick={() => handleDelete(comment.id)}
                            disabled={deletingCommentId === comment.id}
                          >
                            <DeleteIcon fontSize="inherit" />
                          </IconButton>
                        </span>
                      </Tooltip>
                    </Box>
                  )}

                {editingCommentId === comment.id && currentUserId && comment.userGuid &&
                  comment.userGuid.toLowerCase() === currentUserId.toLowerCase() && (
                  <Box className="blog-comment-edit" mt={2}>
                    <Divider sx={{ mb: 2 }} />
                    {editError && (
                      <Alert severity="error" sx={{ mb: 2 }}>
                        {editError}
                      </Alert>
                    )}
                    <CommentForm
                      initialData={{
                        title: comment.title ?? '',
                        content: comment.content ?? '',
                      }}
                      onSubmit={(values) => handleEditSubmit(comment.id, values)}
                      onCancel={() => setEditingCommentId(null)}
                      submitLabel="Save Changes"
                      cancelLabel="Close"
                      showIdentityFields={false}
                      isSubmitting={submittingEditId === comment.id}
                    />
                  </Box>
                )}
              </Box>
            ))}
          </Stack>
        )}
      </CardContent>
    </Card>
  );
};
