import React, { Component } from 'react';
import { Jumbotron, Button } from 'reactstrap';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
      <Jumbotron>
        <h1 className="display-3">Welcome to IUS</h1>
        <p className="lead">This is a Information University System</p>
        <hr className="my-2" />
        <p>This application is created to help student and teachers.</p>
        <p className="lead">
          <Button color="primary">Learn More</Button>
        </p>
      </Jumbotron>
    </div>
    );
  }
}
