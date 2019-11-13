import React from 'react'
import Dashboard from '../components/Dashboard';
import { Grid, Typography } from '@material-ui/core';

function Home() {

    const user = JSON.parse(localStorage.getItem('user'))

    return (
        <Dashboard>            
            <Grid wrap={'wrap'} container spacing={3}>
                <Grid item xs zeroMinWidth>
                    <Typography>
                        Welcome {`${user.firstName} ${user.lastName}`} <br />
                        Your role is: {`${user.role}`} <br />
                        Your token is: {`${user.token}`} <br />
                    </Typography>                    
                </Grid>
            </Grid>     
        </Dashboard>
    )
}

export default Home;