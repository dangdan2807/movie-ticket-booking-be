import { Container } from 'reactstrap';
import VerticalMenu from './VerticalMenu/VerticalMenu';

export function LayoutDashboardAdmin(props) {

  return (
    <>
      <div className="row">
        <VerticalMenu />
        <div className="col-10">
          <Container tag="main">{props.children}</Container>
        </div>
      </div>
    </>
  );
}
