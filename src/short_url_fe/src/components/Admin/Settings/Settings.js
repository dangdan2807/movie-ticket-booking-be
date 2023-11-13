import React, { useContext, useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { LoadingOutlined } from '@ant-design/icons';

import { UserContext } from '../../../context/userContext';
import {
  updateProfile,
  getProfileUser,
  changePassword,
} from './../../../services/UserService';
import { handleError } from '../../../lib/common';

export default function Settings() {
  const { user } = useContext(UserContext);

  const [fullName, setFullName] = useState(user.name);
  const [email, setEmail] = useState(user.email);
  const [address, setAddress] = useState('');

  const [loadingAPIFullName, setLoadingAPIFullName] = useState(false);
  const [loadingAPIEmail, setLoadingAPIEmail] = useState(false);
  const [loadingAPIPassword, setLoadingAPIPassword] = useState(false);
  const [loadingAPIAddress, setLoadingAPIAddress] = useState(false);

  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  useEffect(() => {
    async function fetchData() {
      const res = await getProfileUser();
      if (res && res.data) {
        setFullName(res.data.fullName);
        setEmail(res.data.email);
        setAddress(res.data.address);
      }
    }
    fetchData();
  }, []);

  const [isErrorFullName, setIsErrorFullName] = useState(false);
  const [isErrorEmail, setIsErrorEmail] = useState(false);
  const [isErrorAddress, setIsErrorAddress] = useState(false);
  const [isErrorCurrentPassword, setIsErrorCurrentPassword] = useState(false);
  const [isErrorNewPassword, setIsErrorNewPassword] = useState(false);
  const [isErrorConfirmPassword, setIsErrorConfirmPassword] = useState(false);

  const [isEnableUpdateFullName, setIsEnableUpdateFullName] = useState(false);
  const [isEnableUpdateEmail, setIsEnableUpdateEmail] = useState(false);
  const [isEnableUpdateAddress, setIsEnableUpdateAddress] = useState(false);
  const [isEnableUpdatePassword, setIsEnableUpdatePassword] = useState(false);

  const handleUpdateFullName = async () => {
    if (fullName.trim().length <= 0) {
      toast.error('Full name is required');
      setIsErrorFullName(true);
      return;
    }
    if (fullName.trim().length >= 100 || fullName.trim().length < 6) {
      toast.error('Full name must be between 6 - 100 characters');
      setIsErrorFullName(true);
      return;
    }

    setLoadingAPIFullName(true);
    const resGet = await getProfileUser();
    if (resGet && resGet.data) {
      const resUpdate = await updateProfile(
        fullName.trim(),
        resGet.data?.email,
        resGet.data?.address,
      );
      if (resUpdate && resUpdate?.data) {
        toast.success('Update full name successfully');
        setIsEnableUpdateFullName(false);
      } else {
        handleError(resUpdate, 'Update full name failed');
      }
    } else {
      handleError(resGet, 'User not found');
    }
    setLoadingAPIFullName(false);
  };
  const handleUpdateEmail = async () => {
    if (email.trim().length <= 0) {
      toast.error('email is required');
      setIsErrorEmail(true);
      return;
    }
    if (email.trim().length >= 100 || email.trim().length < 6) {
      toast.error('email must be between 6 - 100 characters');
      setIsErrorEmail(true);
      return;
    }
    var emailPattern =
      /^[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\.[a-zA-Z0-9]+)*$/;
    if (!emailPattern.test(email)) {
      toast.error('Invalid email');
      return;
    }

    setLoadingAPIEmail(true);
    const resGet = await getProfileUser();
    if (resGet && resGet.data) {
      const resUpdate = await updateProfile(
        resGet.data?.fullName,
        email.trim(),
        resGet.data?.address,
      );
      if (resUpdate && resUpdate?.data) {
        toast.success('Update email successfully');
        setIsEnableUpdateEmail(false);
      } else {
        handleError(resUpdate, 'Update email failed');
      }
    } else {
      handleError(resGet, 'User not found');
    }
    setLoadingAPIEmail(false);
  };
  const handleUpdateAddress = async () => {
    if (address.trim().length <= 0) {
      toast.error('address is required');
      setIsErrorAddress(true);
      return;
    }
    if (address.trim().length >= 100 || email.trim().length < 6) {
      toast.error('address must be between 6 - 100 characters');
      setIsErrorAddress(true);
      return;
    }

    setLoadingAPIAddress(true);
    const resGet = await getProfileUser();
    if (resGet && resGet.data) {
      const resUpdate = await updateProfile(
        resGet.data?.fullName,
        resGet.data?.email,
        address.trim(),
      );
      if (resUpdate && resUpdate?.data) {
        toast.success('Update address successfully');
        setIsEnableUpdateAddress(false);
      } else {
        handleError(resUpdate, 'Update address failed');
      }
    } else {
      handleError(resGet, 'User not found');
    }
    setLoadingAPIAddress(false);
  };
  const handleUpdatePassword = async () => {
    let flag = false;
    if (currentPassword.trim().length < 0) {
      flag = true;
      setIsErrorCurrentPassword(true);
      toast.error('current password is required');
    }
    if (
      currentPassword.trim().length > 255 ||
      currentPassword.trim().length < 6
    ) {
      flag = true;
      setIsErrorCurrentPassword(true);
      toast.error('current password must be between 6 - 255 characters');
    }

    if (newPassword.trim().length < 0) {
      flag = true;
      setIsErrorNewPassword(true);
      toast.error('new password is required');
    }
    if (newPassword.trim().length > 255 || newPassword.trim().length < 6) {
      flag = true;
      setIsErrorNewPassword(true);
      toast.error('new password must be between 6 - 255 characters');
    }

    if (confirmPassword.trim().length < 0) {
      flag = true;
      setIsErrorConfirmPassword(true);
      toast.error('confirm password is required');
    }
    if (
      confirmPassword.trim().length > 255 ||
      confirmPassword.trim().length < 6
    ) {
      flag = true;
      setIsErrorConfirmPassword(true);
      toast.error('confirm password must be between 6 - 255 characters');
    }
    if (confirmPassword.trim() !== newPassword.trim()) {
      flag = true;
      setIsErrorConfirmPassword(true);
      toast.error('confirm password is not match');
    }

    if (flag) {
      return;
    }
    setLoadingAPIPassword(true);
    const res = await changePassword(
      currentPassword.trim(),
      newPassword.trim(),
      confirmPassword.trim(),
    );
    if ((res && res.data) || (res && res.statusCode === 200)) {
      toast.success(res.data?.message ?? res?.message);
      setIsEnableUpdatePassword(false);
    } else {
      handleError(res, 'Change Password Failed');
    }
    setLoadingAPIPassword(false);
  };

  return (
    <>
      <div className="row ms-2">
        <div className="col-12">
          <div>
            <h3 className="fw-bolder">Profile</h3>
            <h4 className="fw-bolder">Preferences</h4>
          </div>
          <div className="my-4">
            <label htmlFor="fullNameInput" className="form-label fw-bolder">
              Full name
            </label>
            <input
              type="text"
              className={
                'form-control ' + (isErrorFullName ? 'is-invalid' : '')
              }
              id="fullNameInput"
              value={fullName}
              placeholder="Your full name"
              onChange={(e) => {
                setIsErrorFullName(false);
                setFullName(e.target.value);
                setIsEnableUpdateFullName(false);
              }}
            />
            <button
              type="button"
              className={
                'btn btn-primary mt-3 d-flex justify-content-center align-items-center ' +
                (!isEnableUpdateFullName ? 'disabled' : '')
              }
              onClick={async () => await handleUpdateFullName()}
            >
              {loadingAPIFullName && <LoadingOutlined className="me-2" />}
              Update full name
            </button>
          </div>
          <div className="my-4">
            <label htmlFor="emailInput" className="form-label fw-bolder">
              Email
            </label>
            <input
              type="text"
              className={'form-control ' + (isErrorEmail ? 'is-invalid' : '')}
              id="emailInput"
              value={email}
              placeholder="Your email address"
              onChange={(e) => {
                setEmail(e.target.value);
                setIsErrorEmail(false);
                setIsEnableUpdateEmail(false);
              }}
            />
            <button
              type="button"
              className={
                'btn btn-primary mt-3 d-flex justify-content-center align-items-center ' +
                (!isEnableUpdateEmail ? 'disabled' : '')
              }
              onClick={async () => await handleUpdateEmail()}
            >
              {loadingAPIEmail && <LoadingOutlined className="me-2" />}
              Update email
            </button>
          </div>
          <div className="my-4">
            <label htmlFor="addressInput" className="form-label fw-bolder">
              Address
            </label>
            <input
              type="text"
              className={'form-control ' + (isErrorAddress ? 'is-invalid' : '')}
              id="addressInput"
              value={address}
              placeholder="Your address"
              onChange={(e) => {
                setAddress(e.target.value);
                setIsErrorAddress(false);
                setIsEnableUpdateAddress(true);
              }}
            />
            <button
              type="button"
              className={
                'btn btn-primary mt-3 d-flex justify-content-center align-items-center ' +
                (!isEnableUpdateAddress ? 'disabled' : '')
              }
              onClick={async () => await handleUpdateAddress()}
            >
              {loadingAPIAddress && <LoadingOutlined className="me-2" />}
              Update address
            </button>
          </div>
          <div className="my-5">
            <h4 className="fw-bolder">Security & authentication</h4>
            <p className="fw-bolder mt-3 mb-1">Change password</p>
            <p className="mt-0">
              You will be required to login after changing your password
            </p>

            <label
              htmlFor="currentPasswordInput"
              className="form-label fw-bolder"
            >
              Current password
            </label>
            <input
              type="password"
              className={
                'form-control ' + (isErrorCurrentPassword ? 'is-invalid' : '')
              }
              placeholder="Your current password"
              onChange={(e) => {
                setIsErrorCurrentPassword(false);
                setCurrentPassword(e.target.value);
                if (currentPassword && newPassword && confirmPassword) {
                  setIsEnableUpdatePassword(true);
                } else {
                  setIsEnableUpdatePassword(false);
                }
              }}
            />

            <label
              htmlFor="newPasswordInput"
              className="form-label fw-bolder mt-3"
            >
              New password
            </label>
            <input
              type="password"
              className={
                'form-control ' + (isErrorNewPassword ? 'is-invalid' : '')
              }
              placeholder="Your new password"
              onChange={(e) => {
                setIsErrorNewPassword(false);
                setNewPassword(e.target.value);
                if (currentPassword && newPassword && confirmPassword) {
                  setIsEnableUpdatePassword(true);
                } else {
                  setIsEnableUpdatePassword(false);
                }
              }}
            />

            <label
              htmlFor="confirmNewPasswordInput"
              className="form-label fw-bolder mt-3"
            >
              Confirm new password
            </label>
            <input
              type="password"
              className={
                'form-control ' + (isErrorConfirmPassword ? 'is-invalid' : '')
              }
              placeholder="Your confirm new password"
              onChange={(e) => {
                setIsErrorConfirmPassword(false);
                setConfirmPassword(e.target.value);
                if (currentPassword && newPassword && confirmPassword) {
                  setIsEnableUpdatePassword(true);
                } else {
                  setIsEnableUpdatePassword(false);
                }
              }}
            />

            <button
              type="button"
              className={
                'btn btn-primary mt-3 d-flex justify-content-center align-items-center ' +
                (!isEnableUpdatePassword ? 'disabled' : '')
              }
              onClick={async () => await handleUpdatePassword()}
            >
              {loadingAPIPassword && <LoadingOutlined className="me-2" />}
              Update email
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
