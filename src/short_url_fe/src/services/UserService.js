import axios from './CustomAxios';
import { getTokenFromLocalStorage } from './../lib/common';

export const login = (phone, password) => {
  return axios.post(
    '/v1/auth/login',
    {
      phone,
      password,
    },
    {
      headers: {
        'Content-Type': 'application/json',
      },
    },
  );
};

export const logoutServer = () => {
  const token = getTokenFromLocalStorage();
  return axios.post(
    `/v1/auth/logout`,
    {},
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const getProfile = () => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/users/profile`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};
