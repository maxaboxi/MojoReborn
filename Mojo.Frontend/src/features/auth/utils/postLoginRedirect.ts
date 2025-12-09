const STORAGE_KEY = 'mojo:postLoginRedirect';

export const savePostLoginRedirect = (path: string) => {
  if (!path) return;
  sessionStorage.setItem(STORAGE_KEY, path);
};

export const consumePostLoginRedirect = () => {
  const value = sessionStorage.getItem(STORAGE_KEY);
  if (value) {
    sessionStorage.removeItem(STORAGE_KEY);
  }
  return value;
};
