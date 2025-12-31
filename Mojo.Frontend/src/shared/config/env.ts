const DEFAULT_API_BASE_URL = 'https://localhost:7001/api';

const stripTrailingSlashes = (value: string) => value.replace(/\/+$/, '');
const stripApiSuffix = (value: string) => value.replace(/\/api\/?$/, '');
const normalizePath = (path: string) => (path.startsWith('/') ? path : `/${path}`);

export const getApiBaseUrl = () => import.meta.env.VITE_API_BASE_URL || DEFAULT_API_BASE_URL;

export const getServiceBaseUrl = () => stripTrailingSlashes(stripApiSuffix(getApiBaseUrl()));

export const buildApiUrl = (path: string) => {
  const baseUrl = stripTrailingSlashes(getApiBaseUrl());
  const normalizedPath = normalizePath(path);
  return `${baseUrl}${normalizedPath}`;
};

export const buildHubUrl = (path: string) => {
  const baseUrl = stripTrailingSlashes(getServiceBaseUrl());
  const normalizedPath = normalizePath(path);
  return `${baseUrl}${normalizedPath}`;
};
