import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../providers/AuthProvider';
import { consumePostLoginRedirect } from '../utils/postLoginRedirect';

export const PostLoginRedirectListener = () => {
  const { isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (!isAuthenticated) {
      return;
    }

    const target = consumePostLoginRedirect();
    if (target) {
      navigate(target, { replace: true });
    }
  }, [isAuthenticated, navigate]);

  return null;
};
