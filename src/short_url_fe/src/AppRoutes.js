import { Home } from './components/User/Home/Home';
import { LayoutDashboardUser } from './components/User/Dashboard/LayoutDashboardUser';
import { LayoutDashboardAdmin } from './components/Admin/LayoutDashboardAdmin';
import HomeDashboard from './components/User/Dashboard/Home/HomeDashboard';
import LinksDashboard from './components/User/Dashboard/Links/LinksDashboard';
import CreateLink from './components/User/Dashboard/Links/Create/CreateLink';
import Login from './components/User/Login/Login';
import Register from './components/User/Register/Register';
import SettingsDashboard from './components/User/Dashboard/Settings/SettingsDashboard';

import LoginAdmin from './components/Admin/Login/Login';
import HomeAdmin from './components/Admin/Home/Home';
import UsersAdmin from './components/Admin/Users/Users';
import LinksAdmin from './components/Admin/Links/Links';

import ComingSoon from './components/ComingSoon/ComingSoon';
import RedirectPage from './components/Redirect/RedirectPage';

const AppRoutes = [
  {
    index: true,
    element: <Home />,
  },
  {
    path: '/admin',
    element: (
      <LayoutDashboardAdmin>
        <ComingSoon />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/users',
    element: (
      <LayoutDashboardAdmin>
        <UsersAdmin />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/links',
    element: (
      <LayoutDashboardAdmin>
        <LinksAdmin />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/qr-codes',
    element: (
      <LayoutDashboardAdmin>
        <ComingSoon />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/link-in-bio',
    element: (
      <LayoutDashboardAdmin>
        <ComingSoon />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/settings',
    element: (
      <LayoutDashboardAdmin>
        <ComingSoon />
      </LayoutDashboardAdmin>
    ),
  },
  {
    path: '/admin/login',
    element: <LoginAdmin />,
  },
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/register',
    element: <Register />,
  },
  {
    path: '/forgot-password',
    element: <ComingSoon />,
  },
  {
    path: '/dashboard',
    element: (
      <LayoutDashboardUser>
        <HomeDashboard />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/links',
    element: (
      <LayoutDashboardUser>
        <LinksDashboard />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/links/create',
    element: (
      <LayoutDashboardUser>
        <CreateLink />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/link-in-bio',
    element: (
      <LayoutDashboardUser>
        <ComingSoon />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/qr-codes',
    element: (
      <LayoutDashboardUser>
        <ComingSoon />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/settings',
    element: (
      <LayoutDashboardUser>
        <SettingsDashboard />
      </LayoutDashboardUser>
    ),
  },
  {
    path: '/:slug',
    element: <RedirectPage />,
  },
];

export default AppRoutes;
