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
import { createMuiTheme } from '@material-ui/core/styles';
import { ThemeProvider } from '@material-ui/styles';
import { lightGreen } from '@material-ui/core/colors';
import Schedules from './Schedules';
import { frFR } from '@material-ui/core/locale';


const theme = createMuiTheme({
    palette: {
        primary: { main: lightGreen[500], contrastText: "#fff" },        
    },
}, frFR);

function App() {
    return (
        <ThemeProvider theme={theme}>
            <div>
                <Route path="/login" component={Login} />
                <PrivateRoute exact path="/" component={Home} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/planning/:date?" component={Schedules} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/travaux/" component={Jobs} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/travaux/:id" component={JobDetails} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/personnel/" component={Employees} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/personnel/:id" component={EmployeeDetails} roles={['Admin', 'Manager']} />
                <PrivateRoute exact path="/usagers/" component={Users} roles={['Admin']} />
            </div>
        </ThemeProvider>
    );
}

export default App;
