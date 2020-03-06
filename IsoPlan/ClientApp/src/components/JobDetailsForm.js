import React from 'react';
import { makeStyles, Grid, TextField, FormControl, Button, Select, InputLabel, MenuItem, Checkbox, FormControlLabel } from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import { DevisStatusFR, DevisStatusList } from '../helpers/devisStatus';
import { JobStatusFR, JobStatusList } from '../helpers/jobStatus';
const JobDetailsForm = (props) => {
    const { job, getJob, handleChange, handleSubmit, id } = props
    const classes = useStyles()
    return (
        <form autoComplete="off" onSubmit={handleSubmit}>
            <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                <Grid container justify="space-evenly">
                    <Grid item xs={6} className={classes.container}>
                        <TextField
                            label='Client'
                            required
                            value={job.clientName}
                            className={classes.textField}
                            onChange={handleChange('clientName')}
                            margin="normal"
                        />
                        <TextField
                            label='Affaire'
                            required
                            value={job.name}
                            className={classes.textField}
                            onChange={handleChange('name')}
                            margin="normal"
                        />
                        <TextField
                            label='Adresse'
                            value={job.address}
                            className={classes.wideTextField}
                            onChange={handleChange('address')}
                            margin="normal"
                        />
                        <TextField
                            multiline
                            rowsMax={6}
                            rows={6}
                            variant="outlined"
                            label='Contact info'
                            value={job.clientContact}
                            className={classes.wideTextField}
                            onChange={handleChange('clientContact')}
                            margin="normal"
                        />
                    </Grid>
                    <Grid item xs={12} md={6} className={classes.container}>
                        <FormControl margin="normal" className={classes.textField}>
                            <InputLabel>Statut</InputLabel>
                            <Select
                                value={job.status}
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
                                value={job.devisStatus}
                                onChange={handleChange('devisStatus')}
                            >
                                {DevisStatusList.map((status, i) =>
                                    <MenuItem key={i} value={status}>{DevisStatusFR[status]}</MenuItem>
                                )}
                            </Select>
                        </FormControl>
                        <KeyboardDatePicker
                            margin="normal"
                            label="Date devis"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={job.devisDate}
                            className={classes.textField}
                            onChange={handleChange('devisDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            label="Date debut"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={job.startDate}
                            className={classes.textField}
                            onChange={handleChange('startDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            label="Date fin"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={job.endDate}
                            className={classes.textField}
                            onChange={handleChange('endDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            label="RG date"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={job.rgDate}
                            className={classes.textField}
                            onChange={handleChange('rgDate')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <FormControlLabel className={classes.checkboxField}
                            control={
                                <Checkbox
                                    checked={job.rgCollected}
                                    onChange={handleChange('rgCollected')}
                                    value={job.rgCollected}
                                    color="primary"
                                />
                            }
                            label={
                                <div className={classes.checkboxLabel}>
                                    RG collecté
                                        </div>
                            }
                        />
                        <div className={classes.actions}>
                            <Button variant="contained" color="primary" className={classes.button} onClick={() => getJob(id)}>
                                Annuler
                                </Button>
                            <Button variant="contained" type="submit" color="primary">
                                Enregistrer
                                </Button>
                        </div>
                    </Grid>
                </Grid>
            </MuiPickersUtilsProvider>
        </form>
    )
}

export default JobDetailsForm;

const useStyles = makeStyles(theme => ({
    container: {
        textAlign: 'left',
        minWidth: 390 + theme.spacing(6),
        maxWidth: 390 + theme.spacing(6)
    },
    wideTextField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 390 + theme.spacing(2),
        textAlign: 'left',
    },
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 190,
        textAlign: 'left',
    },
    checkboxField: {
        marginTop: theme.spacing(2),
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 190,
    },
    actions: {
        width: '100%',
        textAlign: 'right',
        marginTop: theme.spacing(1),
    },
    button: {
        marginRight: theme.spacing(1),
    },
}));