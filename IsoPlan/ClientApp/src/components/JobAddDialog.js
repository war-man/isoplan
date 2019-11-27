import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import {
    TextField, DialogTitle, DialogContent, DialogActions, Button, FormControl,
    MenuItem, InputLabel, Select, makeStyles, IconButton, Checkbox, FormControlLabel
} from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { DevisStatusFR, DevisStatusList } from '../helpers/devisStatus';
import { JobStatusFR, JobStatusList } from '../helpers/jobStatus';
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
    checkboxField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 200,
        display: 'flex',
        alignItems: 'flex-end'
    },
    checkboxLabel:{
        marginBottom: '8px',
    },
    closeButton: {
        position: 'absolute',
        right: theme.spacing(1),
        top: theme.spacing(1),
        color: theme.palette.grey[500],
    },
    actions: {
        width: '100%'
    }
}));

function JobAddDialog(props) {
    const classes = useStyles();

    const { open, handleClose, handleAdd, jobToAdd, setJobToAdd } = props;

    const handleChange = name => event => {
        var value;
        if (name === 'devisDate' || name === 'startDate' || name === 'endDate' || name === 'rgDate') {
            value = moment(event).format("YYYY-MM-DD");
        } else if (name === 'rgCollected') {
            value = event.target.checked;
        }
        else {
            value = event.target.value;
        }
        setJobToAdd({ ...jobToAdd, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        handleAdd();
    }

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>
                Ajouter de travail
                <IconButton className={classes.closeButton} onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <MuiPickersUtilsProvider utils={DateFnsUtils}>
                    <DialogContent className={classes.container} >
                        <TextField
                            autoFocus
                            label='Client'
                            required
                            defaultValue={jobToAdd.clientName}
                            className={classes.textField}
                            onBlur={handleChange('clientName')}
                            margin="normal"
                        />
                        <TextField
                            label='Contact info'
                            defaultValue={jobToAdd.clientContact}
                            className={classes.textField}
                            onBlur={handleChange('clientContact')}
                            margin="normal"
                        />
                        <TextField
                            label='Affaire'
                            required
                            defaultValue={jobToAdd.name}
                            className={classes.textField}
                            onBlur={handleChange('name')}
                            margin="normal"
                        />
                        <TextField
                            label='Adresse'
                            defaultValue={jobToAdd.address}
                            className={classes.textField}
                            onBlur={handleChange('address')}
                            margin="normal"
                        />
                        <FormControl margin="normal" className={classes.textField}>
                            <InputLabel>Statut</InputLabel>
                            <Select
                                value={jobToAdd.status}
                                onChange={handleChange('status')}
                            >
                                {JobStatusList.map((status, i) =>
                                    <MenuItem key={i} value={status}>{JobStatusFR[status]}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <div className={classes.textField}></div>
                        <FormControl margin="normal" className={classes.textField}>
                            <InputLabel>Devis statut</InputLabel>
                            <Select
                                value={jobToAdd.devisStatus}
                                onChange={handleChange('devisStatus')}
                            >
                                {DevisStatusList.map((status, i) =>
                                    <MenuItem key={i} value={status}>{DevisStatusFR[status]}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <KeyboardDatePicker
                            margin="normal"
                            id="date-picker-dialog"
                            label="Date devis"
                            format="dd.MM.yyyy"
                            value={jobToAdd.devisDate}
                            className={classes.textField}
                            onChange={handleChange('devisDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            id="date-picker-dialog"
                            label="Date debut"
                            format="dd.MM.yyyy"
                            value={jobToAdd.startDate}
                            className={classes.textField}
                            onChange={handleChange('startDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            id="date-picker-dialog"
                            label="Date fin"
                            format="dd.MM.yyyy"
                            value={jobToAdd.endDate}
                            className={classes.textField}
                            onChange={handleChange('endDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            id="date-picker-dialog"
                            label="RG date"
                            format="dd.MM.yyyy"
                            value={jobToAdd.rgDate}
                            className={classes.textField}
                            onChange={handleChange('rgDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <FormControlLabel className={classes.checkboxField}
                            control={
                                <Checkbox
                                    checked={jobToAdd.rgCollected}
                                    onChange={handleChange('rgCollected')}
                                    value={jobToAdd.rgCollected}
                                    color="primary"
                                />
                            }
                            label={
                                <div className={classes.checkboxLabel}>
                                    RG collecté
                                </div>
                            }
                        />
                    </DialogContent>
                </MuiPickersUtilsProvider>
                <DialogActions className={classes.actions}>
                    <Button variant="contained" type="submit" color="primary">
                        Ajouter
                    </Button>
                </DialogActions>
            </form>
        </Dialog>
    );
}

export default JobAddDialog;