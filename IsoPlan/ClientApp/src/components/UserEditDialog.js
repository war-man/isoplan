import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import { TextField, DialogTitle, DialogContent, DialogActions, Button, FormControl, MenuItem, InputLabel, Select, makeStyles, IconButton } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { getCurrentUser } from '../helpers/authentication';
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
}))

function UserEditDialog(props) {
    const classes = useStyles()

    const { open, handleClose, userToEdit, setUserToEdit, handleSave, roleList } = props

    const saveUser = (event) => {
        event.preventDefault()
        handleSave()
    }

    const handleChange = name => event => {
        setUserToEdit({ ...userToEdit, [name]: event.target.value })
    }

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>
                Details
                <IconButton className={classes.closeButton} onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={saveUser} >
                <DialogContent className={classes.container} >
                    <TextField
                        autoFocus
                        label="Prénom"
                        required
                        defaultValue={userToEdit.firstName}
                        className={classes.textField}
                        onBlur={handleChange('firstName')}
                        margin="normal"
                    />
                    <TextField
                        label="Nom"
                        required
                        defaultValue={userToEdit.lastName}
                        className={classes.textField}
                        onBlur={handleChange('lastName')}
                        margin="normal"
                    />
                    <TextField
                        label="Nom d'utilisateur"
                        required
                        defaultValue={userToEdit.username}
                        className={classes.textField}
                        onBlur={handleChange('username')}
                        margin="normal"
                    />
                    <TextField
                        label="Password"
                        type="password"
                        defaultValue={userToEdit.password}
                        className={classes.textField}
                        onBlur={handleChange('password')}
                        margin="normal"
                        autoComplete="nope"
                    />
                    <FormControl margin="normal" className={classes.textField}>
                        <InputLabel>Rôle</InputLabel>
                        <Select
                            value={userToEdit.role}
                            onChange={handleChange('role')}
                            disabled={getCurrentUser().id === userToEdit.id}
                        >
                            <MenuItem value="" disabled>Rôle</MenuItem>
                            {roleList.map((role, i) =>
                                <MenuItem key={i} value={role}>{RoleFR[role]}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                    <div className={classes.textField}></div>
                </DialogContent>
                <DialogActions className={classes.actions}>                
                    <Button variant="contained" type="submit" color="primary">
                        Save
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    )
}

export default UserEditDialog