import React from 'react';
import { List } from '@material-ui/core';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import DashboardIcon from '@material-ui/icons/Dashboard';
import ApartmentIcon from '@material-ui/icons/Apartment';
import PeopleIcon from '@material-ui/icons/People';
import LinkListItem from './LinkListItem';

function LinkList() {
    return (
        <List>
            <LinkListItem text="Accueil" icon={<DashboardIcon />} to='/'/>
            <LinkListItem text="Travaux" icon={<ApartmentIcon />} to='/travaux/'/>
            <LinkListItem text="Personnel" icon={<PeopleIcon />} to='/personnel/'/>
            <LinkListItem text="Users" icon={<AccountBoxIcon />} to='/users/'/>
        </List>
    )
}


export default LinkList;