import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import { useParams } from 'react-router-dom';
import {
    Grid, Paper, makeStyles, TextField, Button, FormControl,
    MenuItem, InputLabel, Select, Checkbox, FormControlLabel, Snackbar
} from '@material-ui/core';
import { MuiPickersUtilsProvider, KeyboardDatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import { DevisStatusFR, DevisStatusList, DevisStatus } from '../helpers/devisStatus';
import { JobStatusFR, JobStatusList, JobStatus } from '../helpers/jobStatus';
import Files from '../components/Files';
import MaterialTable from 'material-table';
import { jobService } from '../services/jobService';
import CustomSnackbarContent from '../components/CustomSnackbarContent';
import moment from 'moment';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import { Localization } from '../helpers/localization';


const useStyles = makeStyles(theme => ({
    paper: {
        padding: `${theme.spacing(2)}px ${theme.spacing(1)}px`,
        textAlign: 'center',
    },
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
    snackbar: {
        margin: theme.spacing(1),
    },
    totalRow: {
        fontWeight: 'bold',
    },
    button: {
        marginRight: theme.spacing(1),
    },
    topMargin: {
        marginTop: theme.spacing(2),
    }
}));

function JobDetails() {
    const classes = useStyles();
    const { id } = useParams();

    const handleChange = name => event => {
        var value;
        if (name === 'devisDate' || name === 'startDate' || name === 'endDate' || name === 'rgDate') {
            value = moment(event).isValid()
                ? moment(event).format('YYYY-MM-DD')
                : null

        } else if (name === 'rgCollected') {
            value = event.target.checked;
        }
        else {
            value = event.target.value;
        }
        setJob({ ...job, [name]: value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        jobService.update(job)
            .then(() => {
                setVariant('success')
                setMessage('Enregistrement réussi')
                setOpenSnackbar(true);
                getJob(id);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
            })
    }

    const [job, setJob] = useState({
        clientName: '',
        clientContact: '',
        name: '',
        address: '',
        status: JobStatus.Started,
        devisStatus: DevisStatus.InProgress,
        totalBuy: 0,
        totalSell: 0,
        totalProfit: 0,
        devisDate: null,
        startDate: null,
        endDate: null,
        rgDate: null,
        rgCollected: false,
        jobItems: [],
    })

    const columns = [
        { title: 'Type de travail', field: 'name' },
        { title: 'Quantité', field: 'quantity', type: 'numeric' },
        {
            title: `Achat [${job.totalBuy.toFixed(2).toLocaleString('fr-FR')}€]`,
            field: 'buy',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            headerStyle: { textAlign: 'right' },
        },
        {
            title: `Vente [${job.totalSell.toFixed(2).toLocaleString('fr-FR')}€]`,
            field: 'sell',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            headerStyle: { textAlign: 'right' },
        },
        {
            title: `Marge [${job.totalProfit.toFixed(2).toLocaleString('fr-FR')}€]`,
            field: 'profit',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            editable: 'never',
            headerStyle: { textAlign: 'right' },
        },
    ];

    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: false,
        search: false,
        addRowPosition: 'first',
        sorting: false,
    }

    const editable = {
        onRowAdd: newData =>
            new Promise((resolve, reject) => {
                const item = {
                    jobId: job.id,
                    name: newData.name,
                    quantity: parseInt(newData.quantity),
                    buy: parseFloat(newData.buy),
                    sell: parseFloat(newData.sell),
                }
                jobService.createItem(item)
                    .then(() => {
                        getJob(id)
                        resolve()
                    })
                    .catch(() => {
                        setVariant('error')
                        setMessage('Entrée invalide')
                        setOpenSnackbar(true)
                        reject()
                    })
            }),
        onRowUpdate: (newData, oldData) =>
            new Promise((resolve, reject) => {
                const item = {
                    id: newData.id,
                    jobId: job.id,
                    name: newData.name,
                    quantity: parseInt(newData.quantity),
                    buy: parseFloat(newData.buy),
                    sell: parseFloat(newData.sell),
                }
                jobService.updateItem(item)
                    .then(() => {
                        getJob(id)
                        resolve()
                    })
                    .catch(() => {
                        setVariant('error')
                        setMessage('Entrée invalide')
                        setOpenSnackbar(true)
                        reject()
                    })
            }),
        onRowDelete: oldData =>
            new Promise((resolve, reject) => {
                jobService.deleteItem(oldData.id)
                    .then(() => {
                        getJob(id)
                        resolve()
                    })
                    .catch(() => {
                        setVariant('error')
                        setMessage('Erreur')
                        setOpenSnackbar(true)
                        reject()
                    })
            }),
    }

    const getJob = (id) => {
        jobService.get(id)
            .then(res => {
                setJob({ ...res })
                setLoading(false)
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
            })
    }

    const [files, setFiles] = useState([])
    const getFiles = id => {
        setFileLoading(true)
        jobService.getFiles(id)
            .then(res => {
                setFiles(res)
                setFileLoading(false);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setFileLoading(false)
            })
    }
    const uploadFile = id => formData => {
        if (formData.get('file') === 'undefined') {
            return
        }
        setFileLoading(true)
        jobService.uploadFile(id, formData)
            .then(() => {
                getFiles(id);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setFileLoading(false);
            })
    }
    const [deleteId, setDeleteId] = useState(0)
    const deleteFile = () => {
        setConfirmOpen(false);
        jobService.deleteFile(deleteId)
            .then(() => getFiles(id))
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setFileLoading(false);
            })
    }

    useEffect(() => {
        setLoading(true)
        getJob(id)
        getFiles(id)
    }, [id]);

    const [loading, setLoading] = useState(false)

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

    const [fileLoading, setFileLoading] = useState(false)

    return (
        <Dashboard title={`${job.name} (${job.clientName})`}>
            <Paper className={classes.paper} square>
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
            </Paper>
            <Paper square className={classes.topMargin}>
                <MaterialTable
                    components={{
                        Container: props => <Paper {...props} elevation={0} />,
                    }}
                    data={job.jobItems}
                    columns={columns}
                    options={options}
                    title={'Travaux'}
                    editable={editable}
                    isLoading={loading}
                    localization={Localization}
                />
            </Paper>
            <div style={{ marginTop: 16 }}>
                <Files
                    files={files}
                    uploadFile={uploadFile(id)}
                    deleteFile={(id) => {
                        setConfirmOpen(true);
                        setDeleteId(id);
                    }}
                    to={'api/Jobs/Files'}
                    isLoading={fileLoading}
                />
            </div>

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

export default JobDetails;