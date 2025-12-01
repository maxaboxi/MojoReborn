import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Box, Typography, CircularProgress, Alert } from '@mui/material';
import { BlogPostForm } from '../components/BlogPostForm';
import { blogApi } from '../../../api/blog.api';
import { useMenuItems } from '../../../hooks/useMenuItems';
import { useCategories } from '../hooks/useCategories';
import type { CreatePostRequest } from '../../../types/blog.types';

export const CreateBlogPost = () => {
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const { menuItems, loading: loadingMenu, error: menuError } = useMenuItems();
  const { categories, loading: loadingCategories, error: categoriesError } = useCategories();

  // Find the blog page ID from menu items
  const findBlogPageId = (): number | null => {
    // Recursively search for the blog page
    const findInMenu = (items: typeof menuItems): number | null => {
      for (const item of items) {
        if (item.url.toLowerCase().includes('blog') || item.title.toLowerCase().includes('blog')) {
          return item.id;
        }
        if (item.children && item.children.length > 0) {
          const found = findInMenu(item.children);
          if (found !== null) return found;
        }
      }
      return null;
    };
    return findInMenu(menuItems);
  };

  const handleSubmit = async (data: Omit<CreatePostRequest, 'pageId' | 'categories'> & { categories: { id: number; categoryName: string }[] }) => {
    const pageId = findBlogPageId();
    
    if (pageId === null) {
      setError('Could not find blog page ID from menu. Please try again.');
      return;
    }

    setIsLoading(true);
    setError(null);

    try {
      const request: CreatePostRequest = {
        ...data,
        pageId,
      };
      const response = await blogApi.createPost(request);
      
      if (response.isSuccess) {
        // Navigate to the newly created post
        navigate(`/blog/post/${response.blogPostId}`);
      } else {
        setError(response.message || 'Failed to create post');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred while creating the post');
    } finally {
      setIsLoading(false);
    }
  };

  const handleCancel = () => {
    navigate('/blog');
  };

  if (loadingMenu || loadingCategories) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '400px' }}>
        <CircularProgress size={60} />
      </Box>
    );
  }

  if (menuError || categoriesError) {
    return (
      <Container maxWidth="md">
        <Alert severity="error">
          {menuError ? `Failed to load menu: ${menuError}` : `Failed to load categories: ${categoriesError}`}
        </Alert>
      </Container>
    );
  }

  return (
    <Container maxWidth="lg">
      <Box sx={{ py: 4 }}>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 3 }}>
          Home / Blog / Create New Post
        </Typography>
        
        <BlogPostForm
          onSubmit={handleSubmit}
          onCancel={handleCancel}
          isLoading={isLoading}
          error={error}
          existingCategories={categories}
          loadingCategories={loadingCategories}
        />
      </Box>
    </Container>
  );
};
