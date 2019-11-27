import React from 'react';
import { Grid, Paper, makeStyles, List, ListSubheader, IconButton, ListItem, ListItemIcon, ListItemText, ListItemSecondaryAction } from '@material-ui/core';
import AddBoxIcon from '@material-ui/icons/AddBox';
import DescriptionIcon from '@material-ui/icons/Description';
import DeleteIcon from '@material-ui/icons/Delete';

const useStyles = makeStyles(theme => ({
    paper: {
        paddingLeft: theme.spacing(2),
        paddingRight: theme.spacing(2),
        textAlign: 'center',
        overflow: 'auto',
        maxHeight: '240px'
    },
    header: {
        paddingLeft: 0,
        paddingRight: 0,
        backgroundColor: 'white'
    },
    headerText: {
        fontSize: 16
    },
    listText: {
        wordWrap: 'break-word'
    }
}));

function Files(props) {
    const classes = useStyles();

    const { files, to, uploadFile, deleteFile } = props

    const handleFileSubmit = (event) => {
        var formData = new FormData();
        formData.append("file", event.target.files[0])
        uploadFile(formData)
    }

    const handleFileDelete = (id) => () => {
        deleteFile(id)
    }

    const clearInput = event => {
        event.target.value = ''
    }

    return (
        <Paper className={classes.paper}>
            <form>
                <List dense={true}>
                    {                                               
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
                                                    type="file"
                                                    style={{ display: "none" }}
                                                    onChange={handleFileSubmit}
                                                />
                                            </IconButton>
                                        </Grid>
                                    </Grid>
                                </ListSubheader>
                                {
                                    items.map(item =>
                                        <ListItem key={item.id} component={'a'} href={`${to}/${item.id}`} target='_blank'>
                                            <ListItemIcon>
                                                <DescriptionIcon />
                                            </ListItemIcon>
                                            <ListItemText className={classes.listText}
                                                primary={item.name}
                                            />
                                            <ListItemSecondaryAction>
                                                <IconButton edge="end" onClick={handleFileDelete(item.id)}>
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