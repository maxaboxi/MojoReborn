import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Box, Typography, CircularProgress, Alert } from '@mui/material';
import { BlogPostForm } from '../components/BlogPostForm';
import { blogApi } from '../../../api/blog.api';
import { useBlogPost } from '../hooks/useBlogPost';
import { useCategories } from '../hooks/useCategories';
import type { EditPostRequest, Category } from '../../../types/blog.types';

export const EditBlogPost = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { post, loading: loadingPost, error: loadError } = useBlogPost(id || '');
  const { categories: existingCategories, loading: loadingCategories } = useCategories();
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (data: {
    title: string;
    subTitle: string;
    content: string;
    categories: Category[];
  }) => {
    if (!id) return;

    setIsLoading(true);
    setError(null);

    try {
      const request: EditPostRequest = {
        blogPostId: id,
        title: data.title,
        subTitle: data.subTitle,
        content: data.content,
        categories: data.categories,
      };

      const response = await blogApi.updatePost(request);
      
      if (response.isSuccess) {
        // Navigate back to the post
        navigate(`/blog/post/${id}`);
      } else {
        setError(response.message || 'Failed to update post');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred while updating the post');
    } finally {
      setIsLoading(false);
    }
  };

  const handleCancel = () => {
    navigate(`/blog/post/${id}`);
  };

  if (loadingPost || loadingCategories) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '400px' }}>
        <CircularProgress size={60} />
      </Box>
    );
  }

  if (loadError || !post) {
    return (
      <Container maxWidth="md">
        <Alert severity="error">{loadError || 'Post not found'}</Alert>
      </Container>
    );
  }

  // Convert post categories (strings) to Category objects for the form
  const categoriesAsObjects: Category[] = post.categories.map((cat) => ({
    id: 0, // New categories have id 0
    categoryName: cat,
  }));

  return (
    <Container maxWidth="lg">
      <Box sx={{ py: 4 }}>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 3 }}>
          Home / Blog / {post.title} / Edit
        </Typography>
        
        <BlogPostForm
          initialData={{
            title: post.title,
            subTitle: post.subTitle,
            author: post.author,
            content: post.content,
            categories: categoriesAsObjects,
          }}
          onSubmit={handleSubmit}
          onCancel={handleCancel}
          isEdit
          isLoading={isLoading}
          error={error}
          existingCategories={existingCategories}
          loadingCategories={loadingCategories}
        />
      </Box>
    </Container>
  );
};
