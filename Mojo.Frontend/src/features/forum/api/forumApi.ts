import apiClient from '@shared/api/axiosClient';
import type {
  ForumThreadSummary,
  GetThreadsResponseDto,
  GetThreadsRequest,
  GetThreadRequest,
  GetThreadResponseDto,
} from '../types/forum.types';

export const forumApi = {
  getThreads: async ({
    pageId,
    amount = 20,
    lastThreadDate,
    lastThreadId,
  }: GetThreadsRequest): Promise<ForumThreadSummary[]> => {
    const response = await apiClient.get<GetThreadsResponseDto>(
      `/${pageId}/forums/threads`,
      {
        params: {
          amount,
          lastThreadDate,
          lastThreadId,
        },
      }
    );

    if (!response.data.isSuccess) {
      throw new Error(response.data.message ?? 'Failed to load forum threads.');
    }

    return response.data.threads;
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
};
