import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

import './HomeDashboard.scss';

export default function HomeDashboard() {
  const navigate = useNavigate();

  const handleGoToLinks = () => {
    navigate('/links');
  };
  const handleGoToComingSoon = () => {
    toast.info('Coming soon ...');
  };

  const bioRenderData = [
    {
      title: 'Make it short',
      imageUrl: 'https://app.bitly.com/s/bbt2/images/dashboard_links.png',
      btnText: 'Go to Links',
      funBtn: () => handleGoToLinks(),
    },
    {
      title: 'Make it scannable',
      imageUrl: 'https://app.bitly.com/s/bbt2/images/dashboard_qrcs.png',
      btnText: 'Go to QR Codes',
      funBtn: () => handleGoToComingSoon(),
    },
    {
      title: 'Make a page',
      imageUrl: 'https://app.bitly.com/s/bbt2/images/dashboard_lib.png',
      btnText: 'Go to Link-in-bio',
      funBtn: () => handleGoToComingSoon(),
    },
  ];

  return (
    <>
      <div className="fs-2 fw-bolder">Your Connections Platform</div>
      <div className="mt-3 bg-white border border-0 rounded-3 py-3 d-flex align-items-center px-4 row">
        {bioRenderData.map((item, index) => {
          return (
            <div className="col-4 px-0" key={index}>
              <div className={`home-dashboard__card card mx-2`}>
                <div className="row g-0">
                  <div className="home-dashboard__card-child col-md-6 d-flex align-items-center justify-content-center bg-info-subtle">
                    <img
                      src={item.imageUrl}
                      className="img-fluid rounded-start home-dashboard__img"
                      alt={'img_home_' + index}
                    />
                  </div>
                  <div className="col-md-6 home-dashboard__card-child">
                    <div className="card-body d-flex flex-column justify-content-around">
                      <h5 className="card-title home-dashboard__card-title fw-bolder text-center">
                        {item.title}
                      </h5>
                      <button
                        type="button"
                        className="btn btn-outline-primary home-dashboard__card-btn"
                        onClick={item.funBtn}
                      >
                        {item.btnText}
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          );
        })}
      </div>
      <div className="row mt-4">
        <div className="col-6 ps-0">
          <div className="bg-white border border-0 rounded-3 py-3 px-4">
            <div className="">
              <p className="fw-bolder">Usage this month</p>
            </div>
            <div className="my-4">
              <div className="d-flex align-items-center justify-content-between">
                <p className="home-dashboard__report__text">Short links</p>
                <p className="fw-bolder home-dashboard__report__text">
                  2 of 10000 used
                </p>
              </div>
              <div
                className="progress"
                role="progressbar"
                aria-label="Basic example"
                aria-valuenow="2"
                aria-valuemin="0"
                aria-valuemax="10000"
              >
                <div className="progress-bar" style={{ width: '0.02%' }}></div>
              </div>
            </div>
            <div className="my-4">
              <div className="d-flex align-items-center justify-content-between">
                <p className="home-dashboard__report__text">QR codes</p>
                <p className="fw-bolder home-dashboard__report__text">
                  1 of 2 used
                </p>
              </div>
              <div
                className="progress"
                role="progressbar"
                aria-label="Basic example"
                aria-valuenow="1"
                aria-valuemin="0"
                aria-valuemax="2"
              >
                <div className="progress-bar" style={{ width: '50%' }}></div>
              </div>
            </div>
            <div className="my-4">
              <div className="d-flex align-items-center justify-content-between">
                <p className="home-dashboard__report__text">Link-in-bio</p>
                <p className="fw-bolder home-dashboard__report__text">
                  0 of 1 used
                </p>
              </div>
              <div
                className="progress"
                role="progressbar"
                aria-label="Basic example"
                aria-valuenow="0"
                aria-valuemin="0"
                aria-valuemax="1"
              >
                <div className="progress-bar" style={{ width: '0%' }}></div>
              </div>
            </div>
          </div>
        </div>
        <div className="col-6 px-0">
          <div
            className="card bg-white border border-0 rounded-3 py-3 px-4 w-100"
            style={{ width: '18rem' }}
          >
            <div className="home-dashboard__report__card w-100 d-flex justify-content-center">
              <img
                src="https://app.bitly.com/s/bbt2/images/qr-code-customisations_hero.png"
                className="card-img-top fw-bolder home-dashboard__report__card__img"
                alt="..."
              />
            </div>
            <div className="card-body">
              <h5 className="card-title">
                QR Code Makeover: Ignite Your Brand With Our Enhanced
                Customization Options
              </h5>
              <a
                href="https://bitly.com/blog/qr-code-customization-features/"
                className="btn btn-primary"
              >
                Read blog post
              </a>
            </div>
          </div>
        </div>
      </div>
      <div className="row my-4 bg-white border border-0 rounded-3 py-3 px-0">
        <div className="col-12 d-flex align-items-center justify-content-center">
          <div class="card border border-0 mb-2" style={{ maxWidth: '800px' }}>
            <div class="row g-0">
              <div class="col-md-6">
                <img
                  src="https://app.bitly.com/s/bbt2/images/dashboard-empty-state.png"
                  class="img-fluid rounded-start"
                  alt="..."
                />
              </div>
              <div class="col-md-6">
                <div class="card-body">
                  <h5 class="card-title">Every click tells a story</h5>
                  <p class="card-text">
                    Upgrade for a snapshot of your click and scan data in a
                    single dashboard. Get click metrics by location, device,
                    referrers, and more. View plans Learn more
                  </p>
                  <p class="card-text">
                    <a
                      class="btn btn-primary fw-semibold"
                      href="http://localhost:44481/dashboard"
                      role="button"
                      target="_blank"
                    >
                      View plans
                    </a>
                    <a
                      class="btn ms-2 text-primary fw-semibold"
                      href="http://localhost:44481/dashboard"
                      role="button"
                      target="_blank"
                    >
                      Learn more
                    </a>
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
