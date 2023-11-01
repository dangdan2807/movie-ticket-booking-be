import { useNavigate } from 'react-router-dom';
import { Nav, NavItem, NavLink, Button } from 'reactstrap';
import { HomeOutlined, LinkOutlined } from '@ant-design/icons';
import './VerticalMenu.scss';
import { useEffect, useState } from 'react';

export default function VerticalMenu() {
  const navigate = useNavigate();

  const [currentNavItem, setCurrentNavItem] = useState('home');

  useEffect(() => {
    const currentPathName = window.location.pathname;
    switch (currentPathName) {
      case '/dashboard':
        setCurrentNavItem('home');
        break;
      case '/links':
        setCurrentNavItem('links');
        break;
      case '/settings':
        setCurrentNavItem('settings');
        break;
      default:
        setCurrentNavItem('home');
        break;
    }
  }, []);

  return (
    <>
      {/* <div className="row"> */}
        <Nav vertical className="col-2 ps-3 h-100 border-end dashboard__menu bg-white">
          <NavItem className="mt-2">
            <Button
              className="w-100"
              color="primary"
              onClick={() => {
                navigate('/');
              }}
            >
              Create new
            </Button>
          </NavItem>
          <NavItem
            className={
              (currentNavItem === 'home' ? 'active' : '') +
              ' dashboard__nav-item'
            }
          >
            <NavLink
              href="/dashboard"
              className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
            >
              <HomeOutlined className="me-2" />
              Home
            </NavLink>
          </NavItem>
          <NavItem
            className={
              (currentNavItem === 'links' ? 'active' : '') +
              ' dashboard__nav-item'
            }
          >
            <NavLink
              href="/links"
              className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
            >
              <LinkOutlined className="me-2" />
              Links
            </NavLink>
          </NavItem>
          <NavItem
            className={
              (currentNavItem === 'settings' ? 'active' : '') +
              ' dashboard__nav-item'
            }
          >
            <NavLink
              href="/settings"
              className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
            >
              <LinkOutlined className="me-2" />
              Settings
            </NavLink>
          </NavItem>
        </Nav>
      {/* </div> */}
    </>
  );
}
