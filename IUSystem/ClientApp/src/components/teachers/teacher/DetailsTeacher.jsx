import React, { Component, Fragment } from 'react';
import { useParams } from "react-router-dom";


export class DetailsTeacher extends Component {

    constructor(props) {
        super(props);
        this.state = { teachers: [], loading: true, value: '' };
    }

    componentDidMount() {
        const { match: { params } } = this.props;
        debugger;
    }

    render() {
        return (
            <div>
                <h1 className="text-center text-white">Details Page</h1>
            </div>
        );
    }

}
