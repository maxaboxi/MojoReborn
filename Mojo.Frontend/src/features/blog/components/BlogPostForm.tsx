import { useState } from 'react';
import {
  Box,
  TextField,
  Button,
  Paper,
  Typography,
  Chip,
  Stack,
  IconButton,
  Alert,
} from '@mui/material';
import { Add, Delete } from '@mui/icons-material';
import type { CreatePostCategoryDto } from '../../../types/blog.types';

interface BlogPostFormProps {
  initialData?: {
    title?: string;
    subTitle?: string;
    author?: string;
    content?: string;
    categories?: CreatePostCategoryDto[];
  };
  onSubmit: (data: {
    title: string;
    subTitle: string;
    author: string;
    content: string;
    categories: CreatePostCategoryDto[];
  }) => void;
  onCancel: () => void;
  isEdit?: boolean;
  isLoading?: boolean;
  error?: string | null;
}

export const BlogPostForm = ({
  initialData,
  onSubmit,
  onCancel,
  isEdit = false,
  isLoading = false,
  error = null,
}: BlogPostFormProps) => {
  const [title, setTitle] = useState(initialData?.title || '');
  const [subTitle, setSubTitle] = useState(initialData?.subTitle || '');
  const [author, setAuthor] = useState(initialData?.author || '');
  const [content, setContent] = useState(initialData?.content || '');
  const [categories, setCategories] = useState<CreatePostCategoryDto[]>(
    initialData?.categories || []
  );
  const [newCategoryName, setNewCategoryName] = useState('');

  const handleAddCategory = () => {
    if (newCategoryName.trim()) {
      setCategories([
        ...categories,
        { id: 0, categoryName: newCategoryName.trim() },
      ]);
      setNewCategoryName('');
    }
  };

  const handleRemoveCategory = (index: number) => {
    setCategories(categories.filter((_, i) => i !== index));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({ title, subTitle, author, content, categories });
  };

  const isFormValid = () => {
    return (
      title.trim() &&
      subTitle.trim() &&
      author.trim() &&
      content.trim() &&
      categories.length > 0
    );
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
            label="Author"
            value={author}
            onChange={(e) => setAuthor(e.target.value)}
            fullWidth
            required
            disabled={isLoading}
            inputProps={{ maxLength: 100 }}
            helperText={`${author.length}/100`}
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
            <Box sx={{ display: 'flex', gap: 1, mb: 2, flexWrap: 'wrap' }}>
              {categories.map((category, index) => (
                <Chip
                  key={index}
                  label={category.categoryName}
                  onDelete={() => handleRemoveCategory(index)}
                  deleteIcon={<Delete />}
                  color="primary"
                  disabled={isLoading}
                />
              ))}
              {categories.length === 0 && (
                <Typography variant="body2" color="text.secondary">
                  No categories added yet
                </Typography>
              )}
            </Box>
            <Box sx={{ display: 'flex', gap: 1 }}>
              <TextField
                label="Add Category"
                value={newCategoryName}
                onChange={(e) => setNewCategoryName(e.target.value)}
                onKeyPress={(e) => {
                  if (e.key === 'Enter') {
                    e.preventDefault();
                    handleAddCategory();
                  }
                }}
                size="small"
                disabled={isLoading}
                sx={{ flexGrow: 1 }}
              />
              <IconButton
                color="primary"
                onClick={handleAddCategory}
                disabled={!newCategoryName.trim() || isLoading}
              >
                <Add />
              </IconButton>
            </Box>
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
