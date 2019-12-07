import React, { useState, useEffect } from 'react';
import Dashboard from '../components/Dashboard';
import { userService } from '../services/userService';
import MaterialTable from 'material-table';
import UserAddDialog from '../components/UserAddDialog';
import UserEditDialog from '../components/UserEditDialog';
import { Role, AllRoles, RoleFR } from '../helpers/role';
import ConfirmDeleteDialog from '../components/ConfirmDeleteDialog';
import { getCurrentUser } from '../helpers/authentication';
import { Localization } from '../helpers/localization';

function Users() {
    const columns = [
        { title: 'Prénom', field: 'firstName' },
        { title: 'Nom', field: 'lastName' },
        { title: 'Username', field: 'username' },
        {
            title: 'Rôle',
            field: 'role',
            render: rowData => <div>{RoleFR[rowData.role]}</div>,
            customFilterAndSearch: (term, rowData) => RoleFR[rowData.role].toUpperCase().includes(term.toUpperCase())
        },
    ]
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
                setOpenAdd(true)
            }
        },
        {
            icon: 'edit',
            tooltip: 'Détails',
            onClick: (event, rowData) => {
                setOpenEdit(true)
                setUserToEdit({ ...rowData })
            }
        },
        rowData => ({
            icon: 'delete',
            tooltip: 'Supprimer',
            disabled: getCurrentUser().id === rowData.id,
            onClick: (event, rowData) => {
                setUserToEdit({ ...rowData })
                setConfirmOpen(true)
            }
        })
    ]

    const roleList = [...AllRoles]

    const [openAdd, setOpenAdd] = useState(false)
    const [userToAdd, setUserToAdd] = useState({
        firstName: '',
        lastName: '',
        username: '',
        password: '',
        role: Role.User,
    })
    const handleAddUser = () => {
        userService.create(userToAdd)
            .then(() => {
                handleCloseAdd()
                getUsers()
            })
            .catch(err => {
                alert(err)
            })
    }
    const handleCloseAdd = () => {
        setOpenAdd(false)
        setUserToAdd({
            firstName: '',
            lastName: '',
            username: '',
            password: '',
            role: Role.User,
        })
    }

    const [openEdit, setOpenEdit] = useState(false)
    const [userToEdit, setUserToEdit] = useState({})
    const handleSaveUser = () => {
        userService.update(userToEdit)
            .then(() => {
                handleCloseEdit()
                getUsers()
            })
            .catch(err => {
                alert(err)
            })
    }
    const handleDeleteUser = () => {
        userService.deleteUser(userToEdit.id)
            .then(() => {
                handleConfirmClose()
                getUsers()
            })
            .catch(err => {
                alert(err)
            })
    }
    const handleCloseEdit = () => {
        setOpenEdit(false)
    }

    const [confirmOpen, setConfirmOpen] = useState(false)
    const handleConfirmClose = () => {
        setConfirmOpen(false)
    }

    const [data, setData] = useState([])
    const getUsers = () => {
        userService.getAll()
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
        getUsers()
    }, [])

    const [loading, setLoading] = useState(false)

    return (
        <Dashboard>
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Usagers"
                isLoading={loading}
                localization={Localization}
            />
            <UserAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                userToAdd={userToAdd}
                setUserToAdd={setUserToAdd}
                handleAdd={handleAddUser}
                roleList={roleList}
            />
            <UserEditDialog
                open={openEdit}
                handleClose={handleCloseEdit}
                userToEdit={userToEdit}
                setUserToEdit={setUserToEdit}
                roleList={roleList}
                handleSave={handleSaveUser}
                handleDelete={handleDeleteUser}
            />
            <ConfirmDeleteDialog
                open={confirmOpen}
                handleClose={handleConfirmClose}
                handleDelete={handleDeleteUser}
                text={'Confirmer la suppression de usager?'}
            />
        </Dashboard>
    )
}

export default Users