import React, { Component, Fragment } from 'react';
import PropTypes from 'prop-types'
import _ from 'lodash'
import authService from '../api-authorization/AuthorizeService'
import Teacher from './teacher/Teacher';
import './Teachers.css';
import { Search, Label } from 'semantic-ui-react';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';

const resultRenderer = ({ firstName, lastName}) => <Label content={firstName + " " +  lastName} />

resultRenderer.propTypes = {
  firstName: PropTypes.string,
}

export class Teachers extends Component {

  constructor(props) {
    super(props);
    this.state = {
      teachers: [],
      loading: true,
      value: '',
      currentPage: 0,
      isSearchLoading: false,
      results: []
    };

  }

  componentDidMount() {
    this.populateTeachersData();
  }

  handleResultSelect = (e, { result }) => this.setState({ value: result.firstName });

  handleSearchChange = (e, { value }) => {
    this.setState({ isSearchLoading: true, value })
    debugger;
    setTimeout(() => {

      const { results } = this.state;
      const re = new RegExp(_.escapeRegExp(this.state.value), 'i')
      const isMatch = (result) => re.test(result.firstName)

      this.setState({
        isSearchLoading: false,
        results: _.filter(results, isMatch),
      })
    }, 300)
  }

  handleClick = (e, index) => {

    e.preventDefault();
    this.setState({
      currentPage: index
    });

  }

  static renderTeachers(teachers, currentPage, pagesSize, pagesCount, handleClick) {

    return (
      <Fragment>

        <div className="grid-container">
          {
            teachers.slice(
              currentPage * pagesSize,
              (currentPage + 1) * pagesSize
            ).map((teacher, i) =>
              <Teacher key={i} name={teacher.firstName + " " + teacher.middleName[0] + "." + " " + teacher.lastName}
                id={teacher.id}
                lectures={teacher.lectures} />
            )}
        </div>
        <Pagination style={{ marginLeft: "45%" }} aria-label="Page navigation example">
          <PaginationItem disabled={currentPage <= 0}>
            <PaginationLink
              onClick={e => handleClick(e, currentPage - 1)}
              previous
              href="#"
            />
          </PaginationItem>
          {[...Array(pagesCount)].map((page, i) =>
            <PaginationItem active={i === currentPage} key={i}>
              <PaginationLink onClick={e => handleClick(e, i)} href="#">
                {i + 1}
              </PaginationLink>
            </PaginationItem>
          )}

          <PaginationItem disabled={currentPage >= pagesCount - 1}>

            <PaginationLink
              onClick={e => handleClick(e, currentPage + 1)}
              next
              href="#"
            />

          </PaginationItem>
        </Pagination>
      </Fragment>
    );
  }

  render() {
    const pagesSize = 6;
    const pagesCount = Math.ceil(this.state.teachers.length / pagesSize);
    const { loading, isSearchLoading, teachers, currentPage, value, results } = this.state;

    let contents = loading
      ? <p className="text-white"><em>Loading...</em></p>
      : Teachers.renderTeachers(teachers, currentPage, pagesSize, pagesCount, this.handleClick);

    return (
      <div>
        <h1 className="text-center text-white">Find your teacher</h1>
        <div style={{ marginLeft: "41%" }}>
          <Search
            loading={isSearchLoading}
            onResultSelect={this.handleResultSelect}
            onSearchChange={_.debounce(this.handleSearchChange, 500, {
              leading: true,
            })}
            results={results}
            value={value}
            resultRenderer={resultRenderer}
            {...this.props} />
        </div>
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
    this.setState({ teachers: data, results: data, loading: false, currentPage: 0 });
  }
}
