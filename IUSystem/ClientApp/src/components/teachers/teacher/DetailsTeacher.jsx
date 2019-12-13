import React, { Component, Fragment } from 'react';
import authService from '../../api-authorization/AuthorizeService'
import avatar from './assets/avatar.svg'
import { Row, Col, CardImg, Table } from 'reactstrap';

export class DetailsTeacher extends Component {

    constructor(props) {
        super(props);
        this.state = { teacher: {}, loading: true, value: '' };
    }

    componentDidMount() {
        const { id } = this.props.match.params;
        this.populateTeacherData(id)
    }

    renderProfile(teacher) {
        return (
            <Col>
                <h2 className="text-white">{teacher.name}</h2>
                <h3 className="text-white">Email: {teacher.email}</h3>
            </Col>
        )
    }

    renderSchedule(lectures) {

        return (      
            <Table className="text-white" dark striped>
                <thead >
                    <tr>
                        <th>#</th>
                        <th>Date</th>
                        <th>Time</th>
                        <th>Room</th>
                        <th>Subject</th>
                    </tr>
                </thead>
                <tbody>
                    {lectures.map((lecture, i) =>
                        <tr key={i}>
                            <th scope="row">{++i}</th>
                            <td>{lecture.date}</td>
                            <td>{lecture.startTime} - {lecture.endTime}</td>
                            <td>{lecture.room}</td>
                            <td>{lecture.subject}</td>
                        </tr>
                    )}
                </tbody>
            </Table>
        );
    }

    render() {
        const { teacher, loading } = this.state;

        let table = loading
            ? <p className="text-white"><em>Loading...</em></p>
            : this.renderSchedule(teacher.lectures);

        let profile = loading
            ? <p className="text-white"><em>Loading...</em></p>
            : this.renderProfile(teacher);

        return (
            <div style={{ marginTop: "7%" }}>
                <Row>
                    <Col xs="3">
                        <img src={avatar} alt="Card image cap" style={{ width: "fit-content", height: "10em" }} />
                    </Col>
                    {profile}
                </Row>
                <Row className="mt-5 text-white">
                    {table}
                </Row>
            </div>
        );
    }

    async populateTeacherData(id) {
        const token = await authService.getAccessToken();
        const response = await fetch('teacherDetails', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        debugger;
        const data = await response.json();
        const teacher = data.filter(x => x.id == id)[0];
        this.setState({ teacher: teacher, loading: false, value: '' });
    }

}
