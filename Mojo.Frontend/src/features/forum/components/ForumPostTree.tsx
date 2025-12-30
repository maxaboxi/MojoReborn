import { useMemo } from 'react';
import { Box } from '@mui/material';
import type { ForumPost } from '../types/forum.types';
import { ForumPostCard } from './ForumPostCard';

type ForumPostNode = ForumPost & { replies: ForumPostNode[] };

const buildPostTree = (posts: ForumPost[]): ForumPostNode[] => {
  const sorted = [...posts].sort((a, b) => a.threadSequence - b.threadSequence);
  const lookup = new Map<string, ForumPostNode>();
  const roots: ForumPostNode[] = [];

  sorted.forEach((post) => {
    lookup.set(post.postGuid.toLowerCase(), { ...post, replies: [] });
  });

  sorted.forEach((post) => {
    const node = lookup.get(post.postGuid.toLowerCase());
    if (!node) {
      return;
    }

    const parentKey = post.replyToPostId?.toLowerCase();
    if (parentKey) {
      const parentNode = lookup.get(parentKey);
      if (parentNode) {
        parentNode.replies.push(node);
        return;
      }
    }

    roots.push(node);
  });

  return roots;
};

type ForumNestedPostProps = {
  node: ForumPostNode;
  depth: number;
  onReply?: (post: ForumPost) => void;
  onEdit?: (post: ForumPost) => void;
  canEdit?: (post: ForumPost) => boolean;
};

const ForumNestedPost = ({ node, depth, onReply, onEdit, canEdit }: ForumNestedPostProps) => {
  const showEdit = Boolean(onEdit && canEdit?.(node));

  return (
    <Box className="forum-nested-post">
      <ForumPostCard
        post={node}
        depth={depth}
        variant="nested"
        onReply={onReply}
        onEdit={showEdit ? onEdit : undefined}
      />
      {node.replies.length > 0 && (
        <Box className="forum-nested-children">
          {node.replies.map((child) => (
            <ForumNestedPost
              key={child.postGuid}
              node={child}
              depth={depth + 1}
              onReply={onReply}
              onEdit={onEdit}
              canEdit={canEdit}
            />
          ))}
        </Box>
      )}
    </Box>
  );
};

interface ForumPostTreeProps {
  posts: ForumPost[];
  onReply?: (post: ForumPost) => void;
  onEdit?: (post: ForumPost) => void;
  canEdit?: (post: ForumPost) => boolean;
}

export const ForumPostTree = ({ posts, onReply, onEdit, canEdit }: ForumPostTreeProps) => {
  const nestedPosts = useMemo(() => buildPostTree(posts), [posts]);

  return (
    <Box className="forum-thread-nested">
      {nestedPosts.map((node) => (
        <ForumNestedPost
          key={node.postGuid}
          node={node}
          depth={0}
          onReply={onReply}
          onEdit={onEdit}
          canEdit={canEdit}
        />
      ))}
    </Box>
  );
};
