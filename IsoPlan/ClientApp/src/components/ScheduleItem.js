import React from 'react';
import { makeStyles, Zoom, Tooltip } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    item: {
        color: theme.palette.primary.contrastText,
        backgroundColor: theme.palette.primary.main,
        margin: '1px',
        padding: '4px',
        textAlign: 'center',
        minWidth: '140px',
        maxWidth: '140px',
        overflowWrap: 'break-word',
        borderRadius: 10,
        '&:hover': {
            backgroundColor: "#ff9800",
        }
    }

}));

function ScheduleItem(props) {
    const { text, note } = props;
    const classes = useStyles();

    const handleClick = () => {
        alert("REEE")
    }

    return (
        note !== undefined ?
            <Tooltip title={note} TransitionComponent={Zoom}>
                <div className={classes.item} onClick={handleClick}>
                    {text}
                </div>
            </Tooltip>
            :
            <div className={classes.item} onClick={handleClick}>
                {text}
            </div>
    )
}

export default ScheduleItem;