import { useEffect, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import { Nav, NavItem, NavLink, Button } from 'reactstrap';
import {
  HomeOutlined,
  LinkOutlined,
  PlusCircleOutlined,
  QrcodeOutlined,
  ContainerOutlined,
  ToolOutlined,
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

  const renderItems = [
    {
      title: 'Home',
      link: '/dashboard',
      activeItem: 'home',
      icon: <HomeOutlined className="me-2" />,
    },
    {
      title: 'Links',
      link: '/links',
      activeItem: 'links',
      icon: <LinkOutlined className="me-2" />,
    },
    {
      title: 'QR Codes',
      link: '/qr-codes',
      activeItem: 'qr-codes',
      icon: <QrcodeOutlined className="me-2" />,
    },
    {
      title: 'Link-in-bio',
      link: '/link-in-bio',
      activeItem: 'link-in-bio',
      icon: <ContainerOutlined className="me-2" />,
    },
    {
      title: 'Settings',
      link: '/settings',
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
          {renderItems.map((item, index) => {
            return (
              <NavItem
                key={item.activeItem}
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
