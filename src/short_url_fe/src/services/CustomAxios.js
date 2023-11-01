import axios from 'axios';

const instance = axios.create({
  baseURL: process.env.REACT_APP_URL_BACKEND,
  headers: {
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,PUT,POST,DELETE,PATCH,OPTIONS"
  }
});

instance.interceptors.response.use(
  function (res) {
    return res.data;
  },
  function (error) {
    return error;
    // return Promise.reject(error);
  },
);

export default instance;
