import React, { useState, useEffect } from 'react'
import Dashboard from '../components/Dashboard';
import { useParams } from 'react-router-dom';
import MaterialTable from 'material-table';
import ScheduleItem from '../components/ScheduleItem';
import ScheduleAddDialog from '../components/ScheduleAddDialog';
import { scheduleService } from '../services/scheduleService';
import { employeeService } from '../services/employeeService';
import { jobService } from '../services/jobService';
import { EmployeeStatus } from '../helpers/employeeStatus';
import { JobStatus } from '../helpers/jobStatus';
import ScheduleEditDialog from '../components/ScheduleEditDialog';
import moment from 'moment';
import { makeStyles, Button, Paper } from '@material-ui/core';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import { Link, Redirect } from 'react-router-dom';
import { MuiPickersUtilsProvider, DatePicker } from '@material-ui/pickers';
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale'
import { Localization } from '../helpers/localization';

const useStyles = makeStyles(theme => ({
    button: {
        margin: theme.spacing(1),
        height: '32px',
        '&:hover': {
            color: theme.palette.primary.contrastText,
        },
    },
    toolbarTop: {
        display: 'flex',
        justifyContent: 'space-between',
        margin: `0px 0px ${theme.spacing(1)}px`
    }
}));

const headerStyle = {
    textAlign: 'center',
    minWidth: '120px',
    maxWidth: '120px',
}

function Schedules() {
    const { date } = useParams();
    const classes = useStyles();

    const parsedDate = (date !== undefined && moment(date, "YYYY-MM-DD").isValid()) ? moment(date, "YYYY-MM-DD") : moment(new Date())

    const startOfWeek = parsedDate.startOf('isoWeek')
    const startOfWeekString = startOfWeek.format("YYYY-MM-DD")

    const nextStartOfWeekString = moment(startOfWeek).add(1, 'isoWeek').format("YYYY-MM-DD")
    const prevStartOfWeekString = moment(startOfWeek).subtract(1, 'isoWeek').format("YYYY-MM-DD")

    const startOfWeekDisplay = startOfWeek.format('DD.MM.YYYY')
    const endOfWeekDisplay = moment(startOfWeek).add(5, 'days').format('DD.MM.YYYY')

    const titles = [
        moment(startOfWeek).format("DD.MM."),
        moment(startOfWeek).add(1, 'days').format("DD.MM."),
        moment(startOfWeek).add(2, 'days').format("DD.MM."),
        moment(startOfWeek).add(3, 'days').format("DD.MM."),
        moment(startOfWeek).add(4, 'days').format("DD.MM."),
        moment(startOfWeek).add(5, 'days').format("DD.MM."),
    ]

    const columns = [
        {
            title: 'Nom complet', field: 'name', headerStyle: headerStyle, render: rowData => <Link to={`/personnel/${rowData.id}`}>{rowData.name}</Link>
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

    const renderItems = (rowData, attr) => {
        return rowData[attr].map((item, i) =>
            <ScheduleItem job={item} key={i} openDialog={openDialog(item)} />
        )
    }

    const [openEdit, setOpenEdit] = useState(false)
    const handleCloseEdit = () => {
        setOpenEdit(false)
        setScheduleToEdit({
            date: null,
            employeeId: 0,
            employeeName: '',
            jobId: 0,
            jobName: '',
            note: '',
            salary: 0,
        })
    }
    const openDialog = item => event => {
        setScheduleToEdit({ ...item })
        setOpenEdit(true)
    }

    const [scheduleToEdit, setScheduleToEdit] = useState({
        date: null,
        employeeId: 0,
        employeeName: '',
        jobId: 0,
        jobName: '',
        note: '',
        salary: 0,
    })

    const [data, setData] = useState([])
    const [openAdd, setOpenAdd] = useState(false)
    const handleCloseAdd = () => {
        setOpenAdd(false);
    }

    const [employees, setEmployees] = useState([])
    const [jobs, setJobs] = useState([])

    const [loading, setLoading] = useState(false)

    const getEverything = (date) => {
        setLoading(true);
        scheduleService.getAll(date)
            .then(res => {
                setData(res);
                setLoading(false);
            })
            .catch(err => alert(err))
        employeeService.getAll([{
            name: 'status',
            value: EmployeeStatus.Active
        }])
            .then(res => setEmployees(res))
            .catch(err => alert(err))
        jobService.getAll([{
            name: 'status',
            value: JobStatus.Started
        }])
            .then(res => setJobs(res))
            .catch(err => alert(err))
    }

    const handleCreate = schedule => {
        scheduleService.create(schedule)
            .then(() => getEverything(startOfWeekString))
            .catch((err) => alert(err))
    }

    const handleUpdate = () => {
        scheduleService.update(scheduleToEdit)
            .then(() => getEverything(startOfWeekString))
    }

    const handleDelete = () => {
        scheduleService.deleteSchedule(scheduleToEdit)
            .then(() => getEverything(startOfWeekString))
    }

    const [selectedDate, setSelectedDate] = useState(null)

    const renderRedirect = () => {
        if (selectedDate !== null) {
            return <Redirect push to={`/planning/${moment(selectedDate).format("YYYY-MM-DD")}`} />
        }
    }

    useEffect(() => {
        getEverything(startOfWeekString)
    }, [startOfWeekString])

    return (
        <Dashboard title={'Planning'}>
            {renderRedirect()}
            <Paper className={classes.toolbarTop}>
                <Button
                    variant="contained"
                    color="primary"
                    className={classes.button}
                    startIcon={<ChevronLeftIcon />}
                    component={Link}
                    to={`${prevStartOfWeekString}`}
                >
                    précédente
                </Button>
                <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                    <div>
                        <DatePicker
                            showTodayButton={true}
                            todayLabel="Aujourd'hui"
                            cancelLabel="Annuler"
                            labelFunc={() => `Semaine du ${moment(startOfWeek).format("MMM DD.YYYY")}`}
                            variant="dialog"
                            margin="dense"
                            format="dd.MM.yyyy."
                            value={startOfWeek}
                            onChange={(date) => setSelectedDate(date)}
                        />
                    </div>
                </MuiPickersUtilsProvider>
                <Button
                    variant="contained"
                    color="primary"
                    className={classes.button}
                    endIcon={<ChevronRightIcon />}
                    component={Link}
                    to={`${nextStartOfWeekString}`}
                >
                    prochaine
                </Button>
            </Paper>
            <MaterialTable
                columns={columns}
                data={data}
                options={options}
                actions={actions}
                title={`Planning: ${startOfWeekDisplay} - ${endOfWeekDisplay}`}
                isLoading={loading}
                localization={Localization}
            />
            <ScheduleAddDialog
                open={openAdd}
                handleClose={handleCloseAdd}
                jobs={jobs}
                employees={employees}
                handleCreate={handleCreate}
            />
            <ScheduleEditDialog
                open={openEdit}
                handleClose={handleCloseEdit}
                scheduleToEdit={scheduleToEdit}
                setScheduleToEdit={setScheduleToEdit}
                handleSave={handleUpdate}
                handleDelete={handleDelete}
            />
        </Dashboard>
    )
}

export default Schedules;