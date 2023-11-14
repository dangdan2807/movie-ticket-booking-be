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
      <UserRoutes path="/dashboard">
        <HomeDashboard />
      </UserRoutes>
      <UserRoutes path="/links">
        <LinksDashboard />
      </UserRoutes>
      <UserRoutes path="/links/create">
        <CreateLink />
      </UserRoutes>
      <UserRoutes path="/link-in-bio">
        <ComingSoon />
      </UserRoutes>
      <UserRoutes path="/qr-codes">
        <ComingSoon />
      </UserRoutes>
      <UserRoutes path="/settings">
        <SettingsDashboard />
      </UserRoutes>

      <AdminRoutes path="/admin">
        <HomeAdmin />
      </AdminRoutes>
      <AdminRoutes path="/admin/users">
        <UsersAdmin />
      </AdminRoutes>
      <AdminRoutes path="/admin/links">
        <LinksAdmin />
      </AdminRoutes>
      <AdminRoutes path="/admin/qr-codes">
        <ComingSoon />
      </AdminRoutes>
      <AdminRoutes path="/admin/link-in-bio">
        <ComingSoon />
      </AdminRoutes>
      <AdminRoutes path="/admin/settings">
        <SettingsAdmin />
      </AdminRoutes>

      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgot-password" element={<ComingSoon />} />
        <Route path="/admin/login" element={<AdminLogin />} />
        <Route path="/" element={<Home />} />
        <Route path="/:slug" element={<RedirectPage />} />
      </Routes>
    </>
  );
};

export default AppRoutes;
