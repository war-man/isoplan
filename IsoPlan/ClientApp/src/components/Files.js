import React from 'react';
import { Grid, Paper, makeStyles, List, ListSubheader, IconButton, ListItem, ListItemIcon, ListItemText, ListItemSecondaryAction } from '@material-ui/core';
import AddBoxIcon from '@material-ui/icons/AddBox';
import DescriptionIcon from '@material-ui/icons/Description';
import DeleteIcon from '@material-ui/icons/Delete';

const useStyles = makeStyles(theme => ({
    paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
    },
    header: {
        paddingLeft: 0,
        paddingRight: 0
    },
    headerText: {
        fontSize: 16
    }
}));

function Files(props) {
    const classes = useStyles();

    const { files, uploadFile, deleteFile, to} = props

    const handleFileSubmit = (event) => {
        var formData = new FormData();
        formData.append("file", event.target.files[0])
        uploadFile(formData)
    }

    const handleFileDelete = (id) => () => {
        deleteFile(id)
    }

    return (
        <Paper className={classes.paper}>
            <form>
                <List dense={true}
                    subheader={
                        <ListSubheader component="div" className={classes.header}>
                            <Grid
                                justify="space-between"
                                container
                            >
                                <Grid item className={classes.headerText}>
                                    Fichiers
                                </Grid>
                                <Grid>
                                    <IconButton
                                        variant="contained"
                                        component="label"
                                    >
                                        <AddBoxIcon />
                                        <input
                                            type="file"
                                            style={{ display: "none" }}
                                            onChange={handleFileSubmit}
                                        />
                                    </IconButton>
                                </Grid>
                            </Grid>
                        </ListSubheader>
                    }>
                    {
                        files.map(file =>
                            <ListItem key={file.id} component={'a'} href={`${to}/${file.id}`} target='_blank'>
                                <ListItemIcon>
                                    <DescriptionIcon />
                                </ListItemIcon>
                                <ListItemText 
                                    primary={file.name}
                                />
                                <ListItemSecondaryAction>
                                    <IconButton edge="end" onClick={handleFileDelete(file.id)}>
                                        <DeleteIcon />
                                    </IconButton>
                                </ListItemSecondaryAction>
                            </ListItem>
                        )
                    }
                </List>
            </form>
        </Paper>
    )
}

export default Files;