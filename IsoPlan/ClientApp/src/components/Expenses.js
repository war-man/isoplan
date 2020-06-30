import React from 'react';
import MaterialTable from 'material-table';
import { Paper, IconButton } from '@material-ui/core';
import { Localization } from '../helpers/localization';
import NoteAddIcon from '@material-ui/icons/NoteAdd';
import CloseIcon from '@material-ui/icons/Close';
import { getCurrentUser } from '../helpers/authentication';
import moment from 'moment';
import { expenseService } from '../services/expenseService';

const Expenses = (props) => {

    const { job, setMessage, setVariant, loading, setOpenSnackbar, getJob, to, uploadFile, deleteFile } = props;

    const possibleItems = preProcessData(job.jobItems);

    const columns = [
        { title: 'Nom', field: 'name' },
        {
            title: 'Date',
            field: 'date',
            type: 'date',
            render: rowData => <div>{moment(rowData.date).format('DD.MM.YYYY')}</div>
        },
        {
            title: 'Achat type',
            field: 'jobItemId',
            lookup: possibleItems,
        },
        {
            title: `Montant`,
            field: 'value',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            headerStyle: { textAlign: 'right' },
        },
        { title: 'Payé', field: 'paid', type: 'boolean' },
        {
            title: 'Date a payé',
            field: 'datePaid',
            type: 'date',
            render: rowData => <div>{moment(rowData.datePaid).format('DD.MM.YYYY')}</div>
        },
        {
            title: 'Document',
            field: 'filePath',
            editable: 'never',
            sorting: false,
            render: rowData => documentCell(rowData)
        },
    ];

    const handleFileSubmit = factureId => event => {
        event.preventDefault();
        var formData = new FormData();
        formData.append("file", event.target.files[0])
        uploadFile(factureId, formData)
    }

    const handleFileDelete = (id) => (event) => {
        deleteFile(id)
    }

    const clearInput = event => {
        event.target.value = ''
    }

    const documentCell = data => {
        if (data === undefined || data.tableData.editing === 'update') {
            return <div></div>
        }
        if (data.filePath.length === 0) {
            return (
                <div>
                    <IconButton
                        variant="contained"
                        component="label"
                        onClick={clearInput}
                        size={'small'}
                    >
                        <NoteAddIcon />
                        <input
                            type="file"
                            style={{ display: "none" }}
                            onChange={handleFileSubmit(data.id)}
                        />
                    </IconButton>
                </div>
            )
        } else {
            return (
                <div style={{ display: 'flex', alignItems: 'center' }}>
                    <a href={`${process.env.REACT_APP_API_URL}${to}/${data.id}?token=${getCurrentUser().token}`} target='_blank' rel="noopener noreferrer">Télécharger</a>
                    <IconButton onClick={handleFileDelete(data.id)} size={'small'} style={{ marginLeft: 4 }}>
                        <CloseIcon />
                    </IconButton>
                </div>
            )
        }
    }

    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: true,
        pageSize: 5,
        search: false,
        addRowPosition: 'first',
    }

    const editable = {
        onRowAdd: newData =>
            new Promise((resolve, reject) => {
                const item = {
                    id: newData.id,
                    jobId: job.id,
                    name: newData.name,
                    date: moment(newData.date).format("YYYY-MM-DD"),                    
                    value: parseFloat(newData.value),
                    paid: newData.paid,
                    datePaid: moment(newData.datePaid).format("YYYY-MM-DD"),
                    jobItemId: parseInt(newData.jobItemId)
                }
                expenseService.create(item)
                    .then(() => {
                        getJob(job.id)
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
                    date: moment(newData.date).format("YYYY-MM-DD"),
                    value: parseFloat(newData.value),
                    paid: newData.paid,
                    datePaid: moment(newData.datePaid).format("YYYY-MM-DD"),
                    jobItemId: parseInt(newData.jobItemId)
                }
                expenseService.update(item)
                    .then(() => {
                        getJob(job.id)
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
                expenseService.deleteExpense(oldData.id)
                    .then(() => {
                        getJob(job.id)
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

    return (
        <div>
            <MaterialTable
                components={{
                    Container: props => <Paper {...props} elevation={0} />,
                }}
                data={job.expenses}
                columns={columns}
                options={options}
                title={'Achat'}
                editable={editable}
                isLoading={loading}
                localization={Localization}
            />
        </div>
    )
}

export default Expenses;

function preProcessData(data) {
    var newData = {};
    data.forEach(e => {
        newData = { ...newData, [e.id]: e.name }
    });
    return newData;
}