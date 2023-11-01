import React, { useEffect, useState } from 'react';
import { getShortUrlByShortLink, updateClickCount } from './../../services/ShortLinkService';

export default function RedirectPage() {
  const [isError, setIsError] = useState(false);

  useEffect(() => {
    async function fetchData() {
      const res = await getShortUrlByShortLink(window.location.pathname);
      if (res && res.data) {
        await updateClickCount(window.location.pathname);
        setIsError(false);
        window.location.replace(res.data.longUrl);
      } else {
        setIsError(true);
      }
    }
    fetchData();
  }, []);

  return (
    <div>
      <div className={'mt-5 pt-5'}>
        <h1 className="d-flex justify-content-center align-items-center fw-bolder">
          {isError === false ? 'Waiting ...' : 'The short link not exist'}
        </h1>
      </div>
    </div>
  );
}
