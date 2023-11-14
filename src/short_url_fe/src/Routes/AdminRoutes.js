import { LayoutDashboardAdmin } from '../components/Admin/LayoutDashboardAdmin';

const AdminRoute = (props) => {

  return (
    <>
      <LayoutDashboardAdmin>{props.children}</LayoutDashboardAdmin>
    </>
  );
};

export default AdminRoute;
