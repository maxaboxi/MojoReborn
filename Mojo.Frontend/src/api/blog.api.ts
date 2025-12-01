import apiClient from './axios.client';
import { type BlogPost, type EditPostRequest, type EditPostResponse, type CreatePostRequest, type CreatePostResponse, type GetCategoriesResponse, type DeletePostResponse } from '../types/blog.types';

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
    const response = await apiClient.get<GetCategoriesResponse[]>('/blog/posts/categories');
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
};
