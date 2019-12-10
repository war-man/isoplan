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
import { Snackbar, makeStyles } from '@material-ui/core';
import CustomSnackbarContent from '../components/CustomSnackbarContent';

const useStyles = makeStyles(theme => ({
    snackbar: {
        margin: theme.spacing(1),
    }
}));

function Users() {
    const classes = useStyles();
    const columns = [
        { title: 'Prénom', field: 'firstName' },
        { title: 'Nom', field: 'lastName' },
        { title: "Nom d'utilisateur", field: 'username' },
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
                setVariant('error');
                setMessage(err);
                setOpenSnackbar(true);
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
                setVariant('error');
                setMessage(err);
                setOpenSnackbar(true);
            })
    }
    const handleDeleteUser = () => {
        userService.deleteUser(userToEdit.id)
            .then(() => {
                handleConfirmClose()
                getUsers()
            })
            .catch(err => {
                setVariant('error');
                setMessage(err);
                setOpenSnackbar(true);
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
                setVariant('error');
                setMessage(err);
                setOpenSnackbar(true);
            })
    }

    useEffect(() => {
        setLoading(true)
        getUsers()
    }, [])

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

    return (
        <Dashboard title={'Usagers'}>
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
        </Dashboard>
    )
}

export default Users