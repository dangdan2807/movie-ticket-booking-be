import { useEffect, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Nav, NavItem, NavLink, Button } from 'reactstrap';
import {
  HomeOutlined,
  LinkOutlined,
  PlusCircleOutlined,
} from '@ant-design/icons';

import './VerticalMenu.scss';
import { UserContext } from './../../context/userContext';
import ModalAddNew from '../Modals/ModalAddNew';

export default function VerticalMenu() {
  const navigate = useNavigate();

  const { activeItemVerticalMenu, setActiveItemVerticalMenu } =
    useContext(UserContext);
  const [isShowModalAddNew, setIsShowModalAddNew] = useState(false);

  useEffect(() => {
    const currentPathName = window.location.pathname;
    switch (currentPathName) {
      case '/dashboard':
        setActiveItemVerticalMenu('home');
        break;
      case '/links':
        setActiveItemVerticalMenu('links');
        break;
      case '/settings':
        setActiveItemVerticalMenu('settings');
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
                setIsShowModalAddNew(true);
              }}
            >
              <PlusCircleOutlined className="me-2" />
              Create new
            </Button>
            <ModalAddNew
              show={isShowModalAddNew}
              handleClose={() => setIsShowModalAddNew(false)}
            />
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
