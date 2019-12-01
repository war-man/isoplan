import React from 'react';
import { List } from '@material-ui/core';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import DashboardIcon from '@material-ui/icons/Dashboard';
import ApartmentIcon from '@material-ui/icons/Apartment';
import PeopleIcon from '@material-ui/icons/People';
import TodayIcon from '@material-ui/icons/Today';
import LinkListItem from './LinkListItem';

function LinkList(props) {

    const { user } = props;
    
    return (
        <List>
            {
                (user.role === "Admin" || user.role === "Manager") && <LinkListItem text="Accueil" icon={<DashboardIcon />} to='/'/>
            }
            {
                (user.role === "Admin" || user.role === "Manager") && <LinkListItem text="Planning" icon={<TodayIcon />} to='/planning'/>
            }
            {
                (user.role === "Admin" || user.role === "Manager") && <LinkListItem text="Travaux" icon={<ApartmentIcon />} to='/travaux/'/>
            }
            {
                (user.role === "Admin" || user.role === "Manager") && <LinkListItem text="Personnel" icon={<PeopleIcon />} to='/personnel/'/>
            }
            {
                user.role === "Admin" && <LinkListItem text="Usagers" icon={<AccountBoxIcon />} to='/usagers/'/>
            }           
        </List>
    )
}


export default LinkList;