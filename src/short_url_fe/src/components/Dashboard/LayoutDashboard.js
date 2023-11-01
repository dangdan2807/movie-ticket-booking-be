import React, { Component } from 'react';
import { Container } from 'reactstrap';
import VerticalMenu from '../VerticalMenu/VerticalMenu';

export class LayoutDashboard extends Component {
  static displayName = LayoutDashboard.name;

  render() {
    return (
      <>
        <div className="row">
          <VerticalMenu />
          <div className="col-10">
            <Container tag="main">{this.props.children}</Container>
          </div>
        </div>
      </>
    );
  }
}
