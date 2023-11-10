import axios from './CustomAxios';
import { getTokenFromLocalStorage } from './../lib/common';

export const login = (email, password) => {
  return axios.post(
    '/v1/auth/login',
    {
      email,
      password,
    },
    {
      headers: {
        'Content-Type': 'application/json',
      },
    },
  );
};

export const register = (fullName, email, password) => {
  return axios.post(
    '/v1/auth/register',
    {
      fullname: fullName,
      email,
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
