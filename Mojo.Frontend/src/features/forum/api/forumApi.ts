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
  DeleteThreadRequest,
  DeleteThreadResponse,
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

    if (!response.data.isSuccess) {
      throw new Error(response.data.message ?? 'Failed to load forum threads.');
    }

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

    if (!response.data.isSuccess) {
      throw new Error(response.data.message ?? 'Failed to load the forum thread.');
    }

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
  deleteThread: async ({ pageId, forumId, threadId }: DeleteThreadRequest): Promise<DeleteThreadResponse> => {
    const response = await apiClient.delete<DeleteThreadResponse>('/forums/threads', {
      params: { pageId, forumId, threadId },
    });
    return response.data;
  },
};
