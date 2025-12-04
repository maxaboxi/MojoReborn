import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Box, Typography } from '@mui/material';
import { BlogPostForm } from '../components/BlogPostForm';
import { useBlogPostQuery } from '../hooks/useBlogPostQuery';
import { useBlogCategoriesQuery } from '../hooks/useBlogCategoriesQuery';
import { useUpdateBlogPostMutation } from '../hooks/useUpdateBlogPostMutation';
import type { EditPostRequest, Category } from '../types/blog.types';
import { LoadingState, StatusMessage } from '@shared/ui';

export const EditBlogPost = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const {
    data: post,
    isLoading: loadingPost,
    error: loadError,
  } = useBlogPostQuery(id);
  const {
    data: existingCategories = [],
    isLoading: loadingCategories,
    error: categoriesError,
  } = useBlogCategoriesQuery();
  const updatePostMutation = useUpdateBlogPostMutation();
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (data: {
    title: string;
    subTitle: string;
    content: string;
    categories: Category[];
  }) => {
    if (!id) return;

    setError(null);

    try {
      const request: EditPostRequest = {
        blogPostId: id,
        title: data.title,
        subTitle: data.subTitle,
        content: data.content,
        categories: data.categories,
      };

      const response = await updatePostMutation.mutateAsync(request);
      
      if (response.isSuccess) {
        // Navigate back to the post
        navigate(`/blog/post/${id}`);
      } else {
        setError(response.message || 'Failed to update post');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred while updating the post');
    }
  };

  const handleCancel = () => {
    navigate(`/blog/post/${id}`);
  };

  if (loadingPost || loadingCategories) {
    return <LoadingState minHeight={400} />;
  }

  if (loadError || !post) {
    return <StatusMessage>{loadError?.message || 'Post not found'}</StatusMessage>;
  }

  if (categoriesError) {
    return (
      <StatusMessage>
        {`Failed to load categories: ${categoriesError.message}`}
      </StatusMessage>
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
          isLoading={updatePostMutation.isPending}
          error={error}
          existingCategories={existingCategories}
          loadingCategories={loadingCategories}
        />
      </Box>
    </Container>
  );
};
