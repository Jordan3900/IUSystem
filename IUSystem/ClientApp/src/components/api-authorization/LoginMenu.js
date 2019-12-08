import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';

export class LoginMenu extends Component {
    constructor(props) {
        super(props);

        this.state = {
            isAuthenticated: false,
            isAdimn: false,
            userName: null
        };
    }

    componentDidMount() {
        this._subscription = authService.subscribe(() => this.populateState());
        this.populateState();
    }

    componentWillUnmount() {
        authService.unsubscribe(this._subscription);
    }

    async populateState() {
        const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
        this.setState({
            isAuthenticated,
            userName: user && user.name,
            isAdmin: user && user.name == 'admin_ius@dev.com'
        });
    }

    render() {
        const { isAuthenticated, userName, isAdmin } = this.state;
        if (!isAuthenticated) {
            const registerPath = `${ApplicationPaths.Register}`;
            const loginPath = `${ApplicationPaths.Login}`;
            return this.anonymousView(registerPath, loginPath);
        } else {
            const profilePath = `${ApplicationPaths.Profile}`;
            const logoutPath = { pathname: `${ApplicationPaths.LogOut}`, state: { local: true } };
            return this.authenticatedView(userName, profilePath, logoutPath, isAdmin);
        }
    }

    authenticatedView(userName, profilePath, logoutPath, isAdmin) {
        return (<Fragment>
            <NavItem>
                <NavLink tag={Link} className="text-white" to={profilePath}>Hello {userName}</NavLink>
            </NavItem>
            { isAdmin ? 
             <NavItem>
                <NavLink tag={Link} className="text-white" to={profilePath}>Admin panel</NavLink>
            </NavItem> : null
            }
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
