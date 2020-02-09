import React from 'react';
import { Grid, Paper, makeStyles, List, ListSubheader, IconButton, ListItem, ListItemIcon, ListItemText, ListItemSecondaryAction, CircularProgress } from '@material-ui/core';
import AddBoxIcon from '@material-ui/icons/AddBox';
import DescriptionIcon from '@material-ui/icons/Description';
import DeleteIcon from '@material-ui/icons/Delete';
import { getCurrentUser } from '../helpers/authentication';

const useStyles = makeStyles(theme => ({
    paper: {
        textAlign: 'center',
        overflow: 'auto',
        maxHeight: '640px'
    },
    header: {
        paddingLeft: theme.spacing(2),
        paddingRight: theme.spacing(2),
        backgroundColor: '#e8eaf6',
    },
    listText: {
        paddingTop: theme.spacing(1),
        paddingBottom: theme.spacing(1)
    },
    headerText: {
        fontSize: 16
    },
    emptyText: {
        color: 'rgba(0, 0, 0, 0.54)'
    }
}));

function Files(props) {
    const classes = useStyles();

    const { files, to, uploadFile, deleteFile, isLoading } = props

    const handleFileSubmit = header => event => {
        event.preventDefault();
        var formData = new FormData();
        formData.append("folder", header)
        for (let i = 0; i < event.target.files.length; i++) {
            formData.append("files", event.target.files[i])
        }
        uploadFile(formData)
    }

    const handleFileDelete = (id) => (event) => {
        deleteFile(id)
    }

    const clearInput = event => {
        event.target.value = ''
    }

    return (
        <Paper className={classes.paper} square>
            <form>
                <List dense={true} style={{ padding: 0 }}>
                    {isLoading ?
                        <CircularProgress />
                        :
                        files.map(({ header, items }, i) =>
                            <div key={i}>
                                <ListSubheader component="div" className={classes.header}>
                                    <Grid
                                        justify="space-between"
                                        container
                                    >
                                        <Grid item className={classes.headerText}>
                                            {header}
                                        </Grid>
                                        <Grid>
                                            <IconButton
                                                variant="contained"
                                                component="label"
                                                onClick={clearInput}
                                            >
                                                <AddBoxIcon />
                                                <input
                                                    multiple
                                                    type="file"
                                                    style={{ display: "none" }}
                                                    onChange={handleFileSubmit(header)}
                                                />
                                            </IconButton>
                                        </Grid>
                                    </Grid>
                                </ListSubheader>
                                {
                                    items.length === 0 ?
                                        <ListItem className={`${classes.emptyText} ${classes.listText}`}>
                                            Ce dossier est vide
                                        </ListItem>
                                        :
                                        items.map(item =>
                                            <ListItem key={item.id} component={'a'} href={`${process.env.REACT_APP_API_URL}${to}/${item.id}?token=${getCurrentUser().token}`} target='_blank'>
                                                <ListItemIcon>
                                                    <DescriptionIcon />
                                                </ListItemIcon>
                                                <ListItemText className={classes.listText}
                                                    primary={item.name}
                                                />
                                                <ListItemSecondaryAction>
                                                    <IconButton onClick={handleFileDelete(item.id)}>
                                                        <DeleteIcon />
                                                    </IconButton>
                                                </ListItemSecondaryAction>
                                            </ListItem>
                                        )
                                }
                            </div>
                        )
                    }
                </List>
            </form>
        </Paper>
    )
}

export default Files;