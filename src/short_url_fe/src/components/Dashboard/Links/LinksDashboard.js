import { useEffect, useState } from 'react';
import { CheckOutlined } from '@ant-design/icons';
import dayjs from 'dayjs';

import './LinksDashboard.scss';
import ShortLinkItem from './ShortLinkItem';
import { getShortLinks } from '../../../services/ShortLinkService';

export default function LinksDashboard() {
  const currentDate = dayjs();

  const [shortLinks, setShortLinks] = useState([]);
  const [startDate, setStartDate] = useState(currentDate.startOf('day'));
  const [endDate, setEndDate] = useState(
    currentDate.add(1, 'day').endOf('day'),
  );
  const [filterShow, setFilterShow] = useState(true);

  const handlerChangeFilterShow = (filterShow) => {
    setFilterShow(filterShow);
  };

  useEffect(() => {
    async function fetchDate() {
      const res = await getShortLinks(startDate, endDate, filterShow);
      if (res && res.data) {
        setShortLinks(res.data);
      }
    }
    fetchDate();
  }, [startDate, endDate, filterShow]);

  return (
    <>
      <div className="row ms-2">
        <div className="col-12 border-bottom border-2">
          <h1 className="fw-bolder">Links</h1>
          <div className="d-flex align-items-center mb-3">
            <div className="links-dashboard__filter-bar__input-date input-group">
              <span className="input-group-text">Start date:</span>
              <input
                type="date"
                value={startDate.format('YYYY-MM-DD')}
                className="form-control"
                onChange={(e) => setStartDate(dayjs(e.target.value))}
              />
            </div>
            <div className="links-dashboard__filter-bar__input-date input-group ms-2">
              <span className="input-group-text">End date:</span>
              <input
                type="date"
                value={endDate.format('YYYY-MM-DD')}
                className="form-control"
                onChange={(e) => {
                  setEndDate(dayjs(e.target.value));
                }}
              />
            </div>
            <div className="input-group ms-2">
              <span className="input-group-text">Show:</span>
              <button
                type="button"
                className="btn btn-outline-secondary dropdown-toggle dropdown-toggle-split bg-white text-dark links-dashboard__filter-bar__btn-show"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <span className="visually me-1">
                  {filterShow === true ? 'Active' : 'Hidden'}
                </span>
              </button>
              <ul className="dropdown-menu">
                <li>
                  <span
                    className="dropdown-item d-flex align-items-center justify-content-between"
                    href=""
                    onClick={() => handlerChangeFilterShow(true)}
                  >
                    Action
                    <CheckOutlined
                      className={filterShow === true ? '' : 'd-none'}
                    />
                  </span>
                </li>
                <li>
                  <span
                    className="dropdown-item d-flex align-items-center justify-content-between"
                    href=""
                    onClick={() => handlerChangeFilterShow(false)}
                  >
                    Hidden
                    <CheckOutlined
                      className={filterShow === false ? '' : 'd-none'}
                    />
                  </span>
                </li>
              </ul>
            </div>
          </div>
        </div>
        {shortLinks.map((item) => (
          <div className="col-12" key={item.shortUrl}>
            <ShortLinkItem shortLink={item} />
          </div>
        ))}
      </div>
    </>
  );
}
