import { useContext, useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { toast } from 'react-toastify';

import {
  updateUser,
  getUserById,
  getRoles,
} from '../../../services/UserService';
import { handleError } from '../../../lib/common';
import { UserContext } from '../../../context/userContext';
import dayjs from 'dayjs';

export default function ModalUpdateUser(props) {
  const { show, handleClose, reloadShortUrls, userId } = props;
  const { user } = useContext(UserContext);

  const [email, setEmail] = useState('');
  const [id, setId] = useState('');
  const [fullName, setFullName] = useState('');
  const [address, setAddress] = useState('');
  const [createAt, setCreateAt] = useState('');
  const [userStatus, setUserStatus] = useState();
  const [isYourself, setIsYourself] = useState(false);
  const [roles, setRoles] = useState([]);
  const [userRoles, setUserRoles] = useState([]);

  useEffect(() => {
    async function fetchData() {
      const resProfile = await getUserById(userId);
      if (resProfile && resProfile.data) {
        setId(resProfile.data.id);
        setEmail(resProfile.data.email);
        setFullName(resProfile.data.fullName);
        setAddress(resProfile.data.address);
        setCreateAt(resProfile.data.createAt);
        setUserStatus(resProfile.data.status);
        setUserRoles(resProfile.data.roles);
        if (resProfile.data.id == user.id) {
          setIsYourself(true);
        } else {
          setIsYourself(false);
        }
      } else {
        handleError(resProfile, 'Get user failed');
        handleClose();
      }

      const resRoles = await getRoles();
      if (resRoles && resRoles.data) {
        setRoles(resRoles.data);
      } else {
        handleClose(resRoles, 'Get user roles failed');
        handleClose();
      }
    }
    fetchData();
  }, [userId]);

  const handleUpdateUser = async () => {
    const resGet = await getUserById(userId);
    if (resGet && resGet.data) {
      if (userId === user.id) {
        toast.error('You cannot perform this action on yourself');
      } else {
        const userRoleIds = userRoles.map((role) => role.roleId);
        if (userRoleIds.length === 0) {
          toast.error('User must have at least 1 role');
          return;
        }
        const resUpdate = await updateUser(
          userId,
          fullName,
          email,
          address,
          userStatus,
          userRoleIds,
        );
        if (resUpdate && resUpdate.data) {
          toast.success('Update user successfully');
          reloadShortUrls();
          handleClose();
        } else {
          handleError(resUpdate, 'update user failed');
        }
      }
    } else {
      handleError(resGet, 'user not found');
    }
  };

  // Hàm xử lý khi checkbox thay đổi
  const handleCheckboxChange = (role) => {
    const roleCodesArr = userRoles.map((i) => i.roleCode);

    if (roleCodesArr.includes(role.roleCode)) {
      setUserRoles(userRoles.filter((r) => r.roleCode !== role.roleCode));
    } else {
      setUserRoles([...userRoles, role]);
    }
  };

  return (
    <>
      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>Update User</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div className="mb-3">
            <label htmlFor="id" className="form-label fw-bolder">
              Id
            </label>
            <input
              type="number"
              className="form-control"
              id="id"
              value={id}
              disabled
            />
          </div>
          <div className="mb-3">
            <label htmlFor="emailInput" className="form-label fw-bolder">
              Email
            </label>
            <input
              type="email"
              className="form-control"
              id="emailInput"
              placeholder="name@example.com"
              value={email}
              disabled
            />
          </div>
          <div className="mb-3">
            <label htmlFor="fullNameInput" className="form-label fw-bolder">
              Full name
            </label>
            <input
              type="text"
              className="form-control"
              id="fullNameInput"
              placeholder="Your name"
              value={fullName}
              onChange={(e) => setFullName(e.target.value)}
              disabled={isYourself}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="addressInput" className="form-label fw-bolder">
              Address
            </label>
            <input
              type="text"
              className="form-control"
              id="addressInput"
              placeholder="Your address"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              disabled={isYourself}
            />
          </div>
          <div className="mb-3">
            <label htmlFor="createAtInput" className="form-label fw-bolder">
              Create At
            </label>
            <input
              type="date"
              className="form-control"
              id="createAtInput"
              value={dayjs(createAt).format('YYYY-MM-DD')}
              disabled
            />
          </div>
          <div className="mb-3">
            <label htmlFor="statusInput" className="form-label fw-bolder">
              Status
            </label>
            <select
              className="form-select"
              id="statusInput"
              defaultValue={userStatus === true ? 'Active' : 'Inactive'}
              onChange={(e) => setUserStatus(e.target.value === 'Active')}
              disabled={isYourself}
            >
              <option value="Active">Active</option>
              <option value="Inactive">Inactive</option>
            </select>
          </div>
          <div className="mb-3">
            <label htmlFor="rolesInput" className="form-label fw-bolder">
              Roles
            </label>
            <div className="row row-cols-3 ms-1">
              {roles.map((role) => {
                return (
                  <div
                    className="form-check form-check-inline me-0"
                    key={role.roleCode}
                  >
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id={role.roleCode}
                      value={role.roleCode}
                      name="rolesCheckInput"
                      disabled={!role.status || user.id === userId}
                      checked={userRoles
                        .map((i) => i.roleCode)
                        .includes(role.roleCode)}
                      onChange={() => handleCheckboxChange(role)}
                    />
                    <label className="form-check-label" htmlFor={role.roleCode}>
                      {role.roleName}
                    </label>
                  </div>
                );
              })}
            </div>
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button
            variant="success"
            onClick={() => {
              handleUpdateUser();
            }}
            disabled={isYourself}
          >
            Update
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
