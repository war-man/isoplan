import React, { useState } from 'react';
import Dialog from '@material-ui/core/Dialog';
import { DialogTitle, DialogContent, DialogActions, Button, makeStyles, IconButton, TextField } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import Autocomplete from '@material-ui/lab/Autocomplete';
import moment from 'moment';

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
    autocomplete: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: (200 + theme.spacing(1)) * 2,
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


function ScheduleAddDialog(props) {
    const classes = useStyles();

    const { open, handleClose, jobs, employees, handleCreate } = props;

    const handleChange = name => (event, newValue) => {
        var value;
        if (name === 'date') {
            value = moment(event).format("YYYY-MM-DD");
        } else if (name === 'employees') {
            value = newValue
        }
        else if (name === 'job') {
            value = newValue
        }
        setSchedule({ ...schedule, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault()
        handleCreate(schedule)
        handleCloseAdd()
    }

    const handleCloseAdd = () => {
        setSchedule({
            date: null,
            job: {id: 0, name: '', clientName: ''},
            employees: [],
        });
        handleClose();
    }

    const [schedule, setSchedule] = useState({
        date: null,
        job: {id: 0, name: '' , clientName: ''},
        employees: [],
    })

    return (
        <Dialog open={open} onClose={handleCloseAdd}>
            <DialogTitle>
                Ajouter
                <IconButton className={classes.closeButton} onClick={handleCloseAdd}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <DialogContent className={classes.container}>
                    <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                        <KeyboardDatePicker
                            required={true}
                            margin="normal"
                            label="Date"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            className={classes.textField}
                            onChange={handleChange('date')}
                            value={schedule.date}
                            shouldDisableDate={date => date.getDay() === 0}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                    </MuiPickersUtilsProvider>
                    <Autocomplete 
                        className={classes.textField}
                        options={jobs}
                        getOptionLabel={job => job.id === 0 ? '' : `${job.name} (${job.clientName})`}
                        onChange={handleChange('job')}
                        value={schedule.job}
                        renderInput={params => (
                            <TextField
                                {...params}
                                variant="standard"
                                label="Travail"
                                margin="normal"
                                fullWidth
                            />
                        )}
                    />
                    <Autocomplete
                        className={classes.autocomplete}
                        multiple
                        options={employees}
                        getOptionLabel={employee => `${employee.firstName} ${employee.lastName}`}
                        onChange={handleChange('employees')}
                        value={schedule.employees}
                        renderInput={params => (
                            <TextField
                                {...params}
                                variant="standard"
                                label="Personnel"
                                margin="normal"
                                fullWidth
                            />
                        )}
                    />
                </DialogContent>
                <DialogActions className={classes.actions}>
                    <Button variant="contained" type="submit" color="primary">
                        Ajouter
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    );
}

export default ScheduleAddDialog;