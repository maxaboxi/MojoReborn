import { useInfiniteQuery, type InfiniteData } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { GetThreadsResponseDto } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

type UseForumThreadsQueryArgs = {
  pageId?: number | null;
  amount?: number;
};

type ThreadCursor = {
  lastThreadDate: string;
  lastThreadId: number;
};

type ThreadsQueryKey = ReturnType<typeof forumQueryKeys.threads>;

export const useForumThreadsQuery = ({ pageId, amount = 20 }: UseForumThreadsQueryArgs) =>
  useInfiniteQuery<
    GetThreadsResponseDto,
    Error,
    InfiniteData<GetThreadsResponseDto>,
    ThreadsQueryKey,
    ThreadCursor | null
  >({
    queryKey: forumQueryKeys.threads(pageId, amount),
    enabled: typeof pageId === 'number',
    initialPageParam: null,
    queryFn: ({ pageParam }) =>
      forumApi.getThreads({
        pageId: pageId as number,
        amount,
        lastThreadDate: pageParam?.lastThreadDate,
        lastThreadId: pageParam?.lastThreadId,
      }),
    getNextPageParam: (lastPage) => {
      const threads = lastPage.threads;
      if (!threads || threads.length < amount) {
        return null;
      }

      const lastThread = threads[threads.length - 1];
      if (!lastThread?.mostRecentPostDate) {
        return null;
      }

      return {
        lastThreadDate: lastThread.mostRecentPostDate,
        lastThreadId: lastThread.id,
      } satisfies ThreadCursor;
    },
    staleTime: 30 * 1000,
  });
