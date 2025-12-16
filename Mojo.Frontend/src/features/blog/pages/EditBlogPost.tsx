import { useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Container, Box, Typography } from '@mui/material';
import { BlogPostForm } from '../components/BlogPostForm';
import { useBlogPostQuery } from '../hooks/useBlogPostQuery';
import { useBlogCategoriesQuery } from '../hooks/useBlogCategoriesQuery';
import { useUpdateBlogPostMutation } from '../hooks/useUpdateBlogPostMutation';
import type { EditPostRequest, Category } from '../types/blog.types';
import { LoadingState, StatusMessage } from '@shared/ui';
import { useBlogPageContext } from '../hooks/useBlogPageContext';
import { useAuth } from '@features/auth/providers/useAuth';

export const EditBlogPost = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const { blogPageId, blogPageUrl, menuLoading, menuError } = useBlogPageContext();
  const pageUrlQuery = blogPageUrl ? `?pageUrl=${encodeURIComponent(blogPageUrl)}` : '';
  const { user } = useAuth();
  const {
    data: post,
    isLoading: loadingPost,
    error: loadError,
  } = useBlogPostQuery(id, blogPageId);
  const {
    data: existingCategories = [],
    isLoading: loadingCategories,
    error: categoriesError,
  } = useBlogCategoriesQuery(blogPageId);
  const updatePostMutation = useUpdateBlogPostMutation();
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (data: {
    title: string;
    subTitle: string;
    content: string;
    categories: Category[];
  }) => {
    if (!id) return;

    if (blogPageId === null) {
      setError('Could not determine blog page context. Please refresh and try again.');
      return;
    }

    setError(null);

    try {
      const request: EditPostRequest = {
        pageId: blogPageId,
        blogPostId: id,
        title: data.title,
        subTitle: data.subTitle,
        content: data.content,
        categories: data.categories,
      };

      const response = await updatePostMutation.mutateAsync(request);
      
      if (response.isSuccess) {
        // Navigate back to the post
        navigate(`/blog/post/${id}${pageUrlQuery}`);
      } else {
        setError(response.message || 'Failed to update post');
      }
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred while updating the post');
    }
  };

  const handleCancel = () => {
    navigate(`/blog/post/${id}${pageUrlQuery}`);
  };

  if (menuLoading || loadingPost || loadingCategories) {
    return <LoadingState minHeight={400} />;
  }

  if (menuError) {
    return <StatusMessage>{menuError}</StatusMessage>;
  }

  if (blogPageId === null) {
    return (
      <StatusMessage severity="warning">
        Unable to determine the blog page context. Please refresh the page or contact an administrator.
      </StatusMessage>
    );
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
            content: post.content,
            categories: categoriesAsObjects,
          }}
          authorEmail={user?.email ?? post.author}
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
