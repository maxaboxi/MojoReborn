import { useEffect, useMemo, useState } from 'react';
import { Stack, TextField, Button, Alert } from '@mui/material';

export type CommentFormValues = {
  userName: string;
  userEmail: string;
  title: string;
  content: string;
};

interface CommentFormProps {
  initialData?: Partial<CommentFormValues>;
  onSubmit: (values: CommentFormValues) => Promise<void> | void;
  onCancel?: () => void;
  submitLabel?: string;
  cancelLabel?: string;
  isSubmitting?: boolean;
  error?: string | null;
  successMessage?: string | null;
  showIdentityFields?: boolean;
  disabled?: boolean;
}

const defaultValues: CommentFormValues = {
  userName: '',
  userEmail: '',
  title: '',
  content: '',
};

export const CommentForm = ({
  initialData,
  onSubmit,
  onCancel,
  submitLabel = 'Post Comment',
  cancelLabel = 'Cancel',
  isSubmitting = false,
  error = null,
  successMessage = null,
  showIdentityFields = true,
  disabled = false,
}: CommentFormProps) => {
  const [values, setValues] = useState<CommentFormValues>({
    ...defaultValues,
    ...initialData,
  });

  useEffect(() => {
    setValues({
      ...defaultValues,
      ...initialData,
    });
  }, [initialData]);

  const isFormValid = useMemo(() => {
    const hasTitle = values.title.trim().length > 0;
    const hasContent = values.content.trim().length > 0;
    if (!showIdentityFields) {
      return hasTitle && hasContent;
    }
    const hasName = values.userName.trim().length > 0;
    const hasEmail = values.userEmail.trim().length > 0;
    return hasTitle && hasContent && hasName && hasEmail;
  }, [values, showIdentityFields]);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    if (!isFormValid || disabled || isSubmitting) {
      return;
    }
    await onSubmit({ ...values });
  };

  return (
    <form onSubmit={handleSubmit}>
      <Stack spacing={2}>
        {error && <Alert severity="error">{error}</Alert>}
        {successMessage && <Alert severity="success">{successMessage}</Alert>}

        {showIdentityFields && (
          <>
            <TextField
              label="Your Name"
              value={values.userName}
              onChange={(e) => setValues((prev) => ({ ...prev, userName: e.target.value }))}
              required
              disabled={isSubmitting || disabled}
            />
            <TextField
              label="Email"
              type="email"
              value={values.userEmail}
              onChange={(e) => setValues((prev) => ({ ...prev, userEmail: e.target.value }))}
              required
              disabled={isSubmitting || disabled}
            />
          </>
        )}

        <TextField
          label="Comment Title"
          value={values.title}
          onChange={(e) => setValues((prev) => ({ ...prev, title: e.target.value }))}
          required
          disabled={isSubmitting || disabled}
        />

        <TextField
          label="Comment"
          value={values.content}
          onChange={(e) => setValues((prev) => ({ ...prev, content: e.target.value }))}
          multiline
          minRows={4}
          required
          disabled={isSubmitting || disabled}
        />

        <Stack direction="row" spacing={2} justifyContent="flex-end">
          {onCancel && (
            <Button onClick={onCancel} disabled={isSubmitting} variant="outlined">
              {cancelLabel}
            </Button>
          )}
          <Button
            type="submit"
            variant="contained"
            disabled={!isFormValid || isSubmitting || disabled}
          >
            {isSubmitting ? 'Submitting...' : submitLabel}
          </Button>
        </Stack>
      </Stack>
    </form>
  );
};
