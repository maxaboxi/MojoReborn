import { useQuery } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { ForumThreadSummary } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

type UseForumThreadsQueryArgs = {
  pageId?: number | null;
  amount?: number;
  lastThreadDate?: string | null;
  lastThreadId?: number | null;
};

export const useForumThreadsQuery = ({
  pageId,
  amount = 20,
  lastThreadDate,
  lastThreadId,
}: UseForumThreadsQueryArgs) =>
  useQuery<ForumThreadSummary[], Error>({
    queryKey: forumQueryKeys.threads(pageId, amount),
    queryFn: () =>
      forumApi.getThreads({
        pageId: pageId as number,
        amount,
        lastThreadDate,
        lastThreadId,
      }),
    enabled: typeof pageId === 'number',
    staleTime: 30 * 1000,
  });
