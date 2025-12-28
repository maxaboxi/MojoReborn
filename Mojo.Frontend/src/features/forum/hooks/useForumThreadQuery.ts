import { useInfiniteQuery, type InfiniteData } from '@tanstack/react-query';
import { forumApi } from '../api/forumApi';
import type { GetThreadResponseDto } from '../types/forum.types';
import { forumQueryKeys } from './forumQueryKeys';
import { THREAD_POSTS_PAGE_SIZE } from '../constants';

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
  amount = THREAD_POSTS_PAGE_SIZE,
  lastThreadSequence = 0,
}: UseForumThreadQueryArgs) =>
  useInfiniteQuery<
    GetThreadResponseDto,
    Error,
    InfiniteData<GetThreadResponseDto>,
    ReturnType<typeof forumQueryKeys.thread>,
    { lastThreadSequence: number }
  >({
    queryKey: forumQueryKeys.thread(pageId, forumId, threadId, amount),
    enabled:
      typeof pageId === 'number' &&
      typeof forumId === 'number' &&
      typeof threadId === 'number',
    initialPageParam: { lastThreadSequence },
    queryFn: ({ pageParam }) =>
      forumApi.getThread({
        pageId: pageId as number,
        forumId: forumId as number,
        threadId: threadId as number,
        amount,
        lastThreadSequence: pageParam.lastThreadSequence,
      }),
    getNextPageParam: (lastPage) => {
      const posts = lastPage.forumPosts ?? [];
      if (posts.length < amount) {
        return null;
      }
      const lastPost = posts[posts.length - 1];
      return { lastThreadSequence: lastPost.threadSequence };
    },
    staleTime: 30 * 1000,
  });
