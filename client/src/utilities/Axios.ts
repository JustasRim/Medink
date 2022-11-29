import axios from 'axios';

const getToken = () =>
  document.cookie.match('(^|;)\\s*token\\s*=\\s*([^;]+)')?.pop() || '';

const ax = axios.create({
  baseURL: 'https://localhost:7222/api/v1',
});

ax.interceptors.request.use((config) => {
  if (!config?.headers) {
    throw new Error(
      `Expected 'config' and 'config.headers' not to be undefined`
    );
  }

  const token = getToken();
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default ax;
