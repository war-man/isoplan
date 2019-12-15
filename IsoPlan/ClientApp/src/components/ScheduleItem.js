import React from 'react';
import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    item: {
        color: theme.palette.primary.contrastText,
        backgroundColor: theme.palette.primary.main,
        fontWeight: 'bold', 
        margin: 'auto',
        marginTop: '2px',
        marginBottom: '2px',
        padding: '4px',
        textAlign: 'center',
        minWidth: '140px',
        maxWidth: '140px',
        overflowWrap: 'break-word',
        borderRadius: 10,
        '&:hover': {
            backgroundColor: "#689F38",
        },
        
    },
    itemNote: {
        borderTop: '1px solid white',
        textAlign: 'left',
        whiteSpace: 'pre-wrap',
        margin: '2px',
        fontWeight: 'normal'
    }
}));

function ScheduleItem(props) {
    const { job, openDialog } = props;
    const classes = useStyles();

    const handleClick = () => {
        openDialog()
    }

    return (
        (job.note === null || job.note.length === 0) ?
            <div className={classes.item} onClick={handleClick}>
                {`${job.jobName}`}
            </div>
            :
            <div>
                <div className={classes.item} onClick={handleClick}>
                    {`${job.jobName}`}
                    <div className={classes.itemNote}>
                        {job.note}
                    </div>
                </div>
            </div>
        )
}

export default ScheduleItem;