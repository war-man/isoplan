import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import { useParams } from 'react-router-dom';
import { Paper, makeStyles, Snackbar } from '@material-ui/core';
import { DevisStatus } from '../helpers/devisStatus';
import { JobStatus } from '../helpers/jobStatus';
import Files from '../components/Files';
import { jobService } from '../services/jobService';
import { factureService } from '../services/factureService';
import CustomSnackbarContent from '../components/CustomSnackbarContent';
import moment from 'moment';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import JobItems from '../components/JobItems';
import JobDetailsForm from '../components/JobDetailsForm';
import Factures from '../components/Factures';

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
        factures: []
    })

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

    const uploadFactureFile = (factureId, formData) => {
        setLoading(true)
        factureService.uploadFile(factureId, formData)
            .then(() => {
                getJob(id);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setLoading(false);
            })
    }

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

    useEffect(() => {
        setLoading(true)
        getJob(id)
        getFiles(id)
    }, [id]);

    const [deleteMode, setDeleteMode] = useState('file');
    const deleteFunction = () => {
        if (deleteMode === 'file') {
            deleteFile();
        } else if (deleteMode === 'facture') {
            deleteFactureFile();
        }
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

    const deleteFactureFile = () => {
        setConfirmOpen(false);
        setLoading(true);
        factureService.deleteFile(deleteId)
            .then(() => {
                getJob(id);
            })
            .catch(err => {
                setVariant("error");
                setMessage(err);
                setOpenSnackbar(true);
                setLoading(false);
            })
    }

    return (
        <Dashboard title={`${job.name} (${job.clientName})`}>
            <Paper className={classes.paper} square>
                <JobDetailsForm
                    id={id}
                    job={job}
                    getJob={getJob}
                    handleChange={handleChange}
                    handleSubmit={handleSubmit}
                />
            </Paper>            
            <Paper square className={classes.topMargin}>
                <JobItems
                    id={id}
                    job={job}
                    setMessage={setMessage}
                    setVariant={setVariant}
                    loading={loading}
                    setOpenSnackbar={setOpenSnackbar}
                    getJob={getJob}
                />
            </Paper>
            <Paper square className={classes.topMargin}>
                <Factures
                    job={job}
                    setMessage={setMessage}
                    setVariant={setVariant}
                    loading={loading}
                    setOpenSnackbar={setOpenSnackbar}
                    getJob={getJob}
                    to={'api/Factures/Files'}
                    uploadFile={uploadFactureFile}
                    deleteFile={(id) => {
                        setConfirmOpen(true);
                        setDeleteId(id);
                        setDeleteMode('facture')
                    }}
                />
            </Paper>
            <div style={{ marginTop: 16 }}>
                <Files
                    files={files}
                    uploadFile={uploadFile(id)}
                    deleteFile={(id) => {
                        setConfirmOpen(true);
                        setDeleteId(id);
                        setDeleteMode('file')
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
                handleDelete={deleteFunction}
                text={'Confirmer la suppression de fichier?'}
            />
        </Dashboard>
    )
}

export default JobDetails;


const useStyles = makeStyles(theme => ({
    paper: {
        padding: `${theme.spacing(2)}px ${theme.spacing(1)}px`,
        textAlign: 'center',
    },
    snackbar: {
        margin: theme.spacing(1),
    },
    topMargin: {
        marginTop: theme.spacing(2),
    }
}));