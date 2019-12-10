import React, { Component, Fragment } from 'react';
import authService from '../api-authorization/AuthorizeService'

export class Teachers extends Component {

  constructor(props) {
    super(props);
    this.state = { teachers: [], loading: true };
  }

  componentDidMount() {
    this.populateTeachersData();
  }

  static renderTeachers(teachers) {
    
      
    return (
      teachers.map((teacher, i) =>
          <div key={i}>
          <h1 className="text-white">{teacher.firstName}</h1>
          <h2 className="text-white">{teacher.lastName}</h2>
          </div>
      )
    );
  }

  render() {
    let contents = this.state.loading
      ? <p className="text-white"><em>Loading...</em></p>
      : Teachers.renderTeachers(this.state.teachers);

    return (
      <div>
        <h1 className="text-center text-white">Find your teacher</h1>
        {contents}
      </div>
    );
  }

  async populateTeachersData() {
    const token = await authService.getAccessToken();
    const response = await fetch('teacher', {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    console.log(data);
    debugger;
    this.setState({ teachers: data, loading: false });
  }
}
