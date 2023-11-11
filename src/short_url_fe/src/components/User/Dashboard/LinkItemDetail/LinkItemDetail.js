import { useContext, useEffect, useState } from 'react';
import { LeftOutlined } from '@ant-design/icons';
import { useNavigate, Link } from 'react-router-dom';
import {
  CopyOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeInvisibleOutlined,
  EyeOutlined,
  // CalendarOutlined,
  // BarChartOutlined,
} from '@ant-design/icons';
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from 'reactstrap';

import './LinkItemDetail.scss';
import { UserContext } from '../../../context/userContext';
import {
  getShortUrlByShortLink,
  hiddenShortLink,
} from '../../../services/ShortLinkService';

export default function LinkItemDetail() {
  const navigate = useNavigate();
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const { setActiveItemVerticalMenu } = useContext(UserContext);
  const [shortLink, setShortLink] = useState(null);

  const toggle = () => setDropdownOpen((prevState) => !prevState);

  useEffect(() => {
    setActiveItemVerticalMenu('links');
    async function fetchData() {
      const pathname = window.location.pathname;
      const segments = pathname.split('/');
      const desiredSegment = pathname.split('/')[segments.length - 1];
      const res = await getShortUrlByShortLink('/' + desiredSegment);
      if (res && res.data) {
        setShortLink(res.data);
      }
    }
    fetchData();
  }, []);

  return (
    <>
      <div>
        <button
          type="button"
          className="btn btn-light d-flex align-items-center fw-medium mb-2"
          onClick={() => {
            navigate('/links');
          }}
        >
          <LeftOutlined className="me-2" /> Back to list
        </button>
      </div>
      <div className="bg-white rounded px-4 py-4 ">
        <div className="mt-3 d-flex justify-content-between border-bottom border-2">
          <div className="w-75">
            <h2 className="fw-bolder">{`${shortLink?.title ? shortLink?.title : shortLink?.shortUrl}`}</h2>
            <p className="link-item-detail__link mb-2 text-truncate">
              <a
                href={`${window.location.protocol}//${window.location.host}/${
                  shortLink?.shortUrl ? shortLink?.shortUrl : ''
                }`}
              >
                {`${window.location.protocol}//${window.location.host}/${
                  shortLink?.shortUrl ? shortLink?.shortUrl : ''
                }`}
              </a>
            </p>
            <p className="link-item-detail__link">
              <a
                href={`${shortLink?.longUrl ? shortLink?.longUrl : ''}`}
                className="text-black text-truncate d-block"
              >
                {`${shortLink?.longUrl ? shortLink?.longUrl : ''}`}
              </a>
            </p>
          </div>
          <div>
            <div className="d-flex align-items-center">
              <button
                type="button"
                className="short-link-item__btn btn btn-light d-flex align-items-center border"
                onClick={() =>
                  navigator.clipboard.writeText(
                    `${window.location.protocol}//${window.location.host}/${
                      shortLink?.shortUrl ? shortLink?.shortUrl : ''
                    }`,
                  )
                }
              >
                <CopyOutlined /> <span className="ms-1">Copy</span>
              </button>
              <Link
                to={shortLink?.shortUrl ? '' + shortLink?.shortUrl : 'null'}
                className="short-link-item__btn btn btn-light text-black ms-2 d-flex align-items-center border "
              >
                <EditOutlined />
              </Link>
              <div className={'dropdown ms-2 ' + (true ? '' : `d-none`)}>
                <Dropdown
                  isOpen={dropdownOpen}
                  toggle={toggle}
                  color="light"
                  className="border rounded"
                >
                  <DropdownToggle color="light" caret>
                    More
                  </DropdownToggle>
                  <DropdownMenu>
                    <DropdownItem
                      className="short-link-item__dropdown"
                      onClick={async () => {
                        shortLink.status = !shortLink?.status;
                        await hiddenShortLink(shortLink);
                      }}
                    >
                      <div
                        className={
                          'd-flex align-items-center ' +
                          (shortLink?.status === true ? '' : ' d-none')
                        }
                      >
                        <EyeInvisibleOutlined className="me-2" />
                        <EyeOutlined className="me-2" />
                        Hidden
                      </div>
                      <div
                        className={
                          'd-flex align-items-center ' +
                          (shortLink?.status === true ? ' d-none' : '')
                        }
                      >
                        <EyeOutlined className="me-2" />
                        UnHidden
                      </div>
                    </DropdownItem>
                    <DropdownItem className="short-link-item__dropdown">
                      <div className="d-flex align-items-center text-danger">
                        <DeleteOutlined className="me-2" />
                        Delete
                      </div>
                    </DropdownItem>
                  </DropdownMenu>
                </Dropdown>
              </div>
            </div>
          </div>
        </div>
        <div className="mt-3">2</div>
      </div>
    </>
  );
}
