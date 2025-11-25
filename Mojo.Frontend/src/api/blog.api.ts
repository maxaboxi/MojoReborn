import apiClient from './axios.client';
import { type BlogPost } from '../types/blog.types';

export const blogApi = {
  getPosts: async (): Promise<BlogPost[]> => {
    const response = await apiClient.get<BlogPost[]>('/blog/posts');
    return response.data;
  },

  getPost: async (id: string): Promise<BlogPost> => {
    const response = await apiClient.get<BlogPost>(`/blog/posts/${id}`);
    return response.data;
  },

  createPost: async (post: Omit<BlogPost, 'blogPostGuid' | 'createdAt' | 'commentCount' | 'comments'>): Promise<BlogPost> => {
    const response = await apiClient.post<BlogPost>('/blog', post);
    return response.data;
  },

  updatePost: async (id: string, post: Partial<BlogPost>): Promise<BlogPost> => {
    const response = await apiClient.put<BlogPost>(`/blog/${id}`, post);
    return response.data;
  },

  deletePost: async (id: string): Promise<void> => {
    await apiClient.delete(`/blog/${id}`);
  },
};
