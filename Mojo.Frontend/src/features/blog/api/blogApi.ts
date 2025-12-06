import apiClient from '@shared/api/axiosClient';
import type {
  BlogPost,
  EditPostRequest,
  EditPostResponse,
  CreatePostRequest,
  CreatePostResponse,
  GetCategoriesResponse,
  DeletePostResponse,
  CreateCommentRequest,
  CreateCommentResponse,
  EditCommentRequest,
  EditCommentResponse,
  DeleteCommentRequest,
  DeleteCommentResponse,
} from '../types/blog.types';

export const blogApi = {
  getPosts: async (): Promise<BlogPost[]> => {
    const response = await apiClient.get<BlogPost[]>('/blog/posts');
    return response.data;
  },
  getPost: async (id: string): Promise<BlogPost> => {
    const response = await apiClient.get<BlogPost>(`/blog/posts/${id}`);
    return response.data;
  },
  getCategories: async (): Promise<GetCategoriesResponse[]> => {
    const response = await apiClient.get<GetCategoriesResponse[]>('/blog/categories');
    return response.data;
  },
  createPost: async (request: CreatePostRequest): Promise<CreatePostResponse> => {
    const response = await apiClient.post<CreatePostResponse>('/blog/posts', request);
    return response.data;
  },
  updatePost: async (request: EditPostRequest): Promise<EditPostResponse> => {
    const response = await apiClient.put<EditPostResponse>('/blog/posts', request);
    return response.data;
  },
  deletePost: async (id: string): Promise<DeletePostResponse> => {
    const response = await apiClient.delete<DeletePostResponse>(`/blog/posts/${id}`);
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
  deleteComment: async ({ blogPostId, blogCommentId }: DeleteCommentRequest): Promise<DeleteCommentResponse> => {
    const response = await apiClient.delete<DeleteCommentResponse>(
      `/blog/posts/${blogPostId}/comment/${blogCommentId}`
    );
    return response.data;
  },
};
