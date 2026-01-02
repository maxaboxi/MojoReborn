import { isAxiosError } from 'axios';
import apiClient from '@shared/api/axiosClient';
import type {
  BlogComment,
  BlogPostDetail,
  BlogPostDetailDto,
  EditPostRequest,
  EditPostResponse,
  CreatePostRequest,
  CreatePostResponse,
  GetPostsResponse,
  CategoryDto,
  CategoryMutationResponse,
  CreateCategoryRequest,
  EditCategoryRequest,
  DeleteCategoryRequest,
  DeletePostRequest,
  DeletePostResponse,
  CreateCommentRequest,
  CreateCommentResponse,
  EditCommentRequest,
  EditCommentResponse,
  DeleteCommentRequest,
  DeleteCommentResponse,
} from '../types/blog.types';

type GetPostsParams = {
  pageId: number;
  amount?: number;
  lastPostDate?: string;
  lastPostId?: number;
};

type GetPostParams = {
  id: string;
  pageId: number;
  amount?: number;
  lastCommentDate?: string;
};

const mapCommentDto = (comment: BlogPostDetailDto['comments'][number]): BlogComment => ({
  id: comment.id,
  author: comment.userName,
  userGuid: comment.userGuid,
  title: comment.title,
  content: comment.content,
  createdAt: comment.createdAt,
});

export const blogApi = {
  getPosts: async ({ pageId, amount, lastPostDate, lastPostId }: GetPostsParams): Promise<GetPostsResponse> => {
    const response = await apiClient.get<GetPostsResponse>('/blog/posts', {
      params: { pageId, amount, lastPostDate, lastPostId },
    });
    return response.data;
  },
  getPost: async ({ id, pageId, amount, lastCommentDate }: GetPostParams): Promise<BlogPostDetail> => {
    try {
      const response = await apiClient.get<BlogPostDetailDto>(`/blog/posts/${id}`, {
        params: { pageId, amount, lastCommentDate },
      });

      const { comments = [], ...rest } = response.data;

      return {
        ...(rest as Omit<BlogPostDetail, 'comments'>),
        comments: comments.map(mapCommentDto),
      } satisfies BlogPostDetail;
    } catch (error) {
      if (isAxiosError(error)) {
        if (error.response?.status === 403) {
          throw new Error('You are not authorized to view this post.');
        }
        if (error.response?.status === 404) {
          throw new Error('Post not found.');
        }
      }
      throw error;
    }
  },
  getCategories: async (pageId: number): Promise<CategoryDto[]> => {
    const response = await apiClient.get<{ categories: CategoryDto[] }>('/blog/categories', {
      params: { pageId },
    });

    return response.data.categories;
  },
  createCategory: async (request: CreateCategoryRequest): Promise<CategoryMutationResponse> => {
    const response = await apiClient.post<CategoryMutationResponse>('/blog/category', request);
    return response.data;
  },
  updateCategory: async (request: EditCategoryRequest): Promise<CategoryMutationResponse> => {
    const response = await apiClient.put<CategoryMutationResponse>('/blog/category', request);
    return response.data;
  },
  deleteCategory: async (request: DeleteCategoryRequest): Promise<CategoryMutationResponse> => {
    const response = await apiClient.delete<CategoryMutationResponse>(
      `/${request.pageId}/blog/category/${request.categoryId}`
    );
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
