import { useInfiniteQuery, type InfiniteData } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import type { GetPostsResponse } from '../types/blog.types';
import { blogQueryKeys } from './blogQueryKeys';
import { BLOG_POSTS_PAGE_SIZE } from '../constants';

type BlogPostsCursor = {
  lastPostDate: string;
  lastPostId: number;
};

type BlogPostsQueryKey = ReturnType<typeof blogQueryKeys.posts>;

export const useBlogPostsQuery = (pageId?: number | null) =>
  useInfiniteQuery<
    GetPostsResponse,
    Error,
    InfiniteData<GetPostsResponse>,
    BlogPostsQueryKey,
    BlogPostsCursor | null
  >({
    queryKey: blogQueryKeys.posts(pageId),
    enabled: typeof pageId === 'number',
    initialPageParam: null,
    queryFn: ({ pageParam }) =>
      blogApi.getPosts({
        pageId: pageId as number,
        amount: BLOG_POSTS_PAGE_SIZE,
        lastPostDate: pageParam?.lastPostDate,
        lastPostId: pageParam?.lastPostId,
      }),
    getNextPageParam: (lastPage) => {
      const posts = lastPage.blogPosts;

      if (!posts || posts.length < BLOG_POSTS_PAGE_SIZE) {
        return null;
      }

      const lastPost = posts[posts.length - 1];

      if (!lastPost) {
        return null;
      }

      return {
        lastPostDate: lastPost.createdAt,
        lastPostId: lastPost.id,
      } satisfies BlogPostsCursor;
    },
    staleTime: 30 * 1000,
  });
