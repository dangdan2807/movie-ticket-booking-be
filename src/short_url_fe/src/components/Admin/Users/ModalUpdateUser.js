import { useContext, useEffect, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { toast } from 'react-toastify';

import { updateUser, getUserById } from '../../../services/UserService';
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

  useEffect(() => {
    async function fetchData() {
      const res = await getUserById(userId);
      if (res && res.data) {
        setId(res.data.id);
        setEmail(res.data.email);
        setFullName(res.data.fullName);
        setAddress(res.data.address);
        setCreateAt(res.data.createAt);
        setUserStatus(res.data.status);
      } else {
        handleError(res, 'Get user failed');
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
        const resUpdate = await updateUser(
          userId,
          fullName,
          email,
          address,
          userStatus,
          resGet.data?.roles?.map((item) => item.roleId),
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
              value={userStatus === true ? 'Active' : 'Inactive'}
              onChange={(e) => setUserStatus(e.target.value === 'Active')}
            >
              <option value="Active">Active</option>
              <option value="Inactive">Inactive</option>
            </select>
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
          >
            Update
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
