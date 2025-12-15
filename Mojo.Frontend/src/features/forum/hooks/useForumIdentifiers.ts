import { useMemo } from 'react';
import { useLocation, useParams, useSearchParams } from 'react-router-dom';

type ForumRouteParams = {
  forumId?: string;
  threadId?: string;
  '*': string;
};

const parseNumeric = (value?: string | null): number | null => {
  if (!value) {
    return null;
  }

  const parsed = Number.parseInt(value, 10);
  return Number.isNaN(parsed) ? null : parsed;
};

const extractIdAfterKeyword = (segments: string[], keywords: string[]): number | null => {
  for (let i = 0; i < segments.length - 1; i += 1) {
    const current = segments[i]?.toLowerCase();
    if (keywords.includes(current)) {
      const candidate = parseNumeric(segments[i + 1]);
      if (candidate != null) {
        return candidate;
      }
    }
  }

  return null;
};

export const useForumIdentifiers = () => {
  const params = useParams<ForumRouteParams>();
  const [searchParams] = useSearchParams();
  const location = useLocation();

  const wildcardSegments = useMemo(() => {
    const wildcardPath = params['*'];
    if (!wildcardPath) {
      return [] as string[];
    }

    return wildcardPath
      .split('/')
      .map((segment) => segment.trim())
      .filter(Boolean);
  }, [params]);

  const forumId =
    parseNumeric(params.forumId) ??
    parseNumeric(searchParams.get('forumId')) ??
    extractIdAfterKeyword(wildcardSegments, ['forum', 'forums']);

  const threadId =
    parseNumeric(params.threadId) ??
    parseNumeric(searchParams.get('threadId')) ??
    extractIdAfterKeyword(wildcardSegments, ['thread', 'threads']);

  const currentPath = location.pathname;

  return {
    forumId: forumId ?? null,
    threadId: threadId ?? null,
    currentPath,
  } as const;
};
