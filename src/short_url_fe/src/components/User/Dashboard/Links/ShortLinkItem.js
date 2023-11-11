import { useState } from 'react';
import { Link } from 'react-router-dom';
import {
  CopyOutlined,
  EditOutlined,
  DeleteOutlined,
  EyeInvisibleOutlined,
  EyeOutlined,
  CalendarOutlined,
  BarChartOutlined,
} from '@ant-design/icons';
import {
  Dropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from 'reactstrap';
import dayjs from 'dayjs';
import { toast } from 'react-toastify';

import './ShortLinkItem.scss';
import { hiddenShortUrl } from '../../../../services/ShortLinkService';
import ModalConfirmDelete from '../../Modals/ModalConfirmDelete';

export default function ShortLinkItem(props) {
  const [dropdownOpen, setDropdownOpen] = useState(false);
  const { shortLink, reloadShortUrls } = props;
  const [isShowModalConfirmDelete, setIsShowModalConfirmDelete] =
    useState(false);
  const [selectItemDelete, setSelectItemDelete] = useState({});

  const toggle = () => setDropdownOpen((prevState) => !prevState);

  return (
    <>
      <div className="bg-white mt-2 d-flex justify-content-around align-items-start py-2 rounded-2">
        <div className="w-100 ms-3">
          <h5 className="fw-bolder">
            {shortLink?.title ? shortLink?.title : shortLink?.shortUrl}
          </h5>
          <p className="my-1">
            <Link
              className="text-decoration-none"
              to={`${window.location.protocol}//${window.location.host}/${
                shortLink?.shortUrl ? shortLink?.shortUrl : ''
              }`}
            >
              {`${window.location.protocol}//${window.location.host}/${
                shortLink?.shortUrl ? shortLink?.shortUrl : ''
              }`}
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
          <div className="mt-4 d-flex">
            <span className="short-link-item__text me-3 fw-medium d-flex align-items-center text-success">
              <BarChartOutlined className="me-2" />
              {shortLink?.clickCount ? shortLink?.clickCount : 0} engagements
            </span>
            <span className="short-link-item__text me-3 d-flex align-items-center">
              <CalendarOutlined className="me-2" />
              {shortLink?.createdAt
                ? ' ' + dayjs(shortLink?.createdAt).format('DD/MM/YYYY')
                : ' null'}
            </span>

            <span
              className={
                'short-link-item__text d-flex align-items-center ' +
                (shortLink?.status === false ? '' : 'd-none')
              }
            >
              <EyeInvisibleOutlined className="me-2" />
              Hidden
            </span>
          </div>
        </div>
        <div className="me-3 d-flex flex-column justify-content-end align-items-end">
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
                      const res = await hiddenShortUrl(shortLink);
                      if (res && res.data) {
                        toast.success(
                          `${
                            shortLink.status === true ? 'Hidden' : 'UnHidden'
                          } successfully`,
                        );
                      } else {
                        shortLink.status = !shortLink?.status;
                        toast.error(
                          `${
                            shortLink.status === true ? 'Hidden' : 'UnHidden'
                          } failed`,
                        );
                      }
                    }}
                  >
                    <div
                      className={
                        'd-flex align-items-center ' +
                        (shortLink?.status === true ? '' : 'd-none')
                      }
                    >
                      <EyeInvisibleOutlined className="me-2" />
                      Hidden
                    </div>
                    <div
                      className={
                        'd-flex align-items-center ' +
                        (shortLink?.status === true ? 'd-none' : '')
                      }
                    >
                      <EyeOutlined className="me-2" />
                      UnHidden
                    </div>
                  </DropdownItem>
                  <DropdownItem
                    className="short-link-item__dropdown"
                    onClick={async () => {
                      setIsShowModalConfirmDelete(true);
                      setSelectItemDelete(shortLink);
                    }}
                  >
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
      <ModalConfirmDelete
        show={isShowModalConfirmDelete}
        handleClose={() => setIsShowModalConfirmDelete(false)}
        shortLink={selectItemDelete}
        reloadShortUrls={() => reloadShortUrls()}
      />
    </>
  );
}
