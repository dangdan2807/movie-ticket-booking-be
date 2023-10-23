import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu/index';

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div className=''>
        <NavMenu />
        <Container tag="main">
          {this.props.children}
        </Container>
      </div>
    );
  }
}
