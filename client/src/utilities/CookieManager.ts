export const getCookie = (key: string) =>
  document.cookie.match(`(^|;)\\s*${key}\\s*=\\s*([^;]+)`)?.pop() || '';
