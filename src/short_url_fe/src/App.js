import React from 'react';
import { ToastContainer } from 'react-toastify';

import AppRoutes from './Routes/AppRoutes';
import { Layout } from './components/Layout';
import './index.scss';
import './App.scss';

export default function App() {
  return (
    <Layout>
      <AppRoutes />
      <ToastContainer
        position="top-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </Layout>
  );
}
