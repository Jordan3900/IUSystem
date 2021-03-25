import React, { Component, Fragment } from 'react';
import authService from '../api-authorization/AuthorizeService'
import { Row, Col, CardImg, Table } from 'reactstrap';

export class MyStudents extends Component {

    constructor(props) {
        super(props);
        this.state = { students: [], loading: true };
    }

    componentDidMount() {
        this.populateData()
    }


    render() {
        const { students } = this.state;

        return (
            <Fragment>
            <h4 className="text-white text-center">My students</h4>
            <Table className="text-white" dark striped>
                <thead >
                    <tr>
                        <th>##</th>
                        <th>Name</th>
                        <th>Subject</th>
                        <th>Grade</th>
                        <th>Number</th>
                    </tr>
                </thead>
                <tbody>
                    {students.map((student, i) =>
                        <tr key={i}>
                            <th scope="row">{++i}</th>
                            <td>{student.name}</td>
                            <td>{student.subject}</td>
                            <td>{student.grade}</td>
                            <td>{student.number}</td>
                        </tr>
                    )}
                </tbody>
            </Table>
            </Fragment>
        );
    }

    async populateData() {
        const [ user ] = await Promise.all([authService.getUser()]);
        const token = await authService.getAccessToken();
        const response = await fetch(`mystudentsdata?name=${user.name}`, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });

        const data = await response.json();
        this.setState({ students: data});
    }

}
