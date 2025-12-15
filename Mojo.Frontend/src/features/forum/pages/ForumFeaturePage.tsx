import { useForumIdentifiers } from '../hooks/useForumIdentifiers';
import { ForumThreadsPage } from './ForumThreadsPage';
import { ForumThreadPage } from './ForumThreadPage';

export const ForumFeaturePage = () => {
  const { forumId, threadId } = useForumIdentifiers();

  if (threadId != null) {
    return <ForumThreadPage forumId={forumId} threadId={threadId} />;
  }

  return <ForumThreadsPage />;
};
