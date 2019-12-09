import React, { useState } from 'react';
import Dialog from '@material-ui/core/Dialog';
import { TextField, DialogTitle, DialogContent, DialogActions, Button, makeStyles, IconButton } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import moment from 'moment';
import 'moment/locale/fr'
import ConfirmDeleteDialog from './ConfirmDeleteDialog';

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
    capital: {
        textTransform: 'capitalize'
    }
}))

function ScheduleEditDialog(props) {
    const classes = useStyles()

    const { open, handleClose, scheduleToEdit, setScheduleToEdit, handleSave, handleDelete } = props

    const handleChange = name => event => {
        setScheduleToEdit({ ...scheduleToEdit, [name]: event.target.value })
    }

    const handleSubmitForm = (event) => {
        event.preventDefault();
        handleSave();
        handleClose();
    }

    const handleOpenDelete = () => {
        setOpenDelete(true);
    }

    const handleCloseDelete = () => {
        setOpenDelete(false);
    }

    const handleDeleteSchedule = () => {
        handleDelete()
        handleCloseDelete()
        handleClose()
    }

    const [openDelete, setOpenDelete] = useState(false)

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle className={classes.capital}>
                {moment(scheduleToEdit.date).isValid() && moment(scheduleToEdit.date, "YYYY-MM-DDTHH:mm:ss").locale('fr').format('dddd DD.MM.')}
                <IconButton className={classes.closeButton} onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={handleSubmitForm} >
                <DialogContent className={classes.container} >
                    <TextField
                        label='Travail'
                        value={scheduleToEdit.jobName}
                        className={classes.textField}
                        margin="normal"
                        InputProps={{
                            readOnly: true,
                        }}
                    />
                    <TextField
                        label='Nom complet'
                        value={scheduleToEdit.employeeName}
                        className={classes.textField}
                        margin="normal"
                        InputProps={{
                            readOnly: true,
                        }}
                    />
                    <TextField
                        label='Salaire (€)'
                        type='number'
                        required
                        defaultValue={scheduleToEdit.salary}
                        className={classes.textField}
                        onBlur={handleChange('salary')}
                        margin="normal"
                        inputProps={{
                            min: '0', 
                            step: '0.01',                            
                        }}
                    />
                    <TextField
                        label="Remarque"
                        multiline
                        defaultValue={scheduleToEdit.note}
                        onBlur={handleChange('note')}
                        className={classes.textField}
                        fullWidth
                        rowsMax={4}
                        margin="normal"
                        variant="outlined"
                    />
                </DialogContent>
                <DialogActions className={classes.actions}>
                    <Button variant="contained" onClick={handleOpenDelete} color="secondary">
                        Supprimer
                    </Button>
                    <Button variant="contained" type="submit" color="primary">
                        Enregistrer
                    </Button>
                </DialogActions>
            </form>
            <ConfirmDeleteDialog
                open={openDelete}
                handleClose={handleCloseDelete}
                handleDelete={handleDeleteSchedule}
                text={'Confirmer la suppression?'}
            />
        </Dialog>
    )
}

export default ScheduleEditDialog