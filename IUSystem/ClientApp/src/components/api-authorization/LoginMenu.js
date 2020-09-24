import React, { Component, Fragment } from 'react';
import { NavItem, NavLink, Dropdown, DropdownToggle, DropdownItem, DropdownMenu } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            userName: null,
            isAdmin: false,
            isTeacher: false,
            dropdownOpen: false
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateUserData(user) {
        if (!user) {
            return { isAdmin: false, isTeacher: false };
        }
        const token = await authService.getAccessToken();
        const response = await fetch(`userdata?name=${user.name}`, {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });

        const data = await response.json();
        return data;
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()]);
        const data = await this.populateUserData(user);

        this.setState({
            isAuthenticated,
            userName: user && user.name,
            isAdmin: data.isAdmin,
            isTeacher: data.isTeacher
        });
    }

    toggle = () => {
        this.setState({ dropdownOpen: !this.state.dropdownOpen })
    }

    render() {
        const { isAuthenticated, userName, isAdmin, isTeacher } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath, isAdmin, isTeacher);
        }
    }

    authenticatedView(userName, profilePath, logoutPath, isAdmin, isTeacher) {
        return (<Fragment>
            {isTeacher ?
                <NavItem>
                    <NavLink tag={Link} className="text-white" to="/mystudents">My students</NavLink>
                </NavItem> : null}
            <NavItem>
                <NavLink tag={Link} className="text-white" to={profilePath}>Hello {userName}</NavLink>
            </NavItem>

            {isAdmin ?
                <Dropdown className="text-white" nav isOpen={this.state.dropdownOpen} toggle={this.toggle}>
                    <DropdownToggle className="text-white" nav caret>
                        Admin Panel
            </DropdownToggle>
                    <DropdownMenu>
                        <DropdownItem href="/Identity/Students/StudentsAll">Students</DropdownItem>
                        <DropdownItem href="/Identity/Teachers/TeachersAll">Teachers</DropdownItem>
                        <DropdownItem href="/Identity/Subjects/All">Subjects</DropdownItem>
                        <DropdownItem href="/identity/Lectures/LecturesAll">Lectures</DropdownItem>
                    </DropdownMenu>
                </Dropdown>
                : null}
            <NavItem>
                <NavLink tag={Link} className="text-white" to={logoutPath}>Logout</NavLink>
            </NavItem>
        </Fragment>);

    }

    anonymousView(registerPath, loginPath) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-white" to={registerPath}>Register</NavLink>
            </NavItem>
            <NavItem>
                <NavLink tag={Link} className="text-white" to={loginPath}>Login</NavLink>
            </NavItem>
        </Fragment>);
    }
}
