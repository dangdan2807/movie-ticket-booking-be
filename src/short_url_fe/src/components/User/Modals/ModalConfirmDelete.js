import { Button, Modal } from 'react-bootstrap';
import { toast } from 'react-toastify';

import { deleteShortUrlByShortLink } from '../../../services/ShortLinkService';

export default function ModalConfirmDelete(props) {
  const { show, handleClose, shortLink, reloadShortUrls } = props;
  const confirmDelete = async () => {
    const res = await deleteShortUrlByShortLink(shortLink?.shortUrl);
    if (res && res.statusCode === 204) {
      toast.success(`Delete successfully`);
      reloadShortUrls();
      handleClose();
    } else {
      toast.error(`Delete failed`);
    }
  };

  return (
    <>
      <Modal
        show={show}
        onHide={handleClose}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>Confirm delete short url</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          This action can't be undo!
          <br />
          Do want to delete this short url:{' '}
          <span className="fw-bolder">
            {shortLink?.title ? shortLink?.title : 'null'}
          </span>
          ?
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button
            variant="danger"
            onClick={() => {
              confirmDelete();
            }}
          >
            Delete
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
}
