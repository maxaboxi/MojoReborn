import apiClient from '@shared/api/axiosClient';
import type {
  BlogPost,
  EditPostRequest,
  EditPostResponse,
  CreatePostRequest,
  CreatePostResponse,
  GetPostsResponse,
  GetCategoriesResponse,
  CategoryDto,
  DeletePostRequest,
  DeletePostResponse,
  CreateCommentRequest,
  CreateCommentResponse,
  EditCommentRequest,
  EditCommentResponse,
  DeleteCommentRequest,
  DeleteCommentResponse,
} from '../types/blog.types';

export const blogApi = {
  getPosts: async (pageId: number): Promise<BlogPost[]> => {
    const response = await apiClient.get<GetPostsResponse>('/blog/posts', {
      params: { pageId },
    });

    if (!response.data.isSuccess) {
      throw new Error(response.data.message || 'Failed to load blog posts.');
    }

    return response.data.blogPosts;
  },
  getPost: async (id: string, pageId: number): Promise<BlogPost> => {
    const response = await apiClient.get<BlogPost>(`/blog/posts/${id}`, {
      params: { pageId },
    });
    return response.data;
  },
  getCategories: async (pageId: number): Promise<CategoryDto[]> => {
    const response = await apiClient.get<GetCategoriesResponse>('/blog/categories', {
      params: { pageId },
    });

    if (!response.data.isSuccess) {
      throw new Error(response.data.message || 'Failed to load categories.');
    }

    return response.data.categories;
  },
  createPost: async (request: CreatePostRequest): Promise<CreatePostResponse> => {
    const response = await apiClient.post<CreatePostResponse>('/blog/posts', request);
    return response.data;
  },
  updatePost: async (request: EditPostRequest): Promise<EditPostResponse> => {
    const response = await apiClient.put<EditPostResponse>('/blog/posts', request);
    return response.data;
  },
  deletePost: async ({ pageId, blogPostId }: DeletePostRequest): Promise<DeletePostResponse> => {
    const response = await apiClient.delete<DeletePostResponse>(`/${pageId}/blog/posts/${blogPostId}`);
    return response.data;
  },
  createComment: async (request: CreateCommentRequest): Promise<CreateCommentResponse> => {
    const response = await apiClient.post<CreateCommentResponse>('/blog/posts/comment', request);
    return response.data;
  },
  editComment: async (request: EditCommentRequest): Promise<EditCommentResponse> => {
    const response = await apiClient.put<EditCommentResponse>('/blog/posts/comment', request);
    return response.data;
  },
  deleteComment: async ({ pageId, blogPostId, blogCommentId }: DeleteCommentRequest): Promise<DeleteCommentResponse> => {
    const response = await apiClient.delete<DeleteCommentResponse>(
      `/${pageId}/blog/posts/${blogPostId}/comment/${blogCommentId}`
    );
    return response.data;
  },
};
