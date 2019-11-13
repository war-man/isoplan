import React from 'react';
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions, Button } from '@material-ui/core';

function ConfirmDeleteDialog(props) {
    const { open, handleClose, handleDelete, text } = props;
    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Confirmer la suppression</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    {text}
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button variant="contained" onClick={handleDelete} color="secondary" >
                    Supprimer
                </Button>
                <Button variant="contained" onClick={handleClose} color="primary" autoFocus>
                    Annuler
                </Button>
            </DialogActions>
        </Dialog>
    )
}

export default ConfirmDeleteDialog;