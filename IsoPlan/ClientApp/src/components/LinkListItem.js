import React from 'react';
import { ListItem, ListItemText, ListItemIcon, Tooltip } from '@material-ui/core';
import { Link } from 'react-router-dom';
import Zoom from '@material-ui/core/Zoom';

function LinkListItem(props) {
    const {text, icon, to} = props;
    return (
        <ListItem button component={Link} to={to}>
            {!JSON.parse(localStorage.getItem('openDrawer')) ?
                <Tooltip title={text} placement="right" TransitionComponent={Zoom}>
                    <ListItemIcon>
                        {icon}
                    </ListItemIcon>
                </Tooltip>
                :
                <ListItemIcon>
                    {icon}
                </ListItemIcon>
            }
            <ListItemText primary={text} />
        </ListItem>
    )
}

export default LinkListItem;