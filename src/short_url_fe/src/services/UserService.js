import axios from './CustomAxios';
import dayjs from 'dayjs';

import { getTokenFromLocalStorage } from './../lib/common';
const currentDate = dayjs();

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

export const getProfileUser = () => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/users/profile`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};

export const getProfileAdmin = () => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/users/admin/profile`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};

export const updateProfile = (fullName, email) => {
  const token = getTokenFromLocalStorage();
  return axios.put(
    `/v1/users/profile`,
    {
      fullName,
      email,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const changePassword = (
  currentPassword,
  newPassword,
  confirmPassword,
) => {
  const token = getTokenFromLocalStorage();
  return axios.put(
    `/v1/users/profile/password`,
    {
      currentPassword,
      newPassword,
      confirmPassword,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const getUsers = (
  startDate = currentDate.startOf('day'),
  endDate = currentDate.add(1, 'day').endOf('day'),
  keyword = '',
  status = true,
  currentPage = 1,
  pageSize = 10,
  sort = 'ASC',
) => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/users`, {
    params: {
      currentPage,
      pageSize,
      sort,
      startDate: startDate.format('YYYY-MM-DDTHH:mm:ss'),
      endDate: endDate.format('YYYY-MM-DDTHH:mm:ss'),
      status,
      keyword,
    },
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};

export const updateUser = (id, fullName, email, address, status, roleIds) => {
  const token = getTokenFromLocalStorage();
  return axios.put(
    `/v1/users/${id}`,
    {
      fullName,
      email,
      address,
      status,
      roleIds,
    },
    {
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token}`,
      },
    },
  );
};

export const getUserById = (id) => {
  const token = getTokenFromLocalStorage();
  return axios.get(`/v1/users/${id}`, {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};
