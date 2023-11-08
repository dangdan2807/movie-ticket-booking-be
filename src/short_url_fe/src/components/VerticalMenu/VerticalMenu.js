import { useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Nav, NavItem, NavLink, Button } from 'reactstrap';
import {
  HomeOutlined,
  LinkOutlined,
  PlusCircleOutlined,
  QrcodeOutlined,
  ContainerOutlined,
} from '@ant-design/icons';

import './VerticalMenu.scss';
import { UserContext } from './../../context/userContext';

export default function VerticalMenu() {
  const navigate = useNavigate();

  const { activeItemVerticalMenu, setActiveItemVerticalMenu } =
    useContext(UserContext);

  useEffect(() => {
    const currentPathName = window.location.pathname;
    switch (currentPathName) {
      case '/dashboard':
        setActiveItemVerticalMenu('home');
        break;
      case '/links':
      case '/links/create':
        setActiveItemVerticalMenu('links');
        break;
      case '/settings':
        setActiveItemVerticalMenu('settings');
        break;
      case '/qr-codes':
        setActiveItemVerticalMenu('qr-codes');
        break;
      case '/link-in-bio':
        setActiveItemVerticalMenu('link-in-bio');
        break;
      default:
        setActiveItemVerticalMenu('home');
        break;
    }
  }, []);

  return (
    <>
      <div className="col-2">
        <Nav
          vertical
          className="px-3 h-100 border-end dashboard__menu bg-white"
        >
          <NavItem className="mt-2">
            <Button
              className="w-100 d-flex align-items-center justify-content-center"
              color="primary"
              onClick={() => {
                navigate('/links/create');
              }}
            >
              <PlusCircleOutlined className="me-2" />
              Create new
            </Button>
          </NavItem>
          <NavItem
            className={
              (activeItemVerticalMenu === 'home' ? 'active' : '') +
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
              (activeItemVerticalMenu === 'links' ? 'active' : '') +
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
              (activeItemVerticalMenu === 'qr-codes' ? 'active' : '') +
              ' dashboard__nav-item'
            }
          >
            <NavLink
              href="/qr-codes"
              className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
            >
              <QrcodeOutlined className="me-2" />
              QR Codes
            </NavLink>
          </NavItem>
          <NavItem
            className={
              (activeItemVerticalMenu === 'link-in-bio' ? 'active' : '') +
              ' dashboard__nav-item'
            }
          >
            <NavLink
              href="/link-in-bio"
              className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
            >
              <ContainerOutlined className="me-2" />
              Link-in-bio
            </NavLink>
          </NavItem>
          <NavItem
            className={
              (activeItemVerticalMenu === 'settings' ? 'active' : '') +
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
      </div>
    </>
  );
}
