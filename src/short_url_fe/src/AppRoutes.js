import { Home } from './components/Home/Home';
import Login from './components/Login/Login';
import RedirectPage from './components/Redirect/RedirectPage';
import ComingSoon from './components/ComingSoon/ComingSoon';
import HomeDashboard from './components/Dashboard/Home/HomeDashboard';
import LinksDashboard from './components/Dashboard/Links/LinksDashboard';
import SettingsDashboard from './components/Dashboard/Settings/SettingsDashboard';
import { LayoutDashboard } from './components/Dashboard/LayoutDashboard';
import CreateLink from './components/Dashboard/Links/Create/CreateLink';

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
    element: <ComingSoon />,
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
