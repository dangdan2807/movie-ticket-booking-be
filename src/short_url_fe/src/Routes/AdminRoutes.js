import { Route, Routes } from 'react-router-dom';
import { useContext } from 'react';

import { LayoutDashboardAdmin } from '../components/Admin/LayoutDashboardAdmin';
import { UserContext } from '../context/userContext';

const AdminRoute = (props) => {
  const { user } = useContext(UserContext);
  if (user && !user.auth) {
    return <>Your don`t have permission to access</>;
  }
  
  return (
    <>
      <Routes>
        <Route
          path={props.path}
          element={
            <LayoutDashboardAdmin>{props.children}</LayoutDashboardAdmin>
          }
        />
      </Routes>
    </>
  );
};

export default AdminRoute;
