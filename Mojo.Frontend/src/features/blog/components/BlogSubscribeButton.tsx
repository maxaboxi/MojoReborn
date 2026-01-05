import { useState, useCallback } from 'react';
import { Button, Snackbar, Alert } from '@mui/material';
import type { AlertColor } from '@mui/material';
import { Notifications, NotificationsOff } from '@mui/icons-material';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '@features/auth/providers/useAuth';
import { savePostLoginRedirect } from '@features/auth/utils/postLoginRedirect';
import { useSubscribeToBlogMutation } from '../hooks/useSubscribeToBlogMutation';
import { useUnsubscribeFromBlogMutation } from '../hooks/useUnsubscribeFromBlogMutation';
import { useBlogSubscriptionsQuery } from '../hooks/useBlogSubscriptionsQuery';

type BlogSubscribeButtonProps = {
  pageId: number;
  moduleGuid: string | null;
};

export const BlogSubscribeButton = ({ pageId, moduleGuid }: BlogSubscribeButtonProps) => {
  const navigate = useNavigate();
  const location = useLocation();
  const { isAuthenticated } = useAuth();
  const { data: subscriptionsData } = useBlogSubscriptionsQuery();
  const subscribeMutation = useSubscribeToBlogMutation();
  const unsubscribeMutation = useUnsubscribeFromBlogMutation();
  
  const [toast, setToast] = useState<{ message: string; severity: AlertColor } | null>(null);

  // Find the subscription for this blog module
  const subscriptions = subscriptionsData?.subscriptions;
  const subscription = moduleGuid && subscriptions
    ? subscriptions.find((sub) => sub.moduleGuid === moduleGuid)
    : undefined;

  const isSubscribed = Boolean(subscription);
  const isLoading = subscribeMutation.isPending || unsubscribeMutation.isPending;

  const showToast = useCallback((message: string, severity: AlertColor = 'info') => {
    setToast({ message, severity });
  }, []);

  const handleCloseToast = useCallback(() => {
    setToast(null);
  }, []);

  const redirectToLogin = useCallback(() => {
    const target = `${location.pathname}${location.search}`;
    savePostLoginRedirect(target);
    navigate(`/auth/login?redirect=${encodeURIComponent(target)}`);
  }, [location.pathname, location.search, navigate]);

  const handleClick = useCallback(async () => {
    if (!isAuthenticated) {
      redirectToLogin();
      return;
    }

    if (!moduleGuid) {
      showToast('Unable to determine blog module context.', 'error');
      return;
    }

    try {
      if (isSubscribed && subscription) {
        await unsubscribeMutation.mutateAsync({
          pageId,
          subscriptionId: subscription.id,
        });
        showToast('You have unsubscribed from this blog.', 'success');
      } else {
        await subscribeMutation.mutateAsync({ pageId });
        showToast('You are now subscribed to this blog!', 'success');
      }
    } catch (error) {
      const message = error instanceof Error ? error.message : 'An error occurred.';
      showToast(message, 'error');
    }
  }, [
    isAuthenticated,
    isSubscribed,
    moduleGuid,
    pageId,
    redirectToLogin,
    showToast,
    subscribeMutation,
    subscription,
    unsubscribeMutation,
  ]);

  return (
    <>
      <Button
        variant={isSubscribed ? 'outlined' : 'contained'}
        color={isSubscribed ? 'secondary' : 'primary'}
        startIcon={isSubscribed ? <NotificationsOff /> : <Notifications />}
        onClick={handleClick}
        disabled={isLoading}
        size="large"
      >
        {isLoading ? 'Loading...' : isSubscribed ? 'Unsubscribe' : 'Subscribe'}
      </Button>

      <Snackbar
        open={toast !== null}
        autoHideDuration={5000}
        onClose={handleCloseToast}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert onClose={handleCloseToast} severity={toast?.severity ?? 'info'} variant="filled">
          {toast?.message}
        </Alert>
      </Snackbar>
    </>
  );
};
