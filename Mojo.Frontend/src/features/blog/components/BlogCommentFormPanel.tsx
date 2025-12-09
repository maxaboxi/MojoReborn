import { Card, CardContent, Typography, Box, CircularProgress, Alert, Button } from '@mui/material';
import { CommentForm, type CommentFormValues } from './CommentForm';

interface BlogCommentFormPanelProps {
  menuLoading: boolean;
  menuError: string | null;
  blogPageId: number | null;
  commentFormKey: number;
  initialData: CommentFormValues;
  onSubmit: (values: CommentFormValues) => Promise<void> | void;
  isSubmitting: boolean;
  error: string | null;
  successMessage: string | null;
  isAuthenticated: boolean;
  onRequireAuth: () => void;
  showIdentityFields?: boolean;
  identityReadOnly?: boolean;
}

export const BlogCommentFormPanel = ({
  menuLoading,
  menuError,
  blogPageId,
  commentFormKey,
  initialData,
  onSubmit,
  isSubmitting,
  error,
  successMessage,
  isAuthenticated,
  onRequireAuth,
  showIdentityFields = true,
  identityReadOnly = false,
}: BlogCommentFormPanelProps) => {
  const disableForm = menuLoading || !blogPageId;

  return (
    <Card elevation={2} className="blog-comment-form-card">
      <CardContent>
        <Typography variant="h4" component="h3" gutterBottom>
          Add a Comment
        </Typography>
        {menuLoading && (
          <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
            <CircularProgress size={20} />
            <Typography variant="body2" color="text.secondary">
              Loading context...
            </Typography>
          </Box>
        )}
        {menuError && (
          <Alert severity="warning" sx={{ mb: 2 }}>
            Unable to load menu context. Comments may not be submitted until this resolves.
          </Alert>
        )}
        {!blogPageId && !menuLoading && (
          <Alert severity="warning" sx={{ mb: 2 }}>
            Unable to determine the blog page. Please refresh or try again later.
          </Alert>
        )}
        {!isAuthenticated ? (
          <Alert severity="info" sx={{ display: 'flex', flexDirection: 'column', gap: 1 }}>
            <span>Sign in to join the discussion.</span>
            <Button variant="contained" size="small" onClick={onRequireAuth} sx={{ alignSelf: 'flex-start' }}>
              Go to Sign In
            </Button>
          </Alert>
        ) : (
          <CommentForm
            key={commentFormKey}
            initialData={initialData}
            onSubmit={onSubmit}
            isSubmitting={isSubmitting}
            error={error}
            successMessage={successMessage}
            disabled={disableForm}
            showIdentityFields={showIdentityFields}
            identityReadOnly={identityReadOnly}
          />
        )}
      </CardContent>
    </Card>
  );
};
