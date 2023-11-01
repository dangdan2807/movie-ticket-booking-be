import React from 'react';
import { Container } from 'reactstrap';
import { Header } from './Header/Header';

export function Layout(props) {
  return (
    <div className="bg-body-tertiary">
      <Header />
      <Container tag="main">{props.children}</Container>
    </div>
  );
}
