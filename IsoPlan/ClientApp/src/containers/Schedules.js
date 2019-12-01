import React, { useState } from 'react'
import Dashboard from '../components/Dashboard';
import MaterialTable from 'material-table';
import ScheduleItem from '../components/ScheduleItem';
import ScheduleAddDialog from '../components/ScheduleAddDialog';

function Schedules() {
    const headerStyle = {
        textAlign: 'center',
        minWidth: '140px',
        maxWidth: '140px',
    }

    const [titles, setTitles] = useState(
        ["02.12.", "03.12.", "04.12.", "05.12.", "06.12.", "07.12."]
    )

    const renderItems = (rowData, attr) => {
        return rowData[attr].map((item, i) =>
            <ScheduleItem text={item} note={'THIS IS A CUSTOM NOTE'} key={i} />
        )
    }

    const columns = [
        { 
            title: 'Personne', field: 'name', headerStyle: headerStyle 
        },
        {
            title: `Lundi ${titles[0]}`, field: 'date1', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date1')
        },
        {
            title: `Mardi ${titles[1]}`, field: 'date2', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date2')            
        },
        {
            title: `Mercredi ${titles[2]}`, field: 'date3', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date3')
        },
        {
            title: `Jeudi ${titles[3]}`, field: 'date4', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date4')
        },
        {
            title: `Vendredi ${titles[4]}`, field: 'date5', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date5')
        },
        {
            title: `Samedi ${titles[5]}`, field: 'date6', headerStyle: headerStyle, render: rowData => renderItems(rowData, 'date6')
        },
    ];
    const options = {
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: false,
        search: false,
        sorting: false,
    }

    const actions = [
        {
            icon: 'add_box',
            tooltip: 'Ajouter',
            isFreeAction: true,
            onClick: (event) => {
                setOpenAdd(true);
            }
        }
    ]

    const data = [
        { name: 'MAMADU', 'date1': ['NANTERRE', 'PARIS 17'], 'date2': ['NANTERRE'], 'date3': ['NANTERRE'], 'date4': ['NANTERRE'], 'date5': ['NANTERRE'], 'date6': [] },
        { name: 'AMADU', 'date1': ['PARIS 17'], 'date2': [], 'date3': ['NANTERRE'], 'date4': [], 'date5': [], 'date6': [] },
        { name: 'BUBACAR', 'date1': [], 'date2': [], 'date3': [], 'date4': [], 'date5': [], 'date6': [] },
        { name: 'DUSAN', 'date1': ['POASSY'], 'date2': [], 'date3': [], 'date4': [], 'date5': [], 'date6': ['PARIS 13'] },
        { name: 'VICTOR', 'date1': [], 'date2': [], 'date3': [], 'date4': [], 'date5': [], 'date6': [] },
    ]

    const [openAdd, setOpenAdd] = useState(false)
    const handleCloseAdd = () => {
        setOpenAdd(false);
    }

    return (
        <Dashboard>
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title="Planning"
            />
            <ScheduleAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                handleAdd={() => alert("ADD")}
                jobs={[{id:1, name: 'mile'}]}
                employees={[{id:1, name: 'MAMADU'}, {id:2, name: 'AMADU'}, {id:3, name: 'BUBACAR'}]}
            />
        </Dashboard>
    )
}

export default Schedules;