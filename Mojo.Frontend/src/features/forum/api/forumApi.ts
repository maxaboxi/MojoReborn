import apiClient from '@shared/api/axiosClient';
import type {
  GetThreadsResponseDto,
  GetThreadsRequest,
  GetThreadRequest,
  GetThreadResponseDto,
  CreateThreadRequest,
  CreateThreadResponse,
  EditThreadRequest,
  EditThreadResponse,
  CreateForumPostRequest,
  CreateForumPostResponse,
  EditForumPostRequest,
  EditForumPostResponse,
} from '../types/forum.types';

export const forumApi = {
  getThreads: async ({
    pageId,
    amount = 20,
    lastThreadSequence,
  }: GetThreadsRequest): Promise<GetThreadsResponseDto> => {
    const response = await apiClient.get<GetThreadsResponseDto>(`/${pageId}/forums/threads`, {
      params: {
        amount,
        lastThreadSequence,
      },
    });
    return response.data;
  },
  getThread: async ({
    pageId,
    forumId,
    threadId,
    amount = 100,
    lastThreadSequence = 0,
  }: GetThreadRequest): Promise<GetThreadResponseDto> => {
    const response = await apiClient.get<GetThreadResponseDto>(
      `/${pageId}/forums/${forumId}/threads/${threadId}`,
      {
        params: {
          amount,
          lastThreadSequence,
        },
      }
    );
    return response.data;
  },
  createThread: async (request: CreateThreadRequest): Promise<CreateThreadResponse> => {
    const response = await apiClient.post<CreateThreadResponse>('/forums/threads', request);
    return response.data;
  },
  editThread: async (request: EditThreadRequest): Promise<EditThreadResponse> => {
    const response = await apiClient.put<EditThreadResponse>('/forums/threads', request);
    return response.data;
  },
  createPost: async (request: CreateForumPostRequest): Promise<CreateForumPostResponse> => {
    const response = await apiClient.post<CreateForumPostResponse>('/forums/threads/posts', request);
    return response.data;
  },
  editPost: async (request: EditForumPostRequest): Promise<EditForumPostResponse> => {
    const response = await apiClient.put<EditForumPostResponse>('/forums/threads/posts', request);
    return response.data;
  },
};
