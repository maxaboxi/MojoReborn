import { useState, type FormEvent } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  Typography,
  Stack,
  Alert,
} from '@mui/material';

interface ForumPostEditorDialogProps {
  open: boolean;
  title: string;
  submitLabel: string;
  cancelLabel?: string;
  initialContent?: string;
  helperText?: string;
  isSubmitting?: boolean;
  error?: string | null;
  onSubmit: (content: string) => void | Promise<void>;
  onClose: () => void;
}

export const ForumPostEditorDialog = ({
  open,
  title,
  submitLabel,
  cancelLabel = 'Cancel',
  initialContent = '',
  helperText,
  isSubmitting = false,
  error,
  onSubmit,
  onClose,
}: ForumPostEditorDialogProps) => {
  const [content, setContent] = useState(initialContent);

  const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const trimmed = content.trim();
    if (!trimmed) {
      return;
    }
    await onSubmit(trimmed);
  };

  return (
    <Dialog open={open} onClose={isSubmitting ? undefined : onClose} fullWidth maxWidth="md">
      <form onSubmit={handleSubmit}>
        <DialogTitle>{title}</DialogTitle>
        <DialogContent dividers>
          <Stack spacing={2} sx={{ mt: 1 }}>
            {helperText && (
              <Typography variant="body2" color="text.secondary">
                {helperText}
              </Typography>
            )}
            <TextField
              multiline
              minRows={6}
              fullWidth
              autoFocus
              value={content}
              onChange={(event) => setContent(event.target.value)}
              placeholder="Share your reply with the community..."
              disabled={isSubmitting}
            />
            {error && <Alert severity="error">{error}</Alert>}
          </Stack>
        </DialogContent>
        <DialogActions>
          <Button onClick={onClose} disabled={isSubmitting}>
            {cancelLabel}
          </Button>
          <Button type="submit" variant="contained" disabled={isSubmitting || !content.trim()}>
            {isSubmitting ? 'Saving...' : submitLabel}
          </Button>
        </DialogActions>
      </form>
    </Dialog>
  );
};
