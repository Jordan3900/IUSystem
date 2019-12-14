import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import AuthorizeRoute from './components/api-authorization/AuthorizeRoute';
import ApiAuthorizationRoutes from './components/api-authorization/ApiAuthorizationRoutes';
import { ApplicationPaths } from './components/api-authorization/ApiAuthorizationConstants';
import { Teachers } from './components/teachers/Teachers';
import { MyStudents } from './components/students/Mystudents';
import { DetailsTeacher } from './components/teachers/teacher/DetailsTeacher';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;
 
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <AuthorizeRoute path='/teachers' component={Teachers} />
        <AuthorizeRoute path='/mystudents' component={MyStudents} />
        <AuthorizeRoute path='/teacher/:id' component={DetailsTeacher} />
        <Route path={ApplicationPaths.ApiAuthorizationPrefix} component={ApiAuthorizationRoutes} />
      </Layout>
    );
  }
}
