import React from 'react';
import { Route } from 'react-router-dom';
import Login from './Login';
import Home from './Home';
import { PrivateRoute } from './PrivateRoute';
import Users from './Users';
import Jobs from './Jobs';
import Employees from './Employees';
import EmployeeDetails from './EmployeeDetails';
import JobDetails from './JobDetails';

function App() {
    return (
        <div>
            <Route path="/login" component={Login} />
            <PrivateRoute exact path="/" component={Home} roles={['Admin', 'Manager']}/>
            <PrivateRoute exact path="/travaux/" component={Jobs} roles={['Admin', 'Manager']}/>
            <PrivateRoute exact path="/travaux/:id" component={JobDetails} roles={['Admin', 'Manager']}/>
            <PrivateRoute exact path="/personnel/" component={Employees} roles={['Admin', 'Manager']}/>
            <PrivateRoute exact path="/personnel/:id" component ={EmployeeDetails} roles={['Admin', 'Manager']} />
            <PrivateRoute exact path="/users/" component={Users} roles={['Admin']}/>
        </div>
    );
}

export default App;
