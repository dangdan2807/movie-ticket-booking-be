import { Route, Routes } from 'react-router-dom';

import { Home } from './../components/User/Home/Home';
import Register from './../components/User/Register/Register';
import Login from './../components/User/Login/Login';
import AdminLogin from './../components/Admin/Login/Login';
import RedirectPage from './../components/Redirect/RedirectPage';
import ComingSoon from '../components/ComingSoon/ComingSoon';

import HomeDashboard from '../components/User/Dashboard/Home/HomeDashboard';
import LinksDashboard from '../components/User/Dashboard/Links/LinksDashboard';
import CreateLink from '../components/User/Dashboard/Links/Create/CreateLink';
import SettingsDashboard from '../components/User/Dashboard/Settings/SettingsDashboard';

import HomeAdmin from '../components/Admin/Home/Home';
import UsersAdmin from '../components/Admin/Users/Users';
import LinksAdmin from '../components/Admin/Links/Links';
import SettingsAdmin from '../components/Admin/Settings/Settings';

import UserRoutes from './UserRoutes';
import AdminRoutes from './AdminRoutes';

const AppRoutes = () => {
  return (
    <>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgot-password" element={<ComingSoon />} />
        <Route path="/admin/login" element={<AdminLogin />} />
        
        <Route
          path="/dashboard"
          element={
            <UserRoutes>
              <HomeDashboard />
            </UserRoutes>
          }
        />
        <Route
          path="/links"
          element={
            <UserRoutes>
              <LinksDashboard />
            </UserRoutes>
          }
        />
        <Route
          path="/links/create"
          element={
            <UserRoutes>
              <CreateLink />
            </UserRoutes>
          }
        />
        <Route
          path="/link-in-bio"
          element={
            <UserRoutes>
              <ComingSoon />
            </UserRoutes>
          }
        />
        <Route
          path="/qr-codes"
          element={
            <UserRoutes>
              <ComingSoon />
            </UserRoutes>
          }
        />
        <Route
          path="/settings"
          element={
            <UserRoutes>
              <SettingsDashboard />
            </UserRoutes>
          }
        />

        <Route
          path="/admin"
          element={
            <AdminRoutes>
              <HomeAdmin />
            </AdminRoutes>
          }
        />
        <Route
          path="/admin/users"
          element={
            <AdminRoutes>
              <UsersAdmin />
            </AdminRoutes>
          }
        />
        <Route
          path="/admin/links"
          element={
            <AdminRoutes>
              <LinksAdmin />
            </AdminRoutes>
          }
        />
        <Route
          path="/admin/qr-codes"
          element={
            <AdminRoutes>
              <ComingSoon />
            </AdminRoutes>
          }
        />
        <Route
          path="/admin/link-in-bio"
          element={
            <AdminRoutes>
              <ComingSoon />
            </AdminRoutes>
          }
        />
        <Route
          path="/admin/settings"
          element={
            <AdminRoutes>
              <SettingsAdmin />
            </AdminRoutes>
          }
        />

        <Route path="/" element={<Home />} />
        <Route path="/:slug" element={<RedirectPage />} />
      </Routes>
    </>
  );
};

export default AppRoutes;
