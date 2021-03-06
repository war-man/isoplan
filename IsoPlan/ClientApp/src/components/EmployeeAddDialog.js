import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import { TextField, DialogTitle, DialogContent, DialogActions, Button, FormControl, MenuItem, InputLabel, Select, makeStyles, IconButton } from '@material-ui/core';
import CloseIcon from '@material-ui/icons/Close';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import { EmployeeStatusFR, EmployeeStatusList } from '../helpers/employeeStatus';
import { ContractTypeFR, ContractTypeList } from '../helpers/contractType';
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


function EmployeeAddDialog(props) {
    const classes = useStyles();

    const { open, handleClose, handleAdd, employeeToAdd, setEmployeeToAdd } = props;

    const handleChange = name => event => {
        var value;
        if(name === 'workStart' || name === 'workEnd'){
            value = moment(event).isValid()
                    ? moment(event).format('YYYY-MM-DD')
                    : null   
        }else{
            value = event.target.value;
        } 
        setEmployeeToAdd({ ...employeeToAdd, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        handleAdd();
    }

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>
                Ajouter de personnel
                <IconButton className={classes.closeButton} onClick={handleClose}>
                    <CloseIcon />
                </IconButton>
            </DialogTitle>
            <form autoComplete="off" onSubmit={handleSubmit}>
                <DialogContent className={classes.container} >
                    <TextField
                        autoFocus
                        label='Prénom'
                        required
                        defaultValue={employeeToAdd.firstName}
                        className={classes.textField}
                        onBlur={handleChange('firstName')}
                        margin="normal"
                    />
                    <TextField
                        label='Nom'
                        required
                        defaultValue={employeeToAdd.lastName}
                        className={classes.textField}
                        onBlur={handleChange('lastName')}
                        margin="normal"
                    />
                    <FormControl margin="normal" className={classes.textField}>
                        <InputLabel>Statut</InputLabel>
                        <Select                            
                            value={employeeToAdd.status}
                            onChange={handleChange('status')}                            
                        >
                            {EmployeeStatusList.map((status, i) =>
                                <MenuItem key={i} value={status}>{EmployeeStatusFR[status]}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                    <TextField                        
                        label='Journée (€)'
                        required
                        type='number'
                        inputProps={{ min: 0, step: 0.01 }}
                        defaultValue={employeeToAdd.salary}
                        className={classes.textField}
                        onBlur={handleChange('salary')}
                        margin="normal"
                    />
                    <TextField
                        label='Nr de compte'
                        required
                        defaultValue={employeeToAdd.accountNumber}
                        className={classes.textField}
                        onBlur={handleChange('accountNumber')}
                        margin="normal"
                    />
                    <FormControl margin="normal" className={classes.textField}>
                        <InputLabel>Contrat</InputLabel>
                        <Select                            
                            value={employeeToAdd.contractType}
                            onChange={handleChange('contractType')}                            
                        >
                            {ContractTypeList.map((contract, i) =>
                                <MenuItem key={i} value={contract}>{ContractTypeFR[contract]}</MenuItem>
                            )}
                        </Select>
                    </FormControl>
                    <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                        <KeyboardDatePicker
                            required={true}
                            margin="normal"
                            label="Commencé le travail"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={employeeToAdd.workStart}
                            className={classes.textField}
                            onChange={handleChange('workStart')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                        <KeyboardDatePicker
                            margin="normal"
                            label="Arrêté le travail"
                            cancelLabel="Annuler"
                            format="dd.MM.yyyy"
                            value={employeeToAdd.workEnd}
                            className={classes.textField}
                            onChange={handleChange('workEnd')}
                            KeyboardButtonProps={{
                                'aria-label': 'Changer la date',
                            }}
                        />
                    </MuiPickersUtilsProvider>
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

export default EmployeeAddDialog;