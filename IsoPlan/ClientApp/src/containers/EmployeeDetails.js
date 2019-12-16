import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import { useParams } from 'react-router-dom';
import { employeeService } from '../services/employeeService';
import { EmployeeStatus, EmployeeStatusList, EmployeeStatusFR } from '../helpers/employeeStatus';
import { ContractType, ContractTypeList, ContractTypeFR } from '../helpers/contractType';
import { Grid, Paper, TextField, FormControl, MenuItem, Button, InputLabel, Select, makeStyles } from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import Files from '../components/Files';
import Snackbar from '@material-ui/core/Snackbar';
import CustomSnackbarContent from '../components/CustomSnackbarContent';
import moment from 'moment';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';

const useStyles = makeStyles(theme => ({
    paper: {
        padding: theme.spacing(1),
        textAlign: 'center',
    },
    container: {
        paddingLeft: theme.spacing(1),
        paddingRight: theme.spacing(1),
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
    actions: {
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
        width: '100%',
        textAlign: 'right',
    },
    snackbar: {
        margin: theme.spacing(1),
    },
    button: {
        marginRight: '8px'
    }
}));

function EmployeeDetails() {
    const classes = useStyles();
    const { id } = useParams();

    const handleChange = name => (event) => {
        var value;
        if (name === 'workStart' || name === 'workEnd') {
            value = moment(event).isValid()
                    ? moment(event).format('YYYY-MM-DD')
                    : null   
        } else {
            value = event.target.value;
        }
        setEmployee({ ...employee, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        employeeService.update(employee)
            .then(() => {
                getEmployee(id);
                setVariant("success");
                setMessage("Enregistrement réussi");
                setOpenSnackbar(true);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
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
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
            })
    }

    useEffect(() => {
        getEmployee(id)
        getFiles(id)
    }, [id]);

    const [files, setFiles] = useState([])
    const getFiles = id => {
        setLoadingFile(true)
        employeeService.getFiles(id)
            .then(res => {
                setFiles(res)
                setLoadingFile(false)
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setLoadingFile(false)
            })
    }
    const uploadFile = id => formData => {
        if (formData.get('file') === 'undefined') {
            return
        }
        setLoadingFile(true)
        employeeService.uploadFile(id, formData)
            .then(() => {
                getFiles(id)
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setLoadingFile(false);
            })
    }

    const [deleteId, setDeleteId] = useState(0)
    const deleteFile = () => {
        setConfirmOpen(false)
        employeeService.deleteFile(deleteId)
            .then(() => {                
                getFiles(id)
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
            })
    }

    const [variant, setVariant] = useState('success');
    const [message, setMessage] = useState('');
    const [openSnackbar, setOpenSnackbar] = useState(false);
    const handleCloseSnackbar = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        setOpenSnackbar(false);
    };

    const [confirmOpen, setConfirmOpen] = useState(false)
    const handleConfirmClose = () => {
        setConfirmOpen(false)
    }

    const [loadingFile, setLoadingFile] = useState(false)

    return (
        <Dashboard>
            <Grid container spacing={3}>
                <Grid item sm={12} md={5}>
                    <Paper className={classes.paper}>
                        <form autoComplete="off" onSubmit={handleSubmit} className={classes.container}>
                            <TextField
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
                                        <MenuItem key={i} value={status}>{EmployeeStatusFR[status]}</MenuItem>
                                    )}
                                </Select>
                            </FormControl>
                            <TextField
                                label='Per diem (€)'
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
                                    value={employee.workStart}
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
                                    value={employee.workEnd}
                                    className={classes.textField}
                                    onChange={handleChange('workEnd')}
                                    KeyboardButtonProps={{
                                        'aria-label': 'Changer la date',
                                    }}
                                />
                            </MuiPickersUtilsProvider>
                            <TextField
                                label="Remarque"
                                multiline
                                value={employee.note}
                                onChange={handleChange('note')}
                                className={classes.textField}
                                fullWidth
                                rowsMax={10}
                                margin="normal"
                                variant="outlined"
                            />
                            <div className={classes.textField}></div>
                            <div className={classes.actions}>
                                <Button variant="contained" color="primary" className={classes.button} onClick={() => getEmployee(id)}>
                                    Annuler
                                </Button>
                                <Button variant="contained" type="submit" color="primary">
                                    Enregistrer
                                </Button>
                            </div>
                        </form>
                    </Paper>
                </Grid>
                <Grid item sm={12} md={4}>
                    <Files
                        files={files}
                        uploadFile={uploadFile(id)}
                        deleteFile={(id) => {
                            setConfirmOpen(true); 
                            setDeleteId(id);
                        }}
                        to={'api/Employees/Files'}
                        isLoading={loadingFile}
                    />
                </Grid>
            </Grid>
            <Snackbar
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'center',
                }}
                open={openSnackbar}
                autoHideDuration={3000}
                onClose={handleCloseSnackbar}
            >
                <CustomSnackbarContent
                    onClose={handleCloseSnackbar}
                    variant={variant}
                    className={classes.snackbar}
                    message={message}
                />
            </Snackbar>
            <ConfirmDeleteDialog
                open={confirmOpen}
                handleClose={handleConfirmClose}
                handleDelete={deleteFile}
                text={'Confirmer la suppression de fichier?'}
            />
        </Dashboard>
    )
}

export default EmployeeDetails;