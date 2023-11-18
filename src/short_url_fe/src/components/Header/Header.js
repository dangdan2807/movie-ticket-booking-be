import React, { useEffect, useState, useContext } from 'react';
import {
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from 'reactstrap';
import { Link, useNavigate } from 'react-router-dom';
import { UserOutlined } from '@ant-design/icons';

import './Header.scss';
import { UserContext } from '../../context/userContext';
import {
  getProfileAdmin,
  getProfileUser,
  logoutServer,
} from '../../services/UserService';

export function Header() {
  const { logout, user, currentPage, loginContext } = useContext(UserContext);
  const navigate = useNavigate();

  const [collapsed, setCollapsed] = useState(true);
  const [dropdownOpen, setDropdownOpen] = useState(false);

  const toggle = () => setDropdownOpen((prevState) => !prevState);

  const handleLogout = async () => {
    await logoutServer();
    logout();
    navigate('/');
  };

  useEffect(() => {
    async function fetchData() {
      const currentPathname = window.location.pathname;
      const res = currentPathname.includes('/admin')
        ? await getProfileAdmin()
        : await getProfileUser();
      if (res && res.data) {
        const { id, fullName, email, roles } = res.data;
        let isAdmin = false;
        switch (roles[0].roleCode) {
          case 'ADMIN':
          case 'MOD':
            isAdmin = true;
            break;
          default:
            isAdmin = false;
            break;
        }
        loginContext(id, fullName, email, isAdmin);
        if (isAdmin) {
          if (!currentPathname.includes('/admin')) {
            navigate('/admin');
          }
        } else {
          if (currentPathname.includes('/admin')) {
            navigate('/dashboard');
          }
        }
      } else {
        logout();
      }
    }
    fetchData();
  }, []);

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        container
        light
      >
        <NavbarBrand className="fw-bolder fs-2" tag={Link} to="/">
          Short Link
        </NavbarBrand>
        <NavbarToggler
          onClick={() => {
            setCollapsed(!collapsed);
          }}
          className="mr-2"
        />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          isOpen={!collapsed}
          navbar
        >
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink
                tag={Link}
                className={
                  'text-dark ' + (currentPage === 'home' ? 'fw-medium' : '')
                }
                to="/"
              >
                Home
              </NavLink>
            </NavItem>
            <NavItem className={user && user.auth === false ? '' : `d-none`}>
              <NavLink
                tag={Link}
                className={
                  'text-dark ' + (currentPage === 'login' ? 'fw-medium' : '')
                }
                to="/login"
              >
                Login
              </NavLink>
            </NavItem>
            <NavItem className={user && user.auth === false ? '' : `d-none`}>
              <NavLink
                tag={Link}
                className={
                  'text-dark ' + (currentPage === 'register' ? 'fw-medium' : '')
                }
                to="/register"
              >
                Register
              </NavLink>
            </NavItem>
            <NavItem
              className={
                'dropdown ' + (user && user.auth === true ? '' : `d-none`)
              }
            >
              <Dropdown isOpen={dropdownOpen} toggle={toggle} color="white">
                <DropdownToggle color="white" caret>
                  {user && user.auth === true ? `${user.name}` : ''}
                </DropdownToggle>
                <DropdownMenu>
                  <DropdownItem className="header__avatar d-flex justify-content-center align-items-center py-2">
                    <div className="header__avatar-img rounded-circle d-flex justify-content-center align-items-center">
                      <UserOutlined className="text-light fs-3" />
                    </div>
                    <div className="ms-2">
                      <p className="header__avatar__name">
                        {user && user.auth === true ? `${user.name}` : ''}
                      </p>
                      <p className="header__avatar__name">
                        {user && user.auth === true ? `${user.email}` : ''}
                      </p>
                    </div>
                  </DropdownItem>
                  <DropdownItem divider />
                  <DropdownItem>
                    <Link
                      to={
                        user && user.auth === true && user.isAdmin === false
                          ? '/dashboard'
                          : '/admin'
                      }
                      className="text-black text-decoration-none nav-menu-link"
                    >
                      Dashboard
                    </Link>
                  </DropdownItem>
                  <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
                </DropdownMenu>
              </Dropdown>
            </NavItem>
          </ul>
        </Collapse>
      </Navbar>
    </header>
  );
}
