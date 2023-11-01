import { useState, useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { QuestionCircleOutlined } from '@ant-design/icons';
import { Form, FormGroup, Input, Label, Button } from 'reactstrap';
import { toast } from 'react-toastify';

import { createShortUrl } from './../../services/ShortLinkService';
import { UserContext } from '../../context/userContext';

const ShortLinkForm = () => {
  const navigate = useNavigate();
  const { user } = useContext(UserContext);

  const [longUrl, setLongUrl] = useState('');
  const [backHalf, setBackHalf] = useState('');
  const [shortLink, setShortLink] = useState('');
  const [isCreateSuccess, setIsCreateSuccess] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
    if (user && user.auth === true) {
      if (!longUrl.trim()) {
        toast.error('Long url is require');
        return;
      }
      let urlRegex =
        /(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?\/[a-zA-Z0-9]{2,}|((https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z]{2,}(\.[a-zA-Z]{2,})(\.[a-zA-Z]{2,})?)|(https:\/\/www\.|http:\/\/www\.|https:\/\/|http:\/\/)?[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}\.[a-zA-Z0-9]{2,}(\.[a-zA-Z0-9]{2,})?/g;
      if (!urlRegex.test(longUrl)) {
        toast.error('Invalid long url');
        return;
      }
      switch (backHalf) {
        case 'link':
        case 'dashboard':
          toast.error('back half is exist');
          return
        default:
          break;
      }

      setIsCreateSuccess(false);
      const res = await createShortUrl(longUrl, backHalf);
      if (res && res.data) {
        setIsCreateSuccess(true);
        setShortLink(res.data.shortUrl);
        toast.success('create short link success');
      } else {
        const errRes = res.response?.data;
        if (errRes !== undefined) {
          toast.error(errRes.message);
        } else {
          toast.error('Create short link failed');
        }
      }
    } else {
      navigate('/login');
    }
  };

  return (
    <div>
      <h2 className="fw-bolder mt-3">Shorten a long link</h2>
      <Form
        className="py-3"
        acceptCharset="UTF-8"
        onSubmit={async (e) => await handleSubmit(e)}
      >
        <FormGroup>
          <Label for="longUrlInput">Paste a long URL</Label>
          <Input
            type="text"
            name="longUrlInput"
            id="longUrlInput"
            placeholder="Example: http://super-long-link.com/shorten-it"
            onChange={(e) => setLongUrl(`${e.target.value}`)}
          />
        </FormGroup>
        <FormGroup>
          <Label for="backHalfInput">
            <div className="d-flex align-items-center">
              Enter a back-half (optional)
              <QuestionCircleOutlined className="ps-2" />
            </div>
          </Label>
          <Input
            type="text"
            name="backHalfInput"
            id="backHalfInput"
            placeholder="Example: favorite-link"
            onChange={(e) => setBackHalf(`${e.target.value}`)}
          />
        </FormGroup>
        <Button className="bg-primary">
          {user && user.auth === true
            ? 'Get your link'
            : 'Login and create short link'}
        </Button>
        <FormGroup
          className={'mt-3 ' + (isCreateSuccess === '1' ? '' : 'd-none')}
        >
          <Label>Your short link: </Label>
          <Link
            className="ps-1"
            hrefLang={isCreateSuccess === '1' ? '/' + shortLink : ''}
          >
            {isCreateSuccess === '1' ? shortLink : ''}
          </Link>
        </FormGroup>
      </Form>
    </div>
  );
};

export default ShortLinkForm;
