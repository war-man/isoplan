import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import { useParams } from 'react-router-dom';
import { employeeService } from '../services/employeeService';
import { EmployeeStatus, EmployeeStatusList } from '../helpers/employeeStatus';
import { ContractType, ContractTypeList } from '../helpers/contractType';
import { Grid, Paper, TextField, FormControl, MenuItem, Button, InputLabel, Select, makeStyles } from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';

const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1,
    },
    paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
    },
    container: {
        paddingLeft: theme.spacing(4),
        paddingRight: theme.spacing(4),
        display: 'flex',
        flexWrap: 'wrap',
        justifyContent: 'center',
    },
    textField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: 220,
        textAlign: 'left',
    },
    noteField: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
    },
    actions: {
        marginLeft: theme.spacing(12),
        marginRight: theme.spacing(1),
        width: '100%',
        textAlign: 'right',
    },
}));

function EmployeeDetails() {
    const classes = useStyles();
    const { id } = useParams();

    const handleChange = name => (event) => {
        var value;
        if (name === 'workStart' || name === 'workEnd') {
            value = event;
        } else {
            value = event.target.value;
        }
        setEmployee({ ...employee, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        employeeService.update(employee)
            .then(res => {
                getEmployee(id);
            })
            .catch(err => {
                alert(err);
            })
    }

    const [employee, setEmployee] = useState({
        firstName: '',
        lastName: '',
        salary: '',
        accountNumber: '',
        contractType: ContractType.Indefinite,
        workStart: null,
        workEnd: null,
        status: EmployeeStatus.Active,
        note: '',
    })

    const getEmployee = (id) => {
        employeeService.get(id)
            .then(res => {
                setEmployee({ ...res })
            })
            .catch(err => {
                alert(err)
            })
    }

    useEffect(() => {
        getEmployee(id)
    }, [id]);

    return (
        <Dashboard>
            <Grid container spacing={3} className={classes.root}>
                <Grid item sm={12} md={6}>
                    <Paper className={classes.paper}>
                        <form autoComplete="off" onSubmit={handleSubmit} className={classes.container}>
                            <TextField
                                autoFocus
                                label='Prénom'
                                required
                                value={employee.firstName}
                                className={classes.textField}
                                onChange={handleChange('firstName')}
                                margin="normal"
                            />
                            <TextField
                                label='Nom'
                                required
                                value={employee.lastName}
                                className={classes.textField}
                                onChange={handleChange('lastName')}
                                margin="normal"
                            />
                            <FormControl margin="normal" className={classes.textField}>
                                <InputLabel>Statut</InputLabel>
                                <Select
                                    value={employee.status}
                                    onChange={handleChange('status')}
                                >
                                    {EmployeeStatusList.map((status, i) =>
                                        <MenuItem key={i} value={status}>{status}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                            <TextField
                                label='Per diem'
                                required
                                type='number'
                                inputProps={{ min: 0, step: 0.01 }}
                                value={employee.salary}
                                className={classes.textField}
                                onChange={handleChange('salary')}
                                margin="normal"
                            />
                            <TextField
                                label='Numéro de compte'
                                required
                                value={employee.accountNumber}
                                className={classes.textField}
                                onChange={handleChange('accountNumber')}
                                margin="normal"
                            />
                            <FormControl margin="normal" className={classes.textField}>
                                <InputLabel>Contrat</InputLabel>
                                <Select
                                    value={employee.contractType}
                                    onChange={handleChange('contractType')}
                                >
                                    {ContractTypeList.map((contract, i) =>
                                        <MenuItem key={i} value={contract}>{contract}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                            <MuiPickersUtilsProvider utils={DateFnsUtils}>
                                <KeyboardDatePicker
                                    required={true}
                                    margin="normal"
                                    id="date-picker-dialog"
                                    label="Commencé le travail"
                                    format="dd.MM.yyyy"
                                    value={employee.workStart}
                                    className={classes.textField}
                                    onChange={handleChange('workStart')}
                                    KeyboardButtonProps={{
                                        'aria-label': 'Changer la date',
                                    }}
                                />
                                <KeyboardDatePicker
                                    margin="normal"
                                    id="date-picker-dialog"
                                    label="Arrêté le travail"
                                    format="dd.MM.yyyy"
                                    value={employee.workEnd}
                                    className={classes.textField}
                                    onChange={handleChange('workEnd')}
                                    KeyboardButtonProps={{
                                        'aria-label': 'Changer la date',
                                    }}
                                />
                            </MuiPickersUtilsProvider>
                            <TextField
                                id="outlined-multiline-flexible"
                                label="Remarque"
                                multiline
                                value={employee.note}
                                onChange={handleChange('note')}
                                className={classes.noteField}
                                fullWidth
                                margin="normal"
                                variant="outlined"
                            />
                            <div className={classes.actions}>
                                <Button variant="contained" type="submit" color="primary">
                                    Enregistrer
                                </Button>
                            </div>                            
                        </form>
                    </Paper>
                </Grid>
                <Grid item container sm={12} md={6}>
                    <Grid item xs={12}>
                        <Paper className={classes.paper}>files</Paper>
                    </Grid>
                    <Grid item xs={12}>
                        <Paper className={classes.paper}>upload</Paper>
                    </Grid>
                </Grid>
            </Grid>
        </Dashboard>
    )
}

export default EmployeeDetails;