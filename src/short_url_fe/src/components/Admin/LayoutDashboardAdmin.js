import { useContext } from 'react';
import { Container } from 'reactstrap';
import { useNavigate } from 'react-router-dom';

import VerticalMenu from './VerticalMenu/VerticalMenu';
import { UserContext } from '../../context/userContext';
import { getTokenFromLocalStorage } from './../../lib/common';
import { logoutServer } from '../../services/UserService';

export function LayoutDashboardAdmin(props) {
  let token = getTokenFromLocalStorage();
  const { logout, user } = useContext(UserContext);

  const navigate = useNavigate();
  const handleLogout = async () => {
    await logoutServer();
    logout();
    navigate('/');
  };
  
  if (token === undefined || token?.length === 0) {
    handleLogout();
  }
  if (user.isAdmin === false) {
    navigate('/');
  }

  return (
    <>
      <div className="row">
        <VerticalMenu />
        <div className="col-10">
          <Container tag="main">{props.children}</Container>
        </div>
      </div>
    </>
  );
}
