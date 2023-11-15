import { useEffect, useState, useContext } from 'react';
import ReactPaginate from 'react-paginate';
import { SearchOutlined, CheckOutlined, EditOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import './Users.scss';
import {
  getUsers,
  updateUser,
  getUserById,
} from '../../../services/UserService';
import { handleError } from '../../../lib/common';
import { toast } from 'react-toastify';
import { UserContext } from '../../../context/userContext';
import ModalUpdateUser from './ModalUpdateUser';

export default function User() {
  const [isShowModalUpdate, setIsShowModalUpdate] = useState(false);
  const [selectItemIdUpdate, setSelectItemIdUpdate] = useState(1);

  const currentDate = dayjs();
  const { user } = useContext(UserContext);

  const [users, setUsers] = useState([]);
  const [startDate, setStartDate] = useState(currentDate.startOf('day'));
  const [endDate, setEndDate] = useState(
    currentDate.add(1, 'day').endOf('day'),
  );
  const [userStatus, setUserStatus] = useState(true);
  const [keyword, setKeyword] = useState('');
  const [isUpdateUsers, setIsUpdateUsers] = useState(false);

  const [totalPages, setTotalPages] = useState(1);
  const [currentPage, setCurrentPage] = useState(0);

  const handlerChangeFilterShow = (filterShow) => {
    setUserStatus(filterShow);
  };

  const handlePageClick = async (event) => {
    const res = await getUsers(
      startDate,
      endDate,
      keyword,
      userStatus,
      event.selected + 1,
    );
    if (res && res.data) {
      const pagination = res.pagination;
      setTotalPages(Math.ceil(pagination.totalCount / pagination.pageSize));
      setCurrentPage(pagination.currentPage - 1);
      setUsers(res.data);
    } else {
      handleError(res, 'Get users failed');
    }
  };

  const handleFetchAPI = async () => {
    const res = await getUsers(startDate, endDate, keyword, userStatus);
    if (res && res.data) {
      const pagination = res.pagination;
      setTotalPages(Math.ceil(pagination.totalCount / pagination.pageSize));
      setCurrentPage(pagination.currentPage - 1);
      setUsers(res.data);
    }
  };

  const handleUpdate = async (userUpdate) => {
    const resGet = await getUserById(userUpdate.id);
    if (resGet && resGet.data) {
      if (userUpdate.id === user.id) {
        toast.error('You cannot perform this action on yourself');
      } else {
        const resUpdate = await updateUser(
          resGet.data.id,
          userUpdate.fullName,
          userUpdate.email,
          userUpdate.address,
          userUpdate.status,
          userUpdate?.roles?.map((item) => item.roleId) ??
            resGet.data?.roles?.map((item) => item.roleId),
        );
        if (resUpdate && resUpdate.data) {
          toast.success('Update user successfully');
          handleFetchAPI();
        } else {
          handleError(resUpdate, 'update user failed');
        }
      }
    } else {
      handleError(resGet, 'user not found');
    }
  };

  useEffect(() => {
    handleFetchAPI();
  }, [startDate, endDate, userStatus, isUpdateUsers]);

  return (
    <>
      <div className="row ms-2">
        <div className="col-12 border-bottom border-2">
          <h1 className="fw-bolder">Users</h1>
          <div className="d-flex justify-content-between align-items-end mb-3">
            <div className="users__filter-bar__input-date">
              <span className="fw-bolder">Start date</span>
              <input
                type="date"
                value={startDate.format('YYYY-MM-DD')}
                className="form-control mt-2"
                onChange={(e) => setStartDate(dayjs(e.target.value))}
              />
            </div>
            <div className="users__filter-bar__input-date ms-2">
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
                className="btn btn-outline-secondary dropdown-toggle dropdown-toggle-split bg-white text-dark users__filter-bar__btn-show mt-2"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <span className="visually me-1">
                  {userStatus === true ? 'Active' : 'Inactive'}
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
                      className={userStatus === true ? '' : 'd-none'}
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
                      className={userStatus === false ? '' : 'd-none'}
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
                onClick={() => handleFetchAPI()}
              >
                <SearchOutlined />
              </button>
            </div>
          </div>
        </div>
        <table className="table">
          <thead>
            <tr>
              <th scope="col">ID</th>
              <th scope="col">Full name</th>
              <th scope="col">Email</th>
              <th scope="col">Address</th>
              <th scope="col">Status</th>
              <th scope="col">Create at</th>
              <th scope="col" className="">
                Action
              </th>
            </tr>
          </thead>
          <tbody>
            {users.map((item) => (
              <tr key={item.id}>
                <th scope="row">{item.id}</th>
                <td>{item.fullName}</td>
                <td>{item.email}</td>
                <td>{item.address}</td>
                <td>
                  <span
                    className={
                      'text-white p-1 rounded-pill d-flex align-items-center justify-content-center w-75 ' +
                      (item.status === true ? 'bg-success' : 'bg-danger')
                    }
                  >
                    {item.status === true ? 'active' : 'inactive'}
                  </span>
                </td>
                <td>
                  {item?.createAt
                    ? ' ' + dayjs(item?.createAt).format('DD/MM/YYYY')
                    : ' null'}
                </td>
                <td className="d-flex align-items-center">
                  <button
                    type="button"
                    className="btn btn-info px-3 py-2 d-flex align-items-center justify-content-center text-white"
                    onClick={async () => {
                      setIsShowModalUpdate(true);
                      setSelectItemIdUpdate(item.id);
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
      <ModalUpdateUser
        show={isShowModalUpdate}
        handleClose={() => setIsShowModalUpdate(false)}
        userId={selectItemIdUpdate}
        reloadShortUrls={() => setIsUpdateUsers(!isUpdateUsers)}
      />
    </>
  );
}
