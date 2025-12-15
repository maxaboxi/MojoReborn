import { useQuery } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { GetThreadResponseDto } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';

type UseForumThreadQueryArgs = {
  pageId?: number | null;
  forumId?: number | null;
  threadId?: number | null;
  amount?: number;
  lastThreadSequence?: number;
};

export const useForumThreadQuery = ({
  pageId,
  forumId,
  threadId,
  amount = 100,
  lastThreadSequence = 0,
}: UseForumThreadQueryArgs) =>
  useQuery<GetThreadResponseDto, Error>({
    queryKey: forumQueryKeys.thread(pageId, forumId, threadId, amount),
    queryFn: () =>
      forumApi.getThread({
        pageId: pageId as number,
        forumId: forumId as number,
        threadId: threadId as number,
        amount,
        lastThreadSequence,
      }),
    enabled:
      typeof pageId === 'number' &&
      typeof forumId === 'number' &&
      typeof threadId === 'number',
  });
