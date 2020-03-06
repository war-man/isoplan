import React from 'react';
import { Localization } from '../helpers/localization';
import { Paper } from '@material-ui/core';
import MaterialTable from 'material-table';
import { jobService } from '../services/jobService';

const JobItems = (props) => {
    const { job, setMessage, setVariant, loading, setOpenSnackbar, getJob, id } = props;

    const columns = [
        { title: 'Type de travail', field: 'name' },
        { title: 'Quantité', field: 'quantity', type: 'numeric' },
        {
            title: `Achat [${Number(job.totalBuy.toFixed(2)).toLocaleString('fr-FR', { style: 'currency', currency: 'EUR' })}]`,
            field: 'buy',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            headerStyle: { textAlign: 'right' },
        },
        {
            title: `Vente [${Number(job.totalSell.toFixed(2)).toLocaleString('fr-FR', { style: 'currency', currency: 'EUR' })}]`,
            field: 'sell',
            type: 'currency',
            currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' },
            headerStyle: { textAlign: 'right' },
        },
        {
            title: `Marge [${Number(job.totalProfit.toFixed(2)).toLocaleString('fr-FR', { style: 'currency', currency: 'EUR' })}]`,
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

    return (
        <div>
            <MaterialTable
                components={{
                    Container: props => <Paper {...props} elevation={0} />,
                }}
                data={job.jobItems}
                columns={columns}
                options={options}
                title={'Previsionel'}
                editable={editable}
                isLoading={loading}
                localization={Localization}
            />
        </div>
    )
}

export default JobItems;