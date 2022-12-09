import axios from 'axios';
import { getCookie } from './CookieManager';

const ax = axios.create({
  baseURL: 'https://medink-api.azurewebsites.net/api/v1',
});

ax.interceptors.request.use((config) => {
  if (!config?.headers) {
    throw new Error(
      `Expected 'config' and 'config.headers' not to be undefined`
    );
  }

  const token = getCookie('token');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default ax;
