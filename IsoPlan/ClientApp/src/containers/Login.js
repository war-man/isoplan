import React, { useState, useEffect } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import { Redirect } from 'react-router-dom';
import Snackbar from '@material-ui/core/Snackbar';
import CustomSnackbarContent from '../components/CustomSnackbarContent';
import { login, logout, handleResponse } from '../helpers/authentication';

const useStyles = makeStyles(theme => ({
    root: {
        height: '100vh',
    },
    image: {
        backgroundImage: `url(${process.env.PUBLIC_URL}/background.jpg)`,
        backgroundRepeat: 'no-repeat',
        backgroundSize: 'cover',
        backgroundPosition: 'center',
    },
    paper: {
        margin: theme.spacing(8, 4),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: theme.palette.secondary.main,
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
    snackbar: {
        margin: theme.spacing(1),
    },
}));

function Login() {
    const classes = useStyles();

    const [loggedIn, setLoggedIn] = useState(false)
    const [open, setOpen] = React.useState(false);

    useEffect(() => {
        logout()
    }, [])

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        setOpen(false);
    };

    const renderRedirect = () => {
        if (loggedIn) {
            return <Redirect push to='/' />
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault()
        const username = e.currentTarget.username.value
        const password = e.currentTarget.password.value

        const options = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                'username': username,
                'password': password
            })
        };

        fetch(process.env.REACT_APP_API_URL + "api/Users/authenticate", options)
            .then(handleResponse)
            .then(response => {
                login(response)
                setLoggedIn(true)
            })
            .catch(error => {
                setOpen(true)
            })
    }

    return (
        <Grid container component="main" className={classes.root}>
            {renderRedirect()}
            <CssBaseline />
            <Snackbar
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
                open={open}
                autoHideDuration={6000}
                onClose={handleClose}
            >
                <CustomSnackbarContent
                    onClose={handleClose}
                    variant="error"
                    className={classes.snackbar}
                    message="Username or password are incorrect"
                />
            </Snackbar>
            <Grid item xs={false} sm={4} md={7} className={classes.image} />
            <Grid item xs={12} sm={8} md={5} component={Paper} elevation={6} square>
                <div className={classes.paper}>
                    <Avatar className={classes.avatar}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Se connecter
                    </Typography>
                    <form className={classes.form} onSubmit={handleSubmit}>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Username"
                            name="username"
                            autoComplete="username"
                            autoFocus
                        />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            className={classes.submit}
                        >
                            Se connecter
                        </Button>
                    </form>
                </div>
            </Grid>
        </Grid>
    );
}

export default Login;