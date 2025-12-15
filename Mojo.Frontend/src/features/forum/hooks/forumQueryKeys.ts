const FORUM_ROOT = ['forum'] as const;
const unresolved = 'unresolved';

export const forumQueryKeys = {
  all: FORUM_ROOT,
  threads: (
    pageId?: number | null,
    amount?: number
  ) => [...FORUM_ROOT, 'threads', pageId ?? unresolved, amount ?? 'auto'] as const,
  thread: (
    pageId?: number | null,
    forumId?: number | null,
    threadId?: number | null,
    amount?: number
  ) =>
    [...FORUM_ROOT, 'thread', threadId ?? unresolved, pageId ?? unresolved, forumId ?? unresolved, amount ?? 'auto'] as const,
};
