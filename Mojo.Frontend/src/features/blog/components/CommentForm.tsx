import { useEffect, useMemo, useState } from 'react';
import { Stack, TextField, Button, Alert } from '@mui/material';

export type CommentFormValues = {
  author: string;
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
  identityReadOnly?: boolean;
}

const defaultValues: CommentFormValues = {
  author: '',
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
  identityReadOnly = false,
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
    const identityRequired = showIdentityFields;
    if (!identityRequired) {
      return hasTitle && hasContent;
    }
    const hasAuthor = values.author.trim().length > 0;
    return hasTitle && hasContent && hasAuthor;
  }, [showIdentityFields, values]);

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
          <TextField
            label="Author"
            value={values.author}
            onChange={(e) => setValues((prev) => ({ ...prev, author: e.target.value }))}
            required
            disabled={identityReadOnly || isSubmitting || disabled}
            InputProps={identityReadOnly ? { readOnly: true } : undefined}
            helperText={identityReadOnly ? 'Pulled from your signed-in account' : undefined}
          />
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
