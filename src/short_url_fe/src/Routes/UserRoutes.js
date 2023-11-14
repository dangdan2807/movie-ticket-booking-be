import { LayoutDashboardUser } from './../components/User/Dashboard/LayoutDashboardUser';

const UserRoutes = (props) => {
  return (
    <>
      <LayoutDashboardUser>{props.children}</LayoutDashboardUser>
    </>
  );
};

export default UserRoutes;
