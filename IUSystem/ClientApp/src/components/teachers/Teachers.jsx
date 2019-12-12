import React, { Component, Fragment } from 'react';
import _ from 'lodash'
import authService from '../api-authorization/AuthorizeService'
import Teacher from './teacher/Teacher';
import './Teachers.css';
import { Search } from 'semantic-ui-react';

const initialState = { loading: false, results: [], value: '' }

export class Teachers extends Component {

  constructor(props) {
    super(props);
    this.state = { teachers: [], loading: true, value: '' };
  }

  componentDidMount() {
    this.populateTeachersData();
  }

     handleResultSelect = (e, { result }) => this.setState({ value: result.firstName });

     handleSearchChange = (e, { value }) => {
      this.setState({ loading: true, value })
  
      setTimeout(() => {
        if (this.state.value.length < 1) return this.setState(initialState)
        const { teachers } = this.state;
        const re = new RegExp(_.escapeRegExp(this.state.value), 'i')
        const isMatch = (result) => re.test(result.title)
  
        this.setState({
          loading: false,
          results: _.filter(teachers, isMatch),
        })
      }, 300)
    }

  static renderTeachers(teachers) {

    return (
      <Fragment>
        <div style={{marginLeft: "41%"}}>
          <Search />
        </div>
        <div className="grid-container">
          {
            teachers.map((teacher, i) =>
              <Teacher key={i} name={teacher.firstName + " " + teacher.middleName[0] + "." + " " + teacher.lastName}
               id={teacher.id}
               lectures={teacher.lectures} />
            )}
        </div>
      </Fragment>
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
    this.setState({ teachers: data, loading: false, value: '' });
  }
}
