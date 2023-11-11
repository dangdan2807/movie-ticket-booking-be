import { Home } from './components/User/Home/Home';
import { LayoutDashboard } from './components/User/Dashboard/LayoutDashboard';
import HomeDashboard from './components/User/Dashboard/Home/HomeDashboard';
import LinksDashboard from './components/User/Dashboard/Links/LinksDashboard';
import CreateLink from './components/User/Dashboard/Links/Create/CreateLink';
import Login from './components/User/Login/Login';
import Register from './components/User/Register/Register';
import SettingsDashboard from './components/User/Dashboard/Settings/SettingsDashboard';

import ComingSoon from './components/ComingSoon/ComingSoon';
import RedirectPage from './components/Redirect/RedirectPage';

const AppRoutes = [
  {
    index: true,
    element: <Home />,
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
      <LayoutDashboard>
        <HomeDashboard />
      </LayoutDashboard>
    ),
  },
  {
    path: '/links',
    element: (
      <LayoutDashboard>
        <LinksDashboard />
      </LayoutDashboard>
    ),
  },
  {
    path: '/links/create',
    element: (
      <LayoutDashboard>
        <CreateLink />
      </LayoutDashboard>
    ),
  },
  {
    path: '/link-in-bio',
    element: (
      <LayoutDashboard>
        <ComingSoon />
      </LayoutDashboard>
    ),
  },
  {
    path: '/qr-codes',
    element: (
      <LayoutDashboard>
        <ComingSoon />
      </LayoutDashboard>
    ),
  },
  {
    path: '/settings',
    element: (
      <LayoutDashboard>
        <SettingsDashboard />
      </LayoutDashboard>
    ),
  },
  {
    path: '/:slug',
    element: <RedirectPage />,
  },
];

export default AppRoutes;
