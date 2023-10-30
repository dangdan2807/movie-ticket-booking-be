import React, { useState } from 'react';
import {
  TabContent,
  TabPane,
  Nav,
  NavItem,
  NavLink,
  Row,
  Col,
} from 'reactstrap';
import { LinkOutlined, QrcodeOutlined } from '@ant-design/icons';
import ShortLinkForm from './ShortLinkForm';
import './Home.css';

export function Home() {
  const [currentActiveTab, setCurrentActiveTab] = useState('1');

  return (
    <div>
      <div className="d-flex flex-column align-items-center">
        <h1 className="fs-1-xl-title fs-1-md-title fw-bold mt-4">
          Make every <span className="text-primary-color"> connection </span>{' '}
          count
        </h1>
        <div className="w-75">
          <p className="fs-4 text-secondary text-center">
            Create short links, QR Codes, and Link-in-bio pages. Share them
            anywhere. <br /> Track what’s working, and what’s not. All inside
            the
            <span className="fw-bolder"> Short Link Platform.</span>
          </p>
        </div>
      </div>
      <div className="d-flex justify-content-center mt-4">
        <div className="w-75 fs-5 fw-medium">
          <Nav tabs className="d-flex justify-content-center">
            <NavItem
              className={
                'cursor-pointer ' +
                (currentActiveTab === '1'
                  ? 'active border border-bottom-0 rounded'
                  : '')
              }
            >
              <NavLink
                className={
                  'text-body ' + (currentActiveTab === '1' ? 'active' : '')
                }
                onClick={() => {
                  setCurrentActiveTab('1');
                }}
              >
                <div className="d-flex align-items-center">
                  <LinkOutlined className="px-2" />
                  Short Link
                </div>
              </NavLink>
            </NavItem>
            <NavItem
              className={
                'cursor-pointer ' +
                (currentActiveTab === '2'
                  ? 'active border border-bottom-0 rounded'
                  : '')
              }
            >
              <NavLink
                className={
                  'text-body ' + (currentActiveTab === '2' ? 'active' : '')
                }
                onClick={() => {
                  setCurrentActiveTab('2');
                }}
              >
                <div className="d-flex align-items-center">
                  <QrcodeOutlined className="px-2" />
                  QR Code
                </div>
              </NavLink>
            </NavItem>
          </Nav>
          <TabContent
            activeTab={currentActiveTab}
            className="border border-2 rounded py-1 px-5"
          >
            <TabPane tabId="1">
              <Row>
                <Col sm="12" className="py-2">
                  <ShortLinkForm />
                </Col>
              </Row>
            </TabPane>
            <TabPane tabId="2">
              <Row>
                <Col sm="12" className="py-2">
                  <h5>Sample Tab 2 Content</h5>
                </Col>
              </Row>
            </TabPane>
          </TabContent>
        </div>
      </div>
    </div>
  );
}
