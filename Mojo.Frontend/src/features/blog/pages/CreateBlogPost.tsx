import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Container, Box, Typography } from '@mui/material';
import { BlogPostForm } from '../components/BlogPostForm';
import { useBlogCategoriesQuery } from '../hooks/useBlogCategoriesQuery';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import type { CreatePostRequest } from '../types/blog.types';
import { useCreateBlogPostMutation } from '../hooks/useCreateBlogPostMutation';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useAuth } from '@features/auth/providers/AuthProvider';

export const CreateBlogPost = () => {
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);
  const { blogPageId, blogPageUrl, menuLoading, menuError } = useBlogPageContext();
  const { user } = useAuth();
  const {
    data: categories = [],
    isLoading: loadingCategories,
    error: categoriesError,
  } = useBlogCategoriesQuery(blogPageId);
  const createPostMutation = useCreateBlogPostMutation();

  const handleSubmit = async (data: {
    title: string;
    subTitle: string;
    content: string;
    categories: { id: number; categoryName: string }[];
  }) => {
    if (blogPageId === null) {
      setError('Could not find blog page ID from menu. Please try again.');
      return;
    }

    if (!user?.email) {
      setError('Unable to determine your account email. Please sign out and back in.');
      return;
    }

    setError(null);

    try {
      const request: CreatePostRequest = {
        ...data,
        pageId: blogPageId,
      };
      const response = await createPostMutation.mutateAsync(request);
      
      if (response.isSuccess) {
        const pageUrlQuery = blogPageUrl ? `?pageUrl=${encodeURIComponent(blogPageUrl)}` : '';
        navigate(`/blog/post/${response.blogPostId}${pageUrlQuery}`);
      } else {
        setError(response.message || 'Failed to create post');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred while creating the post');
    }
  };

  const handleCancel = () => {
    navigate(blogPageUrl ?? '/blog');
  };

  if (menuLoading || loadingCategories) {
    return <LoadingState minHeight={400} />;
  }

  if (menuError || categoriesError) {
    const blockingError = menuError
      ? `Failed to load menu: ${menuError}`
      : `Failed to load categories: ${categoriesError?.message || 'Unknown error'}`;
    return <StatusMessage>{blockingError}</StatusMessage>;
  }

  if (blogPageId === null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the blog page context. Please refresh the page or contact an administrator.
      </StatusMessage>
    );
  }

  return (
    <Container maxWidth="lg">
      <Box sx={{ py: 4 }}>
        <Typography variant="body2" color="text.secondary" sx={{ mb: 3 }}>
          Home / Blog / Create New Post
        </Typography>
        
        <BlogPostForm
          authorEmail={user?.email ?? ''}
          onSubmit={handleSubmit}
          onCancel={handleCancel}
          isLoading={createPostMutation.isPending}
          error={error}
          existingCategories={categories}
          loadingCategories={loadingCategories}
        />
      </Box>
    </Container>
  );
};
