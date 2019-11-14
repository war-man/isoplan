import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import MaterialTable from 'material-table';
import { employeeService } from '../services/employeeService';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import EmployeeAddDialog from '../components/EmployeeAddDialog';
import { EmployeeStatus, EmployeeStatusList } from '../helpers/employeeStatus';
import { ContractType, ContractTypeList } from '../helpers/contractType';
import moment from 'moment';

function Employees(){
    const columns = [
        { title: 'Prénom', field: 'firstName', cellStyle: {maxWidth: '150px', overflowWrap: 'break-word'}},
        { title: 'Nom', field: 'lastName', cellStyle: {maxWidth: '150px', overflowWrap: 'break-word'}},
        { title: 'Statut', field: 'status'},
        { title: 'Per diem', field: 'salary', type: 'currency', currencySetting: {currencyCode: 'EUR', locale: 'fr-FR'}},
        { title: 'Numéro de compte', field: 'accountNumber', sorting: false},
        { title: 'Contrat', field: 'contractType' },
        { title: 'Commencé', field: 'workStart', render: rowData => {return rowData.workStart && <div>{moment(rowData.workStart).format('DD.MM.YYYY')}</div>}},
        { title: 'Arrêté', field: 'workEnd', render: rowData => {return rowData.workEnd && <div>{moment(rowData.workEnd).format('DD.MM.YYYY')}</div>}},
    ];
    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
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
            icon: 'delete',
            tooltip: 'Supprimer',
            onClick: (event, rowData) => {
                setDeleteId(rowData.id);
                setConfirmOpen(true);
            }
        })
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
        }) ;
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
            })
            .catch(err => {
                alert(err)
            })
    }
    useEffect(() => {
        getEmployees()
    }, [])

    return (
        <Dashboard>
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Personnel"
            />
            <EmployeeAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                handleAdd={handleAddEmployee}
                employeeToAdd={employeeToAdd}
                setEmployeeToAdd={setEmployeeToAdd}          
                employeeStatusList={EmployeeStatusList}
                contractTypeList={ContractTypeList}
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