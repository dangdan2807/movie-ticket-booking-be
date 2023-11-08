import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';

import './CreateLink.scss';
import {
  createShortUrl,
  getShortLinks,
} from './../../../../services/ShortLinkService';

export default function CreateLink() {
  const navigate = useNavigate();

  const [longUrl, setLongUrl] = useState('');
  const [title, setTitle] = useState('');
  const [shortUrl, setShortUrl] = useState('');
  const [messageLongUrl, setMessageLongUrl] = useState('');
  const [messageTitle, setMessageTitle] = useState('');
  const [messageShortUrl, setMessageShortUrl] = useState('');

  const handleAdd = async () => {
    let flag = false;
    if (longUrl.length <= 0) {
      flag = true;
      setMessageLongUrl('long url is required');
    }
    let urlRegex =
      /(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})/g;
    if (!urlRegex.test(longUrl)) {
      flag = true;
      setMessageLongUrl('Invalid long url');
    }
    if (longUrl.length >= 256) {
      flag = true;
      setMessageLongUrl('long url must be less than 255 characters');
    }
    if (longUrl.length >= 256) {
      flag = true;
      setMessageTitle('title must be less than 255 characters');
    }
    if (shortUrl.length >= 256) {
      flag = true;
      setMessageShortUrl('short url must be less than 255 characters');
    }
    if (title.length <= 0) {
      if (shortUrl.length > 0) {
        setTitle(shortUrl);
      }
    }
    if (title.length >= 256) {
      flag = true;
      setMessageTitle('title must be less than 255 characters');
    }
    switch (shortUrl) {
      case 'link':
      case 'links':
      case 'dashboard':
      case 'settings':
      case 'qr-codes':
      case 'link-in-bio':
      case 'links/create':
        flag = true;
        setMessageShortUrl('short url is exist');
        return;
      default:
        break;
    }
    if (!flag) {
      const res = await createShortUrl(longUrl, shortUrl, title);
      if (res && res.data) {
        navigate('/links');
      } else {
        const resErr = res?.response.data.errors;
        const errorMessages = [];

        for (const field in resErr) {
          if (resErr.hasOwnProperty(field)) {
            errorMessages.push(resErr[field][0]);
          }
        }

        const resultString = errorMessages.join(', ');
        toast.error(resultString);
      }
    }
  };

  return (
    <>
      <div>
        <h2 className="fw-bolder">Create new</h2>
        <div class="mb-3 mt-4">
          <label for="long-url" class="form-label fw-bolder">
            Long url
          </label>
          <input
            type="url"
            class={
              'form-control ' + (messageLongUrl.length > 0 ? 'is-invalid' : '')
            }
            id="long-url"
            placeholder="https://example.com/my-long-url"
            onChange={(e) => {
              setMessageLongUrl('');
              setLongUrl(e.target.value);
            }}
          />
          <div class="invalid-feedback">{messageLongUrl}</div>
        </div>
        <div class="mb-3 border-2 border-bottom pb-4">
          <label for="title" class="form-label">
            <span className="fw-bolder">Title </span>
            (optional)
          </label>
          <input
            type="text"
            class={
              'form-control ' + (messageTitle.length > 0 ? 'is-invalid' : '')
            }
            id="title"
            placeholder="title"
            value={title}
            onChange={(e) => {
              setMessageTitle('');
              setTitle(e.target.value);
            }}
          />
          <div class="invalid-feedback">{messageTitle}</div>
        </div>
        <h4 className="fw-bolder">Ways to share</h4>
        <h5 className="fw-bolder mt-3">Short link</h5>
        <div className="mb-3 mt-3 d-flex">
          <div className="w-25 me-3">
            <label for="selectHost" class="form-label fw-bolder">
              Domain <i class="fa-solid fa-lock ms-1"></i>
            </label>
            <select class="form-select" id="selectHost" disabled>
              <option value="1" selected>
                {window.location.host}
              </option>
            </select>
          </div>
          <div className="w-75">
            <label for="customBackHalf" class="form-label">
              <span className="fw-bolder">Custom back-half </span>
              (optional)
            </label>
            <input
              type="email"
              class={
                'form-control ' +
                (messageShortUrl.length > 0 ? 'is-invalid' : '')
              }
              id="customBackHalf"
              placeholder="custom-back-half"
              onChange={(e) => {
                setMessageShortUrl('');
                setShortUrl(e.target.value);
              }}
            />
            <div class="invalid-feedback">{messageShortUrl}</div>
          </div>
        </div>
        <div className="mb-3">
          <label for="checkQR" class="form-label ">
            <span className="fw-bolder">QR Code </span>(optional)
          </label>
          <div class="form-check form-switch">
            <input
              class="form-check-input"
              type="checkbox"
              role="switch"
              id="checkQR"
              disabled
            />
            <label class="form-check-label" for="checkQR">
              Generate a QR Code to share anywhere people can scan it
            </label>
          </div>
        </div>
        <div className="mb-4">
          <label for="checkLinkInBio" class="form-label ">
            <span className="fw-bolder">Link-in-bio </span>(optional)
          </label>
          <div class="form-check form-switch">
            <input
              class="form-check-input"
              type="checkbox"
              role="switch"
              id="checkLinkInBio"
              disabled
            />
            <label class="form-check-label" for="checkLinkInBio">
              Add this link to your Link-in-bio page for people to easily find
            </label>
          </div>
        </div>
        <div className="mb-3 d-flex align-items-center createLink__thumbnail p-4">
          <div>
            <img
              src="https://app.bitly.com/s/bbt2/images/launchpad/intro.svg"
              alt=""
              className="createLink__thumbnail__img"
            />
          </div>
          <div className="ms-4">
            <p className="fw-bolder">
              Create a Link-in-bio page to use this feature
            </p>
            <p>
              Display your most important links on one simple page. Then share
              one simple Link-in-bio to get people there.
              <a
                className="ms-1"
                href="https://app.bitly.com/Bi8g3UqQuDV/launchpads/new"
              >
                Learn more
              </a>
            </p>
          </div>
        </div>
        <div className="mb-3 d-flex justify-content-end">
          <button
            type="button"
            class="btn btn-primary px-5"
            onClick={() => handleAdd()}
          >
            Add
          </button>
        </div>
      </div>
    </>
  );
}
