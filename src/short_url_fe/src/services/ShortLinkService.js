import axios from './CustomAxios';
import dayjs from 'dayjs';
import { getTokenFromLocalStorage } from './../lib/common';

export const getShortUrlByShortLink = (pathname) => {
  return axios.get(`/v1/short-url/short-link${pathname}`);
};

export const createShortUrl = (longUrl, shortUrl, title) => {
  const token = getTokenFromLocalStorage();
  return axios.post(
    '/v1/short-url',
    {
      title: title ?? shortUrl,
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

export const getShortLinks = (
  startDate = dayjs().startOf('day'),
  endDate = dayjs().add(1, 'day').endOf('day'),
  status = true,
  currentPage = 1,
  pageSize = 10,
  sort = 'ASC',
  keyword = '',
) => {
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
      startDate: startDate.format('YYYY-MM-DDTHH:mm:ss'),
      endDate: endDate.format('YYYY-MM-DDTHH:mm:ss'),
      status,
      keyword,
    },
  });
};

export const countShortLinks = () => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/short-url/count`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};

export const updateClickCount = (shortLink) => {
  return axios.put(
    `/v1/short-url/short-link${shortLink}/click`,
    {},
    {
      headers: {
        'Content-Type': 'application/json',
      },
    },
  );
};

export const hiddenShortUrl = (shortLink) => {
  const token = getTokenFromLocalStorage();
  return axios.put(
    `/v1/short-url/short-link/${shortLink?.shortUrl}`,
    {
      title: shortLink?.title,
      longUrl: shortLink?.longUrl,
      shortUrl: shortLink?.shortUrl,
      status: shortLink?.status,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const deleteShortUrlByShortLink = (shortLink) => {
  const token = getTokenFromLocalStorage();
  return axios.delete(`/v1/short-url/short-link/${shortLink}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};

export const updateShortLink = (shortLink) => {
  const token = getTokenFromLocalStorage();
  return axios.put(
    `/v1/short-url/short-link/${shortLink?.shortUrl}`,
    {
      title: shortLink.title,
      longUrl: shortLink.longUrl,
      shortUrl: shortLink?.shortUrl,
      status: shortLink.status,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};
