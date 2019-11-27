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

function Jobs() {
    const columns = [
        { title: 'Date devis', field: 'devisDate', render: rowData => { return rowData.devisDate && <div>{moment(rowData.devisDate).format('DD.MM.YYYY.')}</div> } },
        { title: 'Devis statut', field: 'devisStatus', render: rowData => <div>{DevisStatusFR[rowData.devisStatus]}</div>},
        { title: 'Client', field: 'clientName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Affaire', field: 'name', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Début', field: 'startDate', render: rowData => { return rowData.startDate && <div>{moment(rowData.startDate).format('DD.MM.YYYY.')}</div> } },
        { title: 'Fin', field: 'endDate', render: rowData => { return rowData.endDate && <div>{moment(rowData.endDate).format('DD.MM.YYYY.')}</div> } },
        { title: 'Statut', field: 'status', render: rowData => <div>{JobStatusFR[rowData.status]}</div>},
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
        search: false,
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
        status: JobStatus.Started,
        devisStatus: DevisStatus.InProgress,
        devisDate: null,
        startDate: null,
        endDate: null,
        rgDate: null,
        rgCollected: false,
    })
    const handleAddJob = () => {
        jobService.create(jobToAdd)
            .then(() => {
                handleCloseAdd();
                getJobs();
            })
            .catch(err => {
                alert(err);
            })
    }
    const [openAdd, setOpenAdd] = useState(false)
    const handleCloseAdd = () => {
        setOpenAdd(false);
        setJobToAdd({
            clientName: '',
            clientContact: '',
            name: '',
            address: '',
            status: JobStatus.Started,
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
            .catch(err => {
                alert(err)
            })
    }

    const [data, setData] = useState([])
    const getJobs = () => {
        jobService.getAll()
            .then(res => {
                setData(res)
            })
            .catch(err => {
                alert(err)
            })
    }
    useEffect(() => {
        getJobs()
    }, [])

    const [redirect, setRedirect] = useState(false);
    const [redirectId, setRedirectId] = useState(0);

    const renderRedirect = () => {
        if (redirect) {
            return <Redirect push to={`${redirectId}`} />
        }
    }

    return (
        <Dashboard maxWidth={false}>
            {renderRedirect()}
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Travaux"
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