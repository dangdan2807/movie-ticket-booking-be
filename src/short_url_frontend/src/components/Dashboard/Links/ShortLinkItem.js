import './ShortLinkItem.scss';
import { Link } from 'react-router-dom';

export default function ShortLinkItem(props) {
  const { shortLink } = props;
  return (
    <>
      <div className="bg-white mt-2 d-flex justify-content-around align-items-center py-2 rounded-2">
        <div class="form-check">
          <input class="form-check-input ms-1 me-4" type="checkbox" />
        </div>
        <div className="w-100">
          <h5 className="fw-bolder">
            <Link
              className="text-decoration-none text-black"
              to={shortLink?.shortUrl ? '' + shortLink?.shortUrl : 'null'}
            >
              {shortLink?.shortUrl ? shortLink?.shortUrl : 'null'}
            </Link>
          </h5>
          <p className="my-1">
            <Link
              className="text-decoration-none"
              to={shortLink?.shortUrl ? '' + shortLink?.shortUrl : 'null'}
            >
              {shortLink?.shortUrl ? shortLink?.shortUrl : 'null'}
            </Link>
          </p>
          <p className="my-1">
            <Link
              className="text-decoration-none text-black"
              to={shortLink?.longUrl ? shortLink?.longUrl : 'null'}
            >
              {shortLink?.longUrl ? shortLink?.longUrl : 'null'}
            </Link>
          </p>
          <div className="mt-4">
            <span className="me-3 fw-medium">
              {shortLink?.clickCount ? shortLink?.clickCount : 0} engagements
            </span>
            <span>
              create date:{' '}
              {shortLink?.createdAt ? shortLink?.createdAt : 'null'}
            </span>
          </div>
        </div>
        <div className="me-3">1</div>
      </div>
    </>
  );
}
