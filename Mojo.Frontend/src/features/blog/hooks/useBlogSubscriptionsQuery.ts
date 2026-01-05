import { useQuery } from '@tanstack/react-query';
import { blogApi } from '../api/blogApi';
import { blogQueryKeys } from './blogQueryKeys';
import { useAuth } from '@features/auth/providers/useAuth';
import type { GetBlogSubscriptionsResponse } from '@shared/types/subscription.types';

export const useBlogSubscriptionsQuery = () => {
  const { isAuthenticated } = useAuth();

  return useQuery<GetBlogSubscriptionsResponse, Error>({
    queryKey: blogQueryKeys.subscriptions(),
    queryFn: blogApi.getSubscriptions,
    enabled: isAuthenticated,
    staleTime: 5 * 60 * 1000, // 5 minutes
  });
};
