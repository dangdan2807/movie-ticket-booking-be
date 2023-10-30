import axios from './CustomAxios';
import { getTokenFromLocalStorage } from './../lib/common';

export const getShortUrlByShortLink = (pathname) => {
  return axios.get(`/v1/short-url/short-link${pathname}`);
};

export const createShortUrl = (longUrl, shortUrl) => {
  const token = getTokenFromLocalStorage();
  return axios.post(
    '/v1/short-url',
    {
      longUrl,
      shortUrl,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const getShortLinks = (currentPage = 1, pageSize = 10, sort = 'ASC') => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/short-url`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
    params: {
      currentPage,
      pageSize,
      sort,
    },
  });
};
