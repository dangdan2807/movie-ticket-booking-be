import { useEffect, useState } from 'react';
import { BarChart } from '@mui/x-charts/BarChart';
import dayjs from 'dayjs';

import './Home.scss';
import { statisticsBase } from '../../../services/StatisticsService';
import { handleError } from '../../../lib/common';

export default function Home() {
  const currentYear = dayjs().year();
  const [selectedMonth, setSelectedMonth] = useState(dayjs().month());
  const [daysInMonth, setDaysInMonth] = useState([1]);
  const [dataDays, setDataDays] = useState([1]);
  const [totalUsers, setTotalUsers] = useState(0);
  const [totalShortUrls, setTotalShortUrls] = useState(0);

  useEffect(() => {
    async function fetchData() {
      const res = await statisticsBase();
      if (res && res) {
        setTotalUsers(res.data.totalUsers ?? 0);
        setTotalShortUrls(res.data.totalShortUrls ?? 0);
      } else {
        handleError(res, 'Error fetching data');
      }
    }
    fetchData();
  }, []);

  useEffect(() => {
    let arr = [];
    let value = [];
    for (let i = 1; i <= dayjs(`${currentYear}-${selectedMonth}-01`).daysInMonth(); i++) {
      arr.push(i);
      value.push(Math.floor(Math.random() * 1000));
    }
    setDaysInMonth(arr);
    setDataDays(value);
  }, [selectedMonth]);

  const renderData = [
    {
      title: 'users',
      data: totalUsers,
    },
    {
      title: 'links',
      data: totalShortUrls,
    },
    {
      title: 'Qr code',
      data: 0,
    },
    {
      title: 'link in bio',
      data: 0,
    },
  ];

  return (
    <>
      <div className="row row-cols-4">
        {renderData.map((item, index) => {
          return (
            <div className="" key={index}>
              <div className="w-100 bg-primary-subtle px-3 py-3 rounded">
                <h5 className="fw-bolder text-capitalize">{item.title}</h5>
                <p className="home__report-box__text fw-bolder">{item.data}</p>
              </div>
            </div>
          );
        })}
      </div>

      <div className='row row-cols-1 mt-3'>
        <h5>Statistics</h5>
      </div>
      <div className="row row-cols-3 justify-content-center">
        <div className="col">
          <select
            className="form-select"
            defaultValue={selectedMonth}
            value={selectedMonth}
            onChange={(e) => setSelectedMonth(e.target.value)}
          >
            <option value={1}>Jan, {currentYear}</option>
            <option value={2}>Feb, {currentYear}</option>
            <option value={3}>Mar, {currentYear}</option>
            <option value={4}>Apr, {currentYear}</option>
            <option value={5}>May, {currentYear}</option>
            <option value={6}>Jun, {currentYear}</option>
            <option value={7}>Jul, {currentYear}</option>
            <option value={8}>Aug, {currentYear}</option>
            <option value={9}>Sep, {currentYear}</option>
            <option value={10}>Oct, {currentYear}</option>
            <option value={11}>Nov, {currentYear}</option>
            <option value={12}>Dec, {currentYear}</option>
          </select>
        </div>
      </div>
      <div className="row row-cols-1 mt-1 w-100">
        <BarChart
          xAxis={[
            {
              id: 'barCategories',
              data: daysInMonth,
              scaleType: 'band',
            },
          ]}
          series={[
            {
              data: dataDays,
            },
          ]}
          width={1368}
          height={600}
        />
      </div>
    </>
  );
}
