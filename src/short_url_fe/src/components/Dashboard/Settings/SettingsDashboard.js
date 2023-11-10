import React, { useContext } from 'react';

import { UserContext } from '../../../context/userContext';

export default function SettingsDashboard() {
  const { user } = useContext(UserContext);
  return (
    <>
      <div className="row ms-2">
        <div className="col-12">
          <div>
            <h3 className="fw-bolder">Profile</h3>
            <h4 className="fw-bolder">Preferences</h4>
          </div>
          <div className="my-4">
            <label for="fullNameInput" className="form-label fw-bolder">
              Display name
            </label>
            <input
              type="text"
              className="form-control"
              id="fullNameInput"
              value={user.name}
              placeholder="Your full name"
            />
            <button type="button" className="btn btn-primary mt-3">
              Update display name
            </button>
          </div>
          <div className="my-4">
            <label for="emailInput" className="form-label fw-bolder">
              Email address
            </label>
            <input
              type="text"
              className="form-control"
              id="emailInput"
              value={user.email}
              placeholder="Your email address"
            />
            <button type="button" className="btn btn-primary mt-3">
              Update email
            </button>
          </div>
          <div className="my-5">
            <h4 className="fw-bolder">Security & authentication</h4>
            <p className="fw-bolder mt-3 mb-1">Change password</p>
            <p className="mt-0">
              You will be required to login after changing your password
            </p>
            <label for="currentPasswordInput" className="form-label fw-bolder">
              Current password
            </label>
            <input
              type="text"
              className="form-control"
              id="currentPasswordInput"
              value={''}
              placeholder="Your current password"
            />
            <label for="newPasswordInput" className="form-label fw-bolder mt-3">
              New password
            </label>
            <input
              type="text"
              className="form-control"
              id="newPasswordInput"
              value={''}
              placeholder="Your new password"
            />
            <label
              for="confirmNewPasswordInput"
              className="form-label fw-bolder mt-3"
            >
              Confirm new password
            </label>
            <input
              type="text"
              className="form-control"
              id="confirmNewPasswordInput"
              value={''}
              placeholder="Your confirm new password"
            />
            <button type="button" className="btn btn-primary mt-3">
              Update email
            </button>
          </div>
        </div>
      </div>
    </>
  );
}
