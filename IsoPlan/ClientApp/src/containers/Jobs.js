import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import MaterialTable from 'material-table';
import { Redirect } from 'react-router-dom';
import moment from 'moment';
import { jobService } from '../services/jobService';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import JobAddDialog from '../components/JobAddDialog';
import { DevisStatus, DevisStatusFR } from '../helpers/devisStatus';
import { JobStatus, JobStatusFR } from '../helpers/jobStatus';
import { Localization } from '../helpers/localization';
import { Paper, makeStyles, Typography } from '@material-ui/core';
import { DatePicker, MuiPickersUtilsProvider } from "@material-ui/pickers";
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale';

const useStyles = makeStyles(theme => ({
    toolbarTop: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        margin: `0px 0px ${theme.spacing(1)}px`,
        padding: theme.spacing(2)
    },
    toolbarText: {
        fontWeight: 'bold'
    },
    datePicker: {
        width: '150px'
    }
}));

function Jobs() {
    const classes = useStyles();
    const columns = [
        { title: 'Date devis', field: 'devisDate', render: rowData => { return rowData.devisDate && <div>{moment(rowData.devisDate).format('DD.MM.YYYY.')}</div> } },
        {
            title: 'Devis statut',
            field: 'devisStatus',
            render: rowData => <div>{DevisStatusFR[rowData.devisStatus]}</div>,
            customFilterAndSearch: (term, rowData) => DevisStatusFR[rowData.devisStatus].toUpperCase().includes(term.toUpperCase())
        },
        { title: 'Client', field: 'clientName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Affaire', field: 'name', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Début', field: 'startDate', render: rowData => { return rowData.startDate && <div>{moment(rowData.startDate).format('DD.MM.YYYY.')}</div> } },
        { title: 'Fin', field: 'endDate', render: rowData => { return rowData.endDate && <div>{moment(rowData.endDate).format('DD.MM.YYYY.')}</div> } },
        {
            title: 'Statut',
            field: 'status',
            render: rowData => <div>{JobStatusFR[rowData.status]}</div>,
            customFilterAndSearch: (term, rowData) => JobStatusFR[rowData.status].toUpperCase().includes(term.toUpperCase())
        },
        { title: 'Achat', field: 'totalBuy', type: 'currency', currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' } },
        { title: 'Vente', field: 'totalSell', type: 'currency', currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' } },
        { title: 'Marge', field: 'totalProfit', type: 'currency', currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' } },
        { title: 'RG', field: 'rgCollected', type: 'boolean' },
    ];
    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: false,
        rowStyle: (rowData) => {
            if (rowData.rgCollected === false && moment(rowData.rgDate).isValid() && moment(rowData.rgDate).isBefore(moment(new Date()))) {
                return {
                    backgroundColor: '#FFA726',
                }
            }
        }
    }
    const actions = [
        {
            icon: 'add_box',
            tooltip: 'Ajouter',
            isFreeAction: true,
            onClick: (event) => {
                setOpenAdd(true);
            }
        },
        rowData => ({
            icon: 'more_horiz',
            tooltip: 'Détails',
            onClick: (event, rowData) => {
                setRedirectId(rowData.id);
                setRedirect(true);
            }
        }),
        rowData => ({
            icon: 'delete',
            tooltip: 'Supprimer',
            onClick: (event, rowData) => {
                setDeleteId(rowData.id);
                setConfirmOpen(true);
            }
        }),
    ]

    const [jobToAdd, setJobToAdd] = useState({
        clientName: '',
        clientContact: '',
        name: '',
        address: '',
        status: JobStatus.Paused,
        devisStatus: DevisStatus.InProgress,
        devisDate: null,
        startDate: null,
        endDate: null,
        rgDate: null,
        rgCollected: false,
    })
    const handleAddJob = () => {
        handleCloseAdd();
        jobService.create(jobToAdd)
            .then(() => {
                getJobs();
            })
            .catch(err => alert(err))
    }
    const [openAdd, setOpenAdd] = useState(false)
    const handleCloseAdd = () => {
        setOpenAdd(false);
        setJobToAdd({
            clientName: '',
            clientContact: '',
            name: '',
            address: '',
            status: JobStatus.Paused,
            devisStatus: DevisStatus.InProgress,
            devisDate: null,
            startDate: null,
            endDate: null,
            rgDate: null,
            rgCollected: false,
        });
    }

    const [deleteId, setDeleteId] = useState(0)
    const [confirmOpen, setConfirmOpen] = useState(false)
    const handleConfirmClose = () => {
        setConfirmOpen(false)
    }
    const handleDeleteJob = () => {
        jobService.deleteJob(deleteId)
            .then(() => {
                handleConfirmClose()
                getJobs()
            })
            .catch(err => alert(err))
    }

    const [data, setData] = useState([])
    const getJobs = (params) => {
        setLoading(true)
        jobService.getAll(params)
            .then(res => {
                setData(res)
                setLoading(false)
            })
            .catch(err => alert(err))
    }
    useEffect(() => {
        getJobs()
    }, [])

    useEffect(() => {
        const recalculate = () => {
            setTotal({
                buy: data.map(x => x.totalBuy).reduce((a, b) => a + b, 0),
                sell: data.map(x => x.totalSell).reduce((a, b) => a + b, 0),
                profit: data.map(x => x.totalProfit).reduce((a, b) => a + b, 0),
            })
        }
        recalculate()
    }, [data])

    const [loading, setLoading] = useState(false)

    const [redirect, setRedirect] = useState(false);
    const [redirectId, setRedirectId] = useState(0);

    const renderRedirect = () => {
        if (redirect) {
            return <Redirect push to={`${redirectId}`} />
        }
    }

    const [date, setDate] = useState(null)
    const [total, setTotal] = useState({
        buy: 0,
        sell: 0,
        profit: 0
    })

    const handleDateChange = (date) => {
        if(!moment(date).isValid()){            
            getJobs();
            setDate(null);
        } else{
            getJobs([{
                name: 'startDate',
                value: moment(date).format('YYYY-MM-01')
            }]);
            setDate(date);
        }        
    }

    return (
        <Dashboard maxWidth={false} title={'Travaux'}>
            {renderRedirect()}
            <Paper className={classes.toolbarTop}>
                <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                    <DatePicker
                        clearable
                        clearLabel='Supprimer'
                        cancelLabel="Annuler"
                        views={["month", "year"]}
                        label="Début après:"
                        className={classes.datePicker}
                        value={date}
                        onChange={(date) => handleDateChange(date)}
                    />
                </MuiPickersUtilsProvider>
                <Typography className={classes.toolbarText}>
                    {`Achat total: ${total.buy}€ | Vente total: ${total.sell}€ | Marge total: ${total.profit}€`}
                </Typography>
            </Paper>
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Travaux"
                isLoading={loading}
                localization={Localization}
            />
            <JobAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                handleAdd={handleAddJob}
                jobToAdd={jobToAdd}
                setJobToAdd={setJobToAdd}
            />
            <ConfirmDeleteDialog
                open={confirmOpen}
                handleClose={handleConfirmClose}
                handleDelete={handleDeleteJob}
                text={'Confirmer la suppression de travail?'}
            />
        </Dashboard>
    )
}

export default Jobs;