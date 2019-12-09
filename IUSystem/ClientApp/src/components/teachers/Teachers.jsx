import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService'

export class Teachers extends Component {

  constructor(props) {
    super(props);
    this.state = { teachers: [], loading: true };
  }

//   componentDidMount() {
//     // this.populateWeatherData();
//   }

//   static renderTeachers(forecasts) {
//     return (
//      <h1>Hello From Teachers Page!</h1>
//     );
//   }

  render() {
    // let contents = this.state.loading
    //   ? <p><em>Loading...</em></p>
    //   : FetchData.renderForecastsTable(this.state.forecasts);

    return (
      <div>
        <h1>Hello from techers page!</h1>
      </div>
    );
  }

//   async populateWeatherData() {
//     const token = await authService.getAccessToken();
//     const response = await fetch('weatherforecast', {
//       headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
//     });
//     const data = await response.json();
//     this.setState({ teachers: data, loading: false });
//   }
}
