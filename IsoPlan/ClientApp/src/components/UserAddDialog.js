import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import { TextField, DialogTitle, DialogContent, DialogActions, Button, FormControl, MenuItem, InputLabel, Select, makeStyles, IconButton } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { RoleFR } from '../helpers/role';

const useStyles = makeStyles(theme => ({
    container: {
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
    },
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 200,
        textAlign: 'left',
    },
    closeButton: {
        position: 'absolute',
        right: theme.spacing(1),
        top: theme.spacing(1),
        color: theme.palette.grey[500],
    },
    actions: {
        width: '100%'
    },
}));


function UserAddDialog(props) {
    const classes = useStyles();

    const { open, handleClose, handleAdd, userToAdd, setUserToAdd, roleList } = props;

    const handleChange = name => event => {
        const value = event.target.value
        setUserToAdd({ ...userToAdd, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault()
        handleAdd()
    }

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>                
                Add new user           
                <IconButton className={classes.closeButton} onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <DialogContent className={classes.container}>
                    <TextField
                        autoFocus
                        label="Prénom"
                        required
                        defaultValue={userToAdd.firstName}
                        className={classes.textField}
                        onBlur={handleChange('firstName')}
                        margin="normal"
                    />
                    <TextField
                        label="Nom"
                        required
                        defaultValue={userToAdd.lastName}
                        className={classes.textField}
                        onBlur={handleChange('lastName')}
                        margin="normal"
                    />
                    <TextField
                        label="Nom d'utilisateur"
                        required
                        defaultValue={userToAdd.username}
                        className={classes.textField}
                        onBlur={handleChange('username')}
                        margin="normal"
                    />
                    <TextField
                        label="Password"
                        type="password"
                        required
                        defaultValue={userToAdd.password}
                        className={classes.textField}
                        onBlur={handleChange('password')}
                        margin="normal"
                        autoComplete="nope"
                    /> 
                    <FormControl margin="normal" className={classes.textField}>
                        <InputLabel>Rôle</InputLabel>
                        <Select                            
                            value={userToAdd.role}
                            onChange={handleChange('role')}                            
                        >
                            {roleList.map((role, i) =>
                                <MenuItem key={i} value={role}>{RoleFR[role]}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                    <div className={classes.textField}></div>
                </DialogContent>
                <DialogActions className={classes.actions}>
                    <Button variant="contained" type="submit" color="primary">
                        Add
                    </Button>
                </DialogActions>
            </form>

        </Dialog>
    );
}

export default UserAddDialog;