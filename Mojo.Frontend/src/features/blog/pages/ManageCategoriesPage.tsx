import { useMemo, useState, type FormEvent } from 'react';
import { useNavigate } from 'react-router-dom';
import {
  Alert,
  Box,
  Button,
  Card,
  CardContent,
  Container,
  IconButton,
  Paper,
  Stack,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
  Tooltip,
  Typography,
} from '@mui/material';
import { Add, Close, Delete, Edit, Save } from '@mui/icons-material';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useAuth } from '@features/auth/providers/useAuth';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import { useBlogCategoriesQuery } from '../hooks/useBlogCategoriesQuery';
import { useCreateCategoryMutation } from '../hooks/useCreateCategoryMutation';
import { useEditCategoryMutation } from '../hooks/useEditCategoryMutation';
import { useDeleteCategoryMutation } from '../hooks/useDeleteCategoryMutation';
import type { CategoryDto } from '../types/blog.types';
import './ManageCategoriesPage.css';

type FeedbackState = {
  severity: 'success' | 'error' | 'info';
  message: string;
} | null;

export const ManageCategoriesPage = () => {
  const navigate = useNavigate();
  const { blogPageId, blogPageUrl, menuLoading, menuError } = useBlogPageContext();
  const { isLoading: authLoading, hasRole } = useAuth();
  const isAdmin = hasRole('admin');

  const queryPageId = isAdmin ? blogPageId : null;
  const {
    data: categories = [],
    isLoading: categoriesLoading,
    isFetching: categoriesFetching,
    error: categoriesError,
  } = useBlogCategoriesQuery(queryPageId);

  const createCategoryMutation = useCreateCategoryMutation();
  const editCategoryMutation = useEditCategoryMutation();
  const deleteCategoryMutation = useDeleteCategoryMutation();

  const [newCategoryName, setNewCategoryName] = useState('');
  const [editingCategoryId, setEditingCategoryId] = useState<number | null>(null);
  const [editingValue, setEditingValue] = useState('');
  const [pendingEditId, setPendingEditId] = useState<number | null>(null);
  const [pendingDeleteId, setPendingDeleteId] = useState<number | null>(null);
  const [feedback, setFeedback] = useState<FeedbackState>(null);

  const sortedCategories = useMemo(
    () => [...categories].sort((a, b) => a.categoryName.localeCompare(b.categoryName)),
    [categories]
  );

  const resetEditing = () => {
    setEditingCategoryId(null);
    setEditingValue('');
    setPendingEditId(null);
  };

  const handleCreateCategory = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    if (blogPageId == null) {
      setFeedback({ severity: 'error', message: 'Unable to determine the blog page context.' });
      return;
    }

    const trimmedName = newCategoryName.trim();
    if (!trimmedName) {
      setFeedback({ severity: 'error', message: 'Category name is required.' });
      return;
    }

    setFeedback(null);

    try {
      const response = await createCategoryMutation.mutateAsync({
        pageId: blogPageId,
        categoryName: trimmedName,
      });

      if (response.isSuccess) {
        setFeedback({ severity: 'success', message: 'Category created successfully.' });
        setNewCategoryName('');
      } else {
        setFeedback({ severity: 'error', message: response.message || 'Failed to create category.' });
      }
    } catch (error) {
      setFeedback({
        severity: 'error',
        message:
          error instanceof Error ? error.message : 'An unexpected error occurred while creating the category.',
      });
    }
  };

  const handleStartEdit = (category: CategoryDto) => {
    setFeedback(null);
    setEditingCategoryId(category.id);
    setEditingValue(category.categoryName);
  };

  const handleCancelEdit = () => {
    resetEditing();
  };

  const handleSaveEdit = async () => {
    if (blogPageId == null || editingCategoryId == null) {
      setFeedback({ severity: 'error', message: 'Unable to determine the blog page context.' });
      return;
    }

    const trimmedName = editingValue.trim();
    if (!trimmedName) {
      setFeedback({ severity: 'error', message: 'Category name is required.' });
      return;
    }

    setFeedback(null);
    setPendingEditId(editingCategoryId);

    try {
      const response = await editCategoryMutation.mutateAsync({
        pageId: blogPageId,
        categoryId: editingCategoryId,
        categoryName: trimmedName,
      });

      if (response.isSuccess) {
        setFeedback({ severity: 'success', message: 'Category updated successfully.' });
        resetEditing();
      } else {
        setFeedback({ severity: 'error', message: response.message || 'Failed to update category.' });
      }
    } catch (error) {
      setFeedback({
        severity: 'error',
        message:
          error instanceof Error ? error.message : 'An unexpected error occurred while updating the category.',
      });
    } finally {
      setPendingEditId(null);
    }
  };

  const handleDeleteCategory = async (category: CategoryDto) => {
    if (blogPageId == null) {
      setFeedback({ severity: 'error', message: 'Unable to determine the blog page context.' });
      return;
    }

    const confirmed = window.confirm(
      `Delete the category "${category.categoryName}"? This action cannot be undone.`
    );

    if (!confirmed) {
      return;
    }

    setFeedback(null);
    setPendingDeleteId(category.id);

    try {
      const response = await deleteCategoryMutation.mutateAsync({
        pageId: blogPageId,
        categoryId: category.id,
      });

      if (response.isSuccess) {
        setFeedback({ severity: 'success', message: 'Category deleted successfully.' });
        if (editingCategoryId === category.id) {
          resetEditing();
        }
      } else {
        setFeedback({ severity: 'error', message: response.message || 'Failed to delete category.' });
      }
    } catch (error) {
      setFeedback({
        severity: 'error',
        message:
          error instanceof Error ? error.message : 'An unexpected error occurred while deleting the category.',
      });
    } finally {
      setPendingDeleteId(null);
    }
  };

  if (menuLoading || authLoading || categoriesLoading) {
    return <LoadingState className="manage-categories-loading" message="Loading blog categories..." />;
  }

  if (menuError) {
    return <StatusMessage>{menuError}</StatusMessage>;
  }

  if (blogPageId == null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the blog page context. Please refresh or contact an administrator.
      </StatusMessage>
    );
  }

  if (!isAdmin) {
    return (
      <StatusMessage severity="warning">
        You need administrator access to manage blog categories.
      </StatusMessage>
    );
  }

  if (categoriesError) {
    return <StatusMessage>{categoriesError.message}</StatusMessage>;
  }

  return (
    <Container maxWidth="md" className="manage-categories-page">
      <Box className="manage-categories-header">
        <Box>
          <Typography variant="overline" color="text.secondary">
            Blog administration
          </Typography>
          <Typography variant="h4" component="h1" className="manage-categories-title">
            Manage Categories
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Create, rename, and remove categories that organize blog posts.
          </Typography>
          {categoriesFetching && (
            <Typography variant="caption" color="text.secondary">
              Refreshing categories...
            </Typography>
          )}
        </Box>
        <Button variant="outlined" onClick={() => navigate(blogPageUrl ?? '/blog')}>
          Back to blog
        </Button>
      </Box>

      {feedback && (
        <Alert
          severity={feedback.severity}
          onClose={() => setFeedback(null)}
          className="manage-categories-feedback"
        >
          {feedback.message}
        </Alert>
      )}

      <Card className="manage-categories-card">
        <CardContent>
          <Typography variant="h6">Create category</Typography>
          <Typography variant="body2" color="text.secondary">
            Category names appear publicly and should be descriptive and concise.
          </Typography>
          <form className="manage-categories-form" onSubmit={handleCreateCategory}>
            <TextField
              label="Category name"
              placeholder="e.g. Engineering Updates"
              value={newCategoryName}
              onChange={(event) => setNewCategoryName(event.target.value)}
              className="manage-categories-form-field"
              autoComplete="off"
              required
              disabled={createCategoryMutation.isPending}
            />
            <Button
              type="submit"
              variant="contained"
              startIcon={<Add />}
              className="manage-categories-cta"
              disabled={createCategoryMutation.isPending || !newCategoryName.trim()}
            >
              {createCategoryMutation.isPending ? 'Creating...' : 'Add category'}
            </Button>
          </form>
        </CardContent>
      </Card>

      <Card className="manage-categories-card">
        <CardContent>
          <Box className="manage-categories-list-header">
            <Typography variant="h6">Existing categories</Typography>
            <Typography variant="body2" color="text.secondary">
              {sortedCategories.length} total
            </Typography>
          </Box>
          <TableContainer component={Paper} elevation={0} className="manage-categories-table">
            <Table size="small" aria-label="blog categories">
              <TableHead>
                <TableRow>
                  <TableCell>Name</TableCell>
                  <TableCell align="right">Actions</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {sortedCategories.length === 0 ? (
                  <TableRow>
                    <TableCell colSpan={2}>
                      <Box className="manage-categories-empty">
                        <Typography variant="body1">No categories yet.</Typography>
                        <Typography variant="body2" color="text.secondary">
                          Use the form above to create your first category.
                        </Typography>
                      </Box>
                    </TableCell>
                  </TableRow>
                ) : (
                  sortedCategories.map((category) => {
                    const isEditing = editingCategoryId === category.id;
                    const isSavingThisCategory = editCategoryMutation.isPending && pendingEditId === category.id;
                    const isDeletingThisCategory =
                      deleteCategoryMutation.isPending && pendingDeleteId === category.id;

                    return (
                      <TableRow key={category.id} hover>
                        <TableCell>
                          {isEditing ? (
                            <TextField
                              value={editingValue}
                              onChange={(event) => setEditingValue(event.target.value)}
                              size="small"
                              autoFocus
                              fullWidth
                            />
                          ) : (
                            <Typography variant="body1">{category.categoryName}</Typography>
                          )}
                        </TableCell>
                        <TableCell align="right">
                          <Stack direction="row" spacing={1} justifyContent="flex-end">
                            {isEditing ? (
                              <>
                                <Tooltip title="Save">
                                  <span>
                                    <IconButton
                                      color="primary"
                                      onClick={handleSaveEdit}
                                      disabled={isSavingThisCategory}
                                      size="small"
                                    >
                                      <Save fontSize="small" />
                                    </IconButton>
                                  </span>
                                </Tooltip>
                                <Tooltip title="Cancel">
                                  <span>
                                    <IconButton
                                      onClick={handleCancelEdit}
                                      disabled={isSavingThisCategory}
                                      size="small"
                                    >
                                      <Close fontSize="small" />
                                    </IconButton>
                                  </span>
                                </Tooltip>
                              </>
                            ) : (
                              <>
                                <Tooltip title="Edit category">
                                  <span>
                                    <IconButton
                                      onClick={() => handleStartEdit(category)}
                                      disabled={isDeletingThisCategory}
                                      size="small"
                                    >
                                      <Edit fontSize="small" />
                                    </IconButton>
                                  </span>
                                </Tooltip>
                                <Tooltip title="Delete category">
                                  <span>
                                    <IconButton
                                      color="error"
                                      onClick={() => handleDeleteCategory(category)}
                                      disabled={isDeletingThisCategory}
                                      size="small"
                                    >
                                      <Delete fontSize="small" />
                                    </IconButton>
                                  </span>
                                </Tooltip>
                              </>
                            )}
                          </Stack>
                        </TableCell>
                      </TableRow>
                    );
                  })
                )}
              </TableBody>
            </Table>
          </TableContainer>
        </CardContent>
      </Card>
    </Container>
  );
};
