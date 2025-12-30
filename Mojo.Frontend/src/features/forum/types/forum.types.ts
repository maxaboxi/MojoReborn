export type ForumThreadSummary = {
  id: number;
  forumId: number;
  forumSequence: number;
  subject: string;
  createdAt: string;
  totalViews: number;
  totalReplies: number;
  sortOrder: number;
  isLocked: boolean;
  mostRecentPostDate?: string | null;
  mostRecentPostUserId?: number | null;
  startedByUserId: number;
  startedByUserName: string;
  threadGuid: string;
  lockedReason?: string | null;
  lockedUtc?: string | null;
};

export type ForumPost = {
  forumId: number;
  id: number;
  postGuid: string;
  threadId: number;
  threadSequence: number;
  threadSubject: string;
  content: string;
  points: number;
  userId: number;
  userName: string;
  replyToPostId?: string | null;
  createdAt: string;
};

export type GetThreadsResponseDto = {
  threads: ForumThreadSummary[];
};

export type GetThreadResponseDto = {
  id: number;
  forumId: number;
  subject: string;
  threadGuid: string;
  userId: number;
  userName: string;
  forumPosts: ForumPost[];
};

export type GetThreadsRequest = {
  pageId: number;
  amount?: number;
  lastThreadSequence?: number | null;
};

export type GetThreadRequest = {
  pageId: number;
  forumId: number;
  threadId: number;
  amount?: number;
  lastThreadSequence?: number;
};

export type ForumViewMode = 'classic' | 'nested';

export type CreateThreadRequest = {
  pageId: number;
  forumId: number;
  subject: string;
};

export type CreateThreadResponse = {
  threadId: number;
};

export type EditThreadRequest = {
  pageId: number;
  forumId: number;
  threadId: number;
  subject: string;
};

export type EditThreadResponse = void;
