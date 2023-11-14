import { Route, Routes } from 'react-router-dom';
import { useContext } from 'react';

import { LayoutDashboardUser } from './../components/User/Dashboard/LayoutDashboardUser';
import { UserContext } from '../context/userContext';

const UserRoutes = (props) => {
  const { user } = useContext(UserContext);

  if (user && !user.auth) {
    return <>Your don`t have permission to access</>;
  }

  return (
    <>
      <Routes>
        <Route
          path={props.path}
          element={<LayoutDashboardUser>{props.children}</LayoutDashboardUser>}
        />
      </Routes>
    </>
  );
};

export default UserRoutes;
