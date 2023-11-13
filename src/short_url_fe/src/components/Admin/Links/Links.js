import { useEffect, useState, useContext } from 'react';
import ReactPaginate from 'react-paginate';
import { SearchOutlined, CheckOutlined, EditOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import './Links.scss';
import {
  getShortLinks,
  getShortUrlByShortLink,
  updateShortLink,
} from '../../../services/ShortLinkService';
import { handleError } from '../../../lib/common';
import { toast } from 'react-toastify';
import { UserContext } from '../../../context/userContext';
import ModalUpdateLink from './ModalUpdateLink';

export default function Links() {
  const [isShowModalUpdate, setIsShowModalUpdate] = useState(false);
  const [selectItemUpdate, setSelectItemUpdate] = useState(1);

  const currentDate = dayjs();
  const { user } = useContext(UserContext);

  const [links, setLinks] = useState([]);
  const [startDate, setStartDate] = useState(currentDate.startOf('day'));
  const [endDate, setEndDate] = useState(
    currentDate.add(1, 'day').endOf('day'),
  );
  const [status, setStatus] = useState(true);
  const [keyword, setKeyword] = useState('');
  const [isUpdateUsers, setIsUpdateUsers] = useState(false);

  const [totalPages, setTotalPages] = useState(1);
  const [currentPage, setCurrentPage] = useState(0);

  const handlerChangeFilterShow = (filterShow) => {
    setStatus(filterShow);
  };

  const handlePageClick = async (event) => {
    const res = await getShortLinks(
      startDate,
      endDate,
      status,
      event.selected + 1,
      10,
      'ASC',
      keyword,
    );
    if (res && res.data) {
      const pagination = res.pagination;
      setTotalPages(Math.ceil(pagination.totalCount / pagination.pageSize));
      setCurrentPage(pagination.currentPage - 1);
      setLinks(res.data);
    } else {
      handleError(res, 'Get users failed');
    }
  };

  const handleFetchAPI = async () => {
    const res = await getShortLinks(
      startDate,
      endDate,
      status,
      currentPage + 1,
      10,
      'ASC',
      keyword,
    );
    if (res && res.data) {
      const pagination = res.pagination;
      setTotalPages(Math.ceil(pagination.totalCount / pagination.pageSize));
      setCurrentPage(pagination.currentPage - 1);
      setLinks(res.data);
    }
  };

  const handleUpdate = async (linkUpdate) => {
    const resGet = await getShortUrlByShortLink('/' + linkUpdate.shortUrl);
    if (resGet && resGet.data) {
      const resUpdate = await updateShortLink(linkUpdate);
      if (resUpdate && resUpdate.data) {
        toast.success('Update short url successfully');
        handleFetchAPI();
      } else {
        handleError(resUpdate, 'update short url failed');
      }
    } else {
      handleError(resGet, 'short url not found');
    }
  };

  useEffect(() => {
    handleFetchAPI();
  }, [startDate, endDate, status, isUpdateUsers]);

  return (
    <>
      <div className="row ms-2">
        <div className="col-12 border-bottom border-2">
          <h1 className="fw-bolder">Users</h1>
          <div className="d-flex justify-content-between align-items-end mb-3">
            <div className="links__filter-bar__input-date">
              <span className="fw-bolder">Start date</span>
              <input
                type="date"
                value={startDate.format('YYYY-MM-DD')}
                className="form-control mt-2"
                onChange={(e) => setStartDate(dayjs(e.target.value))}
              />
            </div>
            <div className="links__filter-bar__input-date ms-2">
              <span className="fw-bolder">End date</span>
              <input
                type="date"
                value={endDate.format('YYYY-MM-DD')}
                className="form-control mt-2"
                onChange={(e) => {
                  setEndDate(dayjs(e.target.value));
                }}
              />
            </div>
            <div className="ms-2 ">
              <span className="fw-bolder">Status:</span>
              <button
                type="button"
                className="btn btn-outline-secondary dropdown-toggle dropdown-toggle-split bg-white text-dark links__filter-bar__btn-show mt-2"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <span className="visually me-1">
                  {status === true ? 'Active' : 'Inactive'}
                </span>
              </button>
              <ul className="dropdown-menu">
                <li>
                  <span
                    className="dropdown-item d-flex align-items-center justify-content-between"
                    href=""
                    onClick={() => handlerChangeFilterShow(true)}
                  >
                    Active
                    <CheckOutlined
                      className={status === true ? '' : 'd-none'}
                    />
                  </span>
                </li>
                <li>
                  <span
                    className="dropdown-item d-flex align-items-center justify-content-between"
                    href=""
                    onClick={() => handlerChangeFilterShow(false)}
                  >
                    Inactive
                    <CheckOutlined
                      className={status === false ? '' : 'd-none'}
                    />
                  </span>
                </li>
              </ul>
            </div>
            <div className="input-group ms-2">
              <input
                type="text"
                className="form-control"
                onChange={(e) => setKeyword(e.target.value)}
                onKeyDown={(e) => {
                  if (e.key === 'Enter') {
                    handleFetchAPI();
                  }
                }}
              />
              <button
                type="button"
                className="btn btn-info d-flex justify-content-center align-items-center"
                onClick={() => {
                  setCurrentPage(0);
                  handleFetchAPI();
                }}
              >
                <SearchOutlined />
              </button>
            </div>
          </div>
        </div>
        <table className="table table-striped">
          <thead>
            <tr>
              <th scope="col">STT</th>
              <th scope="col">Title</th>
              <th scope="col">Short url</th>
              <th scope="col" className="text-truncate">
                Long url
              </th>
              <th scope="col">Status</th>
              <th scope="col">Click</th>
              <th scope="col">User id</th>
              <th scope="col">Create at</th>
              <th scope="col">Action</th>
            </tr>
          </thead>
          <tbody>
            {links.map((item, index) => (
              <tr key={item.shortUrl}>
                <th scope="row">{currentPage * 10 + (index + 1)}</th>
                <td className="text-truncate">{item.title}</td>
                <td>
                  <a
                    href={`${window.location.protocol}//${
                      window.location.host
                    }/${item?.shortUrl ? item?.shortUrl : ''}`}
                  >
                    {item.shortUrl}
                  </a>
                </td>
                <td className="text-truncate">
                  <a href={`${item?.longUrl ? item?.longUrl : ''}`}>
                    {new URL(item.longUrl).hostname.replace(/^www\./, '')}
                  </a>
                </td>
                <td>
                  <span
                    className={
                      'text-white py-1 px-2 rounded-pill d-flex align-items-center justify-content-center w-75 ' +
                      (item.status === true ? 'bg-success' : 'bg-danger')
                    }
                  >
                    {item.status === true ? 'active' : 'inactive'}
                  </span>
                </td>
                <td>{item.clickCount}</td>
                <td>{item.userId}</td>
                <td>
                  {item?.createdAt
                    ? ' ' + dayjs(item?.createdAt).format('DD/MM/YYYY')
                    : ' null'}
                </td>
                <td className="d-flex align-items-center">
                  <button
                    type="button"
                    className="btn btn-info px-3 py-2 d-flex align-items-center justify-content-center text-white"
                    onClick={async () => {
                      setIsShowModalUpdate(true);
                      setSelectItemUpdate(item.shortUrl);
                    }}
                  >
                    <EditOutlined />
                  </button>
                  <button
                    type="button"
                    className={
                      'btn px-3 py-1 ms-1 d-flex align-items-center justify-content-center ' +
                      (item.status === true ? 'btn-danger' : 'btn-success')
                    }
                    onClick={async () => {
                      await handleUpdate({ ...item, status: !item.status });
                    }}
                  >
                    {item.status !== true ? 'active' : 'inactive'}
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

        <div className="mt-3 d-flex justify-content-center">
          <ReactPaginate
            onPageChange={handlePageClick}
            pageRangeDisplayed={3}
            marginPagesDisplayed={1}
            pageCount={totalPages}
            forcePage={currentPage}
            renderOnZeroPageCount={null}
            previousLabel="< previous"
            breakLabel="..."
            nextLabel="next >"
            pageClassName="page-item"
            pageLinkClassName="page-link"
            previousClassName="page-item"
            previousLinkClassName="page-link"
            nextClassName="page-item"
            nextLinkClassName="page-link"
            breakClassName="page-item"
            breakLinkClassName="page-link"
            containerClassName="pagination"
            activeClassName="active"
          />
        </div>
      </div>
      <ModalUpdateLink
        show={isShowModalUpdate}
        handleClose={() => setIsShowModalUpdate(false)}
        userId={selectItemUpdate}
        reloadShortUrls={() => setIsUpdateUsers(!isUpdateUsers)}
      />
    </>
  );
}
