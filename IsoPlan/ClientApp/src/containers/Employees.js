import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import MaterialTable from 'material-table';
import { employeeService } from '../services/employeeService';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import EmployeeAddDialog from '../components/EmployeeAddDialog';
import { EmployeeStatus, EmployeeStatusFR } from '../helpers/employeeStatus';
import { ContractType, ContractTypeFR } from '../helpers/contractType';
import { Redirect } from 'react-router-dom';
import moment from 'moment';
import { Localization } from '../helpers/localization';

function Employees() {
    const columns = [
        { title: 'Prénom', field: 'firstName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Nom', field: 'lastName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        {
            title: 'Statut',
            field: 'status',
            render: rowData => <div>{EmployeeStatusFR[rowData.status]}</div>,
            customFilterAndSearch: (term, rowData) => EmployeeStatusFR[rowData.status].toUpperCase().includes(term.toUpperCase())
        },
        { title: 'Per diem', field: 'salary', type: 'currency', currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' } },
        { title: 'Numéro de compte', field: 'accountNumber', sorting: false, cellStyle: { maxWidth: '200px', overflowWrap: 'break-word' } },
        { 
            title: 'Contrat', 
            field: 'contractType', 
            render: rowData => <div>{ContractTypeFR[rowData.contractType]}</div>,
            customFilterAndSearch: (term, rowData) => ContractTypeFR[rowData.contractType].toUpperCase().includes(term.toUpperCase())
        },
        { title: 'Commencé', field: 'workStart', render: rowData => { return rowData.workStart && <div>{moment(rowData.workStart).format('DD.MM.YYYY')}</div> } },
        { title: 'Arrêté', field: 'workEnd', render: rowData => { return rowData.workEnd && <div>{moment(rowData.workEnd).format('DD.MM.YYYY')}</div> } },
    ];
    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: false,
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

    const [employeeToAdd, setEmployeeToAdd] = useState({
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
    const handleAddEmployee = () => {
        employeeService.create(employeeToAdd)
            .then(() => {
                handleCloseAdd();
                getEmployees();
            })
            .catch(err => {
                alert(err);
            })
    }

    const [openAdd, setOpenAdd] = useState(false)
    const handleCloseAdd = () => {
        setOpenAdd(false);
        setEmployeeToAdd({
            firstName: '',
            lastName: '',
            salary: '',
            accountNumber: '',
            contractType: ContractType.Indefinite,
            workStart: null,
            workEnd: null,
            status: EmployeeStatus.Active,
            note: '',
        });
    }

    const [deleteId, setDeleteId] = useState(0)
    const [confirmOpen, setConfirmOpen] = useState(false)
    const handleConfirmClose = () => {
        setConfirmOpen(false)
    }
    const handleDeleteEmployee = () => {
        employeeService.deleteEmployee(deleteId)
            .then(() => {
                handleConfirmClose()
                getEmployees()
            })
            .catch(err => {
                alert(err)
            })
    }

    const [data, setData] = useState([])
    const getEmployees = () => {
        employeeService.getAll()
            .then(res => {
                setData(res)
                setLoading(false)
            })
            .catch(err => {
                alert(err)
            })
    }
    useEffect(() => {
        setLoading(true)
        getEmployees()
    }, [])

    const [loading, setLoading] = useState(false)
    const [redirect, setRedirect] = useState(false);
    const [redirectId, setRedirectId] = useState(0);

    const renderRedirect = () => {
        if (redirect) {
            return <Redirect push to={`${redirectId}`} />
        }
    }

    return (
        <Dashboard>
            {renderRedirect()}
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Personnel"
                isLoading={loading}
                localization={Localization}
            />
            <EmployeeAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                handleAdd={handleAddEmployee}
                employeeToAdd={employeeToAdd}
                setEmployeeToAdd={setEmployeeToAdd}
            />
            <ConfirmDeleteDialog
                open={confirmOpen}
                handleClose={handleConfirmClose}
                handleDelete={handleDeleteEmployee}
                text={'Confirmer la suppression de personnel?'}
            />
        </Dashboard>
    )
}

export default Employees