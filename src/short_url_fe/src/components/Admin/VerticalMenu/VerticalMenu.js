import { useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Nav, NavItem, NavLink } from 'reactstrap';
import {
  HomeOutlined,
  LinkOutlined,
  QrcodeOutlined,
  ContainerOutlined,
  ToolOutlined,
  UserOutlined,
} from '@ant-design/icons';

import './VerticalMenu.scss';
import { UserContext } from '../../../context/userContext';

export default function VerticalMenu() {
  const navigate = useNavigate();

  const { activeItemVerticalMenu, setActiveItemVerticalMenu } =
    useContext(UserContext);

  useEffect(() => {
    const currentPathName = window.location.pathname;
    switch (currentPathName) {
      case '/admin':
        setActiveItemVerticalMenu('home');
        break;
      case '/admin/users':
        setActiveItemVerticalMenu('users');
        break;
      case '/admin/links':
        setActiveItemVerticalMenu('links');
        break;
      case '/admin/settings':
        setActiveItemVerticalMenu('settings');
        break;
      case '/admin/qr-codes':
        setActiveItemVerticalMenu('qr-codes');
        break;
      case '/admin/link-in-bio':
        setActiveItemVerticalMenu('link-in-bio');
        break;
      default:
        setActiveItemVerticalMenu('home');
        break;
    }
  }, []);

  const renderItems = [
    {
      title: 'Home',
      link: '/admin',
      activeItem: 'home',
      icon: <HomeOutlined className="me-2" />,
    },
    {
      title: 'Users',
      link: '/admin/users',
      activeItem: 'users',
      icon: <UserOutlined className="me-2" />,
    },
    {
      title: 'Links',
      link: '/admin/links',
      activeItem: 'links',
      icon: <LinkOutlined className="me-2" />,
    },
    {
      title: 'QR Codes',
      link: '/admin/qr-codes',
      activeItem: 'qr-codes',
      icon: <QrcodeOutlined className="me-2" />,
    },
    {
      title: 'Link-in-bio',
      link: '/admin/link-in-bio',
      activeItem: 'link-in-bio',
      icon: <ContainerOutlined className="me-2" />,
    },
    {
      title: 'Settings',
      link: '/admin/settings',
      activeItem: 'settings',
      icon: <ToolOutlined className="me-2" />,
    },
  ];

  return (
    <>
      <div className="col-2">
        <Nav
          vertical
          className="px-3 h-100 border-end dashboard__menu bg-white"
        >
          {renderItems.map((item, index) => {
            return (
              <NavItem
                className={
                  (activeItemVerticalMenu === item.activeItem ? 'active' : '') +
                  ' dashboard__nav-item'
                }
              >
                <NavLink
                  href={item.link}
                  className="dashboard__nav-item__link d-flex justify-content-start align-items-center fw-semibold text-black"
                >
                  {item.icon}
                  {item.title}
                </NavLink>
              </NavItem>
            );
          })}
        </Nav>
      </div>
    </>
  );
}
