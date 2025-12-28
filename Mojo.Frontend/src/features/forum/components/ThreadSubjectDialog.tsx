import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  Typography,
  Alert,
} from '@mui/material';
import type { FormEvent } from 'react';
import './ThreadSubjectDialog.css';

interface ThreadSubjectDialogProps {
  open: boolean;
  title: string;
  helperText?: string;
  subject: string;
  onSubjectChange: (value: string) => void;
  onClose: () => void;
  onSubmit: (event: FormEvent<HTMLFormElement>) => void;
  isSubmitting?: boolean;
  error?: string | null;
  submitLabel?: string;
  cancelLabel?: string;
}

export const ThreadSubjectDialog = ({
  open,
  title,
  helperText,
  subject,
  onSubjectChange,
  onClose,
  onSubmit,
  isSubmitting = false,
  error = null,
  submitLabel = 'Save',
  cancelLabel = 'Cancel',
}: ThreadSubjectDialogProps) => (
  <Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
    <form onSubmit={onSubmit} className="thread-subject-dialog-form">
      <DialogTitle>{title}</DialogTitle>
      <DialogContent className="thread-subject-dialog-content">
        {helperText && (
          <Typography variant="body2" color="text.secondary" className="thread-subject-dialog-helper">
            {helperText}
          </Typography>
        )}
        <TextField
          label="Thread subject"
          value={subject}
          onChange={(event) => onSubjectChange(event.target.value)}
          autoFocus
          fullWidth
          disabled={isSubmitting}
          placeholder="What would you like to discuss?"
        />
        {error && (
          <Alert severity="error" className="thread-subject-dialog-error">
            {error}
          </Alert>
        )}
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose} disabled={isSubmitting}>
          {cancelLabel}
        </Button>
        <Button type="submit" variant="contained" disabled={isSubmitting}>
          {isSubmitting ? 'Saving…' : submitLabel}
        </Button>
      </DialogActions>
    </form>
  </Dialog>
);
