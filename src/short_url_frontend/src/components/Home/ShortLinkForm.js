import { QuestionCircleOutlined } from '@ant-design/icons';
import { useState } from 'react';
import { Form, FormGroup, Input, Label, Button } from 'reactstrap';

const ShortLinkForm = () => {
  const [longUrl, setLongUrl] = useState('');
  const [backHalf, setBackHalf] = useState('');
  console.log(process.env.URL_BACKEND);
  return (
    <div>
      <h2 className="fw-bolder mt-3">Shorten a long link</h2>
      <Form className="py-3" method="POST" action="" acceptCharset="UTF-8">
        <FormGroup>
          <Label for="longUrlInput">Paste a long URL</Label>
          <Input
            type="text"
            name="longUrlInput"
            id="longUrlInput"
            placeholder="Example: http://super-long-link.com/shorten-it"
            pattern="https://.*"
            required
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
          />
        </FormGroup>
        <Button className="bg-primary">Get your link</Button>
      </Form>
    </div>
  );
};

export default ShortLinkForm;
