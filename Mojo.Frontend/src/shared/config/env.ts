const DEFAULT_API_BASE_URL = 'https://localhost:7001/api';

export const getApiBaseUrl = () => import.meta.env.VITE_API_BASE_URL || DEFAULT_API_BASE_URL;

export const buildApiUrl = (path: string) => {
  const baseUrl = getApiBaseUrl().replace(/\/$/, '');
  const normalizedPath = path.startsWith('/') ? path : `/${path}`;
  return `${baseUrl}${normalizedPath}`;
};
