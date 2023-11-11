import React, { useEffect, useState, useContext } from 'react';
import { Link } from 'react-router-dom';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import {
  GoogleOutlined,
  LoadingOutlined,
  EyeInvisibleOutlined,
  EyeOutlined,
} from '@ant-design/icons';
import { toast } from 'react-toastify';

import { UserContext } from '../../../context/userContext';
import { register } from '../../../services/UserService';
import { handleError } from '../../../lib/common';

export default function Register() {
  const { setCurrentPage } = useContext(UserContext);

  const [showPassword, setShowPassword] = useState(false);
  const [fullName, setFullName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [isErrorFullName, setIsErrorFullName] = useState(false);
  const [isErrorEmail, setIsErrorEmail] = useState(false);
  const [isErrorPassword, setIsErrorPassword] = useState(false);
  const [loadingAPI, setLoadingAPI] = useState(false);

  useEffect(() => {
    setCurrentPage('register');
  }, []);

  const handleWidthRegisterForm = () => {
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
    if (!fullName.trim() || fullName.length === 0) {
      setIsErrorFullName(true);
      toast.error('Full name is required');
    }
    if (fullName.length >= 100) {
      setIsErrorFullName(true);
      toast.error('Full name must be less than 100 characters');
    }
    if (!email.trim() || email.length === 0) {
      setIsErrorEmail(true);
      toast.error('email is required');
    }
    var emailPattern =
      /^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$/;
    if (!emailPattern.test(email)) {
      setIsErrorEmail(true);
      toast.error('Invalid email');
    }
    if (!password.trim() || password.length === 0) {
      setIsErrorPassword(true);
      toast.error('Password is required');
    }
    if (password.length >= 255) {
      setIsErrorPassword(true);
      toast.error('Password must be less than 255 characters');
    }
    var passwordPattern = /^.{6,}$/;
    if (!passwordPattern.test(password)) {
      setIsErrorPassword(true);
      toast.error('Invalid password');
    }

    if (isErrorFullName || isErrorPassword || isErrorEmail) {
      return;
    }

    setLoadingAPI(true);
    let res = await register(fullName, email, password);
    if (res && res.data) {
      toast.success('Register successful');
    } else {
      handleError(res, 'Register failed');
    }
    setLoadingAPI(false);
  };

  return (
    <>
      <div className="mt-5 d-flex flex-column align-items-center text-secondary">
        <h2 className=" fw-medium text-dark-emphasis">Create your account</h2>
        <p>
          Already have an account? <Link to="/login">login</Link>
        </p>
        <div className="d-flex w-50">
          <Button className="btn-bg-primary fw-medium border border-0 rounded-1 py-2 px-3 me-1 d-flex w-100 justify-content-center align-items-center">
            <GoogleOutlined className="me-1" />
            Sign up with Google
          </Button>
        </div>
        <p className="separator mt-3 mb-1 fw-medium fs-5">
          <span>OR</span>
        </p>
        <div className={handleWidthRegisterForm()}>
          <Form
            className="py-3"
            method="POST"
            acceptCharset="UTF-8"
            onSubmit={async (e) => await handleSubmit(e)}
          >
            <FormGroup>
              <Label htmlFor="fullNameInput">Full Name</Label>
              <Input
                className={isErrorFullName ? 'is-invalid' : ''}
                type="text"
                name="fullNameInput"
                id="fullNameInput"
                placeholder="Your full name"
                onChange={(e) => {
                  setIsErrorFullName(false);
                  setFullName(`${e.target.value}`);
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label htmlFor="usernameInput">Email</Label>
              <Input
                className={isErrorEmail ? 'is-invalid' : ''}
                type="email"
                name="usernameInput"
                id="usernameInput"
                placeholder="Your email"
                onChange={(e) => {
                  setIsErrorEmail(false);
                  setEmail(`${e.target.value}`);
                }}
              />
            </FormGroup>
            <FormGroup>
              <div className="d-flex justify-content-between align-items-center mb-1">
                <Label htmlFor="backHalfInput">
                  <div className="d-flex align-items-center">Password</div>
                </Label>
                <div
                  className={
                    'd-flex align-items-center text-primary cursor-pointer ' +
                    (showPassword ? 'd-none' : '')
                  }
                  onClick={() => setShowPassword(true)}
                >
                  <EyeOutlined className="me-1" /> Show
                </div>
                <div
                  className={
                    'd-flex align-items-center text-primary cursor-pointer ' +
                    (!showPassword ? 'd-none' : '')
                  }
                  onClick={() => setShowPassword(false)}
                >
                  <EyeInvisibleOutlined className="me-1" /> Hide
                </div>
              </div>
              <Input
                className={isErrorPassword ? 'is-invalid' : ''}
                type={!showPassword ? 'password' : 'text'}
                name="backHalfInput"
                id="backHalfInput"
                placeholder="Your password"
                onChange={(e) => {
                  setIsErrorPassword(false);
                  setPassword(`${e.target.value}`);
                }}
              />
            </FormGroup>
            <FormGroup>
              <Button className="btn-bg-primary w-100 d-flex justify-content-center align-items-center mt-4">
                {loadingAPI && <LoadingOutlined className="me-2" />}
                Sign up with Phone
              </Button>
              <div className="mt-4 d-flex flex-column justify-content-center align-items-center">
                <span>By creating an account, you agree to</span>
                <span className="">
                  Bitly's
                  <a
                    href="https://bitly.com/pages/terms-of-service"
                    className="ms-1 text-secondary"
                  >
                    Terms of Service
                  </a>
                  ,
                  <a
                    href="https://bitly.com/pages/privacy"
                    className="ms-1 text-secondary"
                  >
                    Privacy Policy
                  </a>{' '}
                  and
                  <a
                    href="https://bitly.com/pages/acceptable-use"
                    className="ms-1 text-secondary"
                  >
                    Acceptable Use Policy
                  </a>
                  .
                </span>
              </div>
            </FormGroup>
          </Form>
        </div>
      </div>
    </>
  );
}
