import axios from './CustomAxios';

import { getTokenFromLocalStorage } from '../lib/common';

export const statisticsBase = () => {
  const token = getTokenFromLocalStorage();

  return axios.get('/v1/statistics/base', {
    headers: {
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    },
  });
};
