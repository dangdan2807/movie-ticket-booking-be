import React, { useEffect, useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import {
  GoogleOutlined,
  FacebookOutlined,
  LoadingOutlined,
} from '@ant-design/icons';
import { toast } from 'react-toastify';

import { UserContext } from './../../context/userContext';
import { storeTokenInLocalStorage } from './../../lib/common';
import { getProfile, login } from './../../services/UserService';
import './Login.scss';

export default function Login() {
  const navigate = useNavigate();
  const { loginContext, logout, user, setCurrentPage } =
    useContext(UserContext);

  const [phone, setPhone] = useState('');
  const [password, setPassword] = useState('');
  const [loadingAPI, setLoadingAPI] = useState(false);

  useEffect(() => {
    setCurrentPage('login');
  }, []);

  useEffect(() => {
    if (user && user.auth === true) {
      navigate('/');
    } else {
      navigate('/login');
    }
  }, [user]);

  const handleWidthLoginForm = () => {
    var w = window.innerWidth;
    if (w >= 1024) {
      return 'w-50';
    } else if (w < 1024 && w >= 768) {
      return 'w-75';
    } else if (w < 768) {
      return 'w-100';
    }
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (!phone.trim() || phone.length === 0) {
      toast.error('Phone is required');
      return;
    }
    var phonePattern = /^0\d{9}$/;
    if (!phonePattern.test(phone)) {
      toast.error('`Invalid` phone number');
      return;
    }
    if (!password.trim() || password.length === 0) {
      toast.error('Password is required');
      return;
    }
    var passwordPattern = /^.{6,}$/;
    if (!passwordPattern.test(password)) {
      toast.error('`Invalid` password');
      return;
    }

    setLoadingAPI(true);
    let res = await login(phone, password);
    if (res && res.data) {
      storeTokenInLocalStorage(res.data.accessToken);
      let resProfile = await getProfile();
      if (res && res.data) {
        loginContext(resProfile.data.fullName, resProfile.data.phone);
        navigate('/');
      } else {
        logout();
        navigate('/login');
      }
    } else {
      const req = res.response?.data;
      if (req !== undefined) {
        toast.error(req.message);
      } else {
        toast.error('Login failed');
      }
    }
    setLoadingAPI(false);
  };

  return (
    <div className="mt-5 d-flex flex-column align-items-center text-secondary">
      <h2 className=" fw-medium">Log in and start sharing</h2>
      <p>
        Don't have an account? <Link to="/register">Sign up</Link>
      </p>
      <p>Log in with:</p>
      <div className="d-flex">
        <Button className="btn-bg-primary fw-medium border border-0 rounded-1 py-2 px-3 d-flex align-items-center me-1">
          <GoogleOutlined className="me-1" />
          Google
        </Button>
        <Button className="btn-bg-primary fw-medium border border-0 rounded-1 py-2 px-3 d-flex align-items-center">
          <FacebookOutlined className="me-1" />
          Facebook
        </Button>
      </div>
      <p className="separator mt-3 mb-1 fw-medium fs-5">
        <span>OR</span>
      </p>
      <div className={handleWidthLoginForm()}>
        <Form
          className="py-3"
          method="POST"
          acceptCharset="UTF-8"
          onSubmit={async (e) => await handleSubmit(e)}
        >
          <FormGroup>
            <Label for="usernameInput">Phone</Label>
            <Input
              type="phone"
              name="usernameInput"
              id="usernameInput"
              placeholder="Your phone"
              onChange={(e) => setPhone(`${e.target.value}`)}
              // required
            />
          </FormGroup>
          <FormGroup>
            <Label for="backHalfInput">
              <div className="d-flex align-items-center">Password</div>
            </Label>
            <Input
              type="password"
              name="backHalfInput"
              id="backHalfInput"
              placeholder="Your password"
              onChange={(e) => setPassword(`${e.target.value}`)}
              // required
            />
          </FormGroup>
          <FormGroup>
            <Link className="d-flex justify-content-end" to="/forgot-password">
              Forgot your password?
            </Link>
          </FormGroup>
          <FormGroup>
            <Button className="btn-bg-primary w-100 d-flex justify-content-center align-items-center">
              {loadingAPI && <LoadingOutlined className="me-2" />}
              Login
            </Button>
          </FormGroup>
        </Form>
      </div>
    </div>
  );
}
