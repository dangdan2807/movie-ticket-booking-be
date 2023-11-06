import React, { useEffect, useState } from 'react';
import {
  getShortUrlByShortLink,
  updateClickCount,
} from './../../services/ShortLinkService';

export default function RedirectPage() {
  const [message, setMessage] = useState('The short link not exist');

  useEffect(() => {
    async function fetchData() {
      const res = await getShortUrlByShortLink(window.location.pathname);
      if (res && res.data) {
        const shortUrl = res.data;
        if (shortUrl?.status) {
          await updateClickCount(window.location.pathname);
          setMessage('Waiting ...');
          window.location.replace(res.data.longUrl);
        } else {
          setMessage('The short link is not public');
        }
      } else {
        setMessage('Waiting ...');
      }
    }
    fetchData();
  }, []);

  return (
    <div>
      <div className={'mt-5 pt-5'}>
        <h1 className="d-flex justify-content-center align-items-center fw-bolder">
          {message}
        </h1>
      </div>
    </div>
  );
}
