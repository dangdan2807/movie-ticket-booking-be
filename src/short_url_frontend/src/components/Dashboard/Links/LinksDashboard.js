import { useEffect, useState } from 'react';
import './LinksDashboard.scss';
import ShortLinkItem from './ShortLinkItem';
import { getShortLinks } from '../../../services/ShortLinkService';

export default function LinksDashboard() {
  const [shortLinks, setShortLinks] = useState([]);

  useEffect(() => {
    async function fetchDate() {
      const res = await getShortLinks();
      if (res && res.data) {
        setShortLinks(res.data);
        console.log(res.data);
      }
    }
    fetchDate();
  }, []);

  return (
    <>
      <div className="row ms-2">
        <div className="col-12 border-bottom">
          <h1 className="fw-bolder">Links</h1>
        </div>
        {shortLinks.map((item) => (
          <div className="col-12">
            <ShortLinkItem shortLink={item} />
          </div>
        ))}
      </div>
    </>
  );
}
