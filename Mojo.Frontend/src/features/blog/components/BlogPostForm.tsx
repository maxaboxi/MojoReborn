import { useState, type SyntheticEvent } from 'react';
import {
  Box,
  TextField,
  Button,
  Paper,
  Typography,
  Chip,
  Stack,
  Alert,
  Autocomplete,
  CircularProgress,
} from '@mui/material';
import type { CreatePostCategoryDto, CategoryDto } from '../types/blog.types';

interface BlogPostFormProps {
  initialData?: {
    title?: string;
    subTitle?: string;
    content?: string;
    categories?: CreatePostCategoryDto[];
  };
  authorEmail?: string | null;
  onSubmit: (data: {
    title: string;
    subTitle: string;
    content: string;
    categories: CreatePostCategoryDto[];
  }) => void;
  onCancel: () => void;
  isEdit?: boolean;
  isLoading?: boolean;
  error?: string | null;
  existingCategories?: CategoryDto[];
  loadingCategories?: boolean;
}

export const BlogPostForm = ({
  initialData,
  authorEmail,
  onSubmit,
  onCancel,
  isEdit = false,
  isLoading = false,
  error = null,
  existingCategories = [],
  loadingCategories = false,
}: BlogPostFormProps) => {
  const [title, setTitle] = useState(initialData?.title || '');
  const [subTitle, setSubTitle] = useState(initialData?.subTitle || '');
  const [content, setContent] = useState(initialData?.content || '');
  const [categories, setCategories] = useState<CreatePostCategoryDto[]>(
    initialData?.categories || []
  );

  const handleCategoriesChange = (
    _event: SyntheticEvent,
    newValue: Array<CategoryDto | string>
  ) => {
    const selectedCategories: CreatePostCategoryDto[] = newValue.map((item) => {
      if (typeof item === 'string') {
        // New category typed by user
        return { id: 0, categoryName: item };
      } else {
        // Existing category selected from dropdown
        return { id: item.id, categoryName: item.categoryName };
      }
    });
    setCategories(selectedCategories);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({ title, subTitle, content, categories });
  };

  const isFormValid = () => {
    return title.trim() && subTitle.trim() && content.trim() && categories.length > 0;
  };

  return (
    <Paper elevation={3} sx={{ p: 4, maxWidth: 900, mx: 'auto' }}>
      <Typography variant="h4" component="h1" gutterBottom>
        {isEdit ? 'Edit Blog Post' : 'Create New Blog Post'}
      </Typography>

      {error && (
        <Alert severity="error" sx={{ mb: 3 }}>
          {error}
        </Alert>
      )}

      <form onSubmit={handleSubmit}>
        <Stack spacing={3}>
          <TextField
            label="Title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            fullWidth
            required
            disabled={isLoading}
            inputProps={{ maxLength: 255 }}
            helperText={`${title.length}/255`}
          />

          <TextField
            label="Subtitle"
            value={subTitle}
            onChange={(e) => setSubTitle(e.target.value)}
            fullWidth
            required
            disabled={isLoading}
            inputProps={{ maxLength: 500 }}
            helperText={`${subTitle.length}/500`}
          />

          <TextField
            label="Author Email"
            value={authorEmail ?? ''}
            fullWidth
            disabled
            helperText="Automatically populated from your signed-in account"
          />

          <TextField
            label="Content"
            value={content}
            onChange={(e) => setContent(e.target.value)}
            multiline
            rows={10}
            fullWidth
            required
            disabled={isLoading}
            helperText="Supports HTML formatting"
          />

          <Box>
            <Typography variant="h6" gutterBottom>
              Categories
            </Typography>
            <Autocomplete
              multiple
              freeSolo
              options={existingCategories}
              getOptionLabel={(option) => 
                typeof option === 'string' ? option : option.categoryName
              }
              value={categories.map(cat => {
                // Map selected categories back to the format Autocomplete expects
                const existing = existingCategories.find(ec => ec.id === cat.id && ec.categoryName === cat.categoryName);
                return existing || cat.categoryName;
              })}
              onChange={handleCategoriesChange}
              loading={loadingCategories}
              disabled={isLoading}
              renderInput={(params) => (
                <TextField
                  {...params}
                  label="Select or type categories"
                  placeholder="Start typing..."
                  helperText="Select existing categories or type to create new ones"
                  InputProps={{
                    ...params.InputProps,
                    endAdornment: (
                      <>
                        {loadingCategories ? <CircularProgress color="inherit" size={20} /> : null}
                        {params.InputProps.endAdornment}
                      </>
                    ),
                  }}
                />
              )}
              renderValue={(value, getTagProps) =>
                value.map((option, index) => {
                  const label = typeof option === 'string' ? option : option.categoryName;
                  const isNew = typeof option === 'string' || option.id === 0;
                  return (
                    <Chip
                      label={label}
                      {...getTagProps({ index })}
                      color={isNew ? 'secondary' : 'primary'}
                      variant={isNew ? 'outlined' : 'filled'}
                    />
                  );
                })
              }
              isOptionEqualToValue={(option, value) => {
                if (typeof option === 'string' && typeof value === 'string') {
                  return option === value;
                }
                if (typeof option !== 'string' && typeof value !== 'string') {
                  return option.id === value.id && option.categoryName === value.categoryName;
                }
                if (typeof option !== 'string' && typeof value === 'string') {
                  return option.categoryName === value;
                }
                return false;
              }}
            />
          </Box>

          <Box sx={{ display: 'flex', gap: 2, justifyContent: 'flex-end' }}>
            <Button
              variant="outlined"
              onClick={onCancel}
              disabled={isLoading}
              size="large"
            >
              Cancel
            </Button>
            <Button
              type="submit"
              variant="contained"
              disabled={!isFormValid() || isLoading}
              size="large"
            >
              {isLoading ? 'Saving...' : isEdit ? 'Update Post' : 'Create Post'}
            </Button>
          </Box>
        </Stack>
      </form>
    </Paper>
  );
};
