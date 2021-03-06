import React, { useState, useEffect } from 'react'
import Dashboard from '../components/Dashboard';
import { Grid, Typography, makeStyles, TextField } from '@material-ui/core';
import { scheduleService } from '../services/scheduleService';
import moment from 'moment';
import MaterialTable from 'material-table';
import { Localization } from '../helpers/localization';
import { DatePicker, MuiPickersUtilsProvider } from "@material-ui/pickers";
import DateFnsUtils from '@date-io/date-fns';
import { fr } from 'date-fns/locale';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { jobService } from '../services/jobService';
import { employeeService } from '../services/employeeService';

const useStyles = makeStyles(theme => ({
    toolbarTop: {
        display: 'flex',
        alignItems: 'center',
    },
    datePicker: {
        width: '150px',
        marginLeft: '24px'
    },
    autocomplete: {
        width: '240px',
    },
    bottomMargin: {
        marginBottom: theme.spacing(3)
    }
}));


function Analysis() {
    const classes = useStyles();

    const options = {
        exportButton: true,
        exportFileName: 'analyse',
        draggable: false,
        actionsColumnIndex: -1,
        pageSizeOptions: [],
        paging: false,
        search: false,
    }

    const [totalLoading, setTotalLoading] = useState(false);
    const [byEmployeeLoading, setbyEmployeeLoading] = useState(false);
    const [byJobLoading, setByJobLoading] = useState(false);


    const totalColumns = [
        { title: 'Prénom', field: 'employee.firstName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Nom', field: 'employee.lastName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Jours travaillés', field: 'totalDays', type: 'numeric' },
        { title: 'Salaire', field: 'salary', type: 'currency', currencySetting: { currencyCode: 'EUR', locale: 'fr-FR' } }
    ]

    const byEmployeeColumns = [
        { title: 'Client', field: 'job.clientName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Affaire', field: 'job.name', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Jours travaillés', field: 'totalDays', type: 'numeric' },
    ]

    const byJobColumns = [
        { title: 'Prénom', field: 'employee.firstName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Nom', field: 'employee.lastName', cellStyle: { maxWidth: '150px', overflowWrap: 'break-word' } },
        { title: 'Jours travaillés', field: 'totalDays', type: 'numeric' },
    ]

    const [date, setDate] = useState(new Date())
    const dateString = moment(date).format('YYYY-MM-01')
    const dateDisplayString = moment(date).format('MMMM YYYY')
    const handleDateChange = (date) => {
        if (!moment(date).isValid()) {
            setDate(new Date());
        } else {
            setDate(date);
        }
        setEmployee({
            id: 0,
            firstName: '',
            lastName: ''
        });
        setJob({
            id: 0,
            name: '',
            clientName: ''
        });
    }

    const [totalData, setTotalData] = useState([])
    const getTotal = (dateString) => {
        setTotalLoading(true);
        scheduleService.getTotal(dateString)
            .then(res => {
                setTotalData(res);
                setTotalLoading(false);
            })
    }

    const [employee, setEmployee] = useState({
        id: 0,
        firstName: '',
        lastName: ''
    })
    const handleEmployeeChange = (event, newEmployee) => {
        if (newEmployee === null) {
            setEmployee({
                id: 0,
                firstName: '',
                lastName: ''
            });
        } else {
            setEmployee(newEmployee);
        }
    }
    const [byEmployee, setByEmployee] = useState([])
    const getByEmployee = (id, dateString) => {
        if (id === 0 || id === undefined) {
            setByEmployee([]);
            return;
        }
        setbyEmployeeLoading(true);
        scheduleService.getJobsPerEmployee(id, dateString)
            .then(res => {
                setByEmployee(res);
                setbyEmployeeLoading(false);
            })
    }


    const [job, setJob] = useState({
        id: 0,
        name: '',
        clientName: ''
    })
    const handleJobChange = (event, newJob) => {
        if (newJob === null) {
            setJob({
                id: 0,
                name: '',
                clientName: ''
            });
        } else {
            setJob(newJob);
        }
    }
    const [byJob, setByJob] = useState([])
    const getByJob = (id, dateString) => {
        if (id === 0 || id === undefined) {
            setByJob([]);
            return;
        }
        setByJobLoading(true);
        scheduleService.getEmployeesPerJob(id, dateString)
            .then(res => {
                setByJob(res);
                setByJobLoading(false);
            })
    }

    const [employees, setEmployees] = useState([])
    const getEmployees = (params) => {
        employeeService.getBySchedules(params)
            .then(res => setEmployees(res))
    }

    const [jobs, setJobs] = useState([])
    const getJobs = (params) => {
        jobService.getBySchedules(params)
            .then(res => setJobs(res))
    }

    useEffect(() => {
        const params = [
            {
                name: 'startDate',
                value: dateString
            }
        ]
        getTotal(dateString);
        getEmployees(params);
        getJobs(params);
    }, [dateString])

    useEffect(() => {
        getByEmployee(employee.id, dateString);
    }, [dateString, employee.id])

    useEffect(() => {
        getByJob(job.id, dateString);
    }, [dateString, job.id])

    return (
        <Dashboard title={'Analyse'}>
            <Grid container spacing={2} alignItems="flex-start">
                <Grid item xs={12} md={6}>
                    <MaterialTable
                        columns={totalColumns}
                        data={totalData}
                        options={options}
                        localization={Localization}
                        onRowClick={(event, rowData) => setEmployee({ ...rowData.employee })}
                        isLoading={totalLoading}
                        title={
                            <div className={classes.toolbarTop}>
                                <Typography variant="h6">
                                    {`Total ${dateDisplayString}`}
                                </Typography>
                                <MuiPickersUtilsProvider utils={DateFnsUtils} locale={fr}>
                                    <DatePicker
                                        cancelLabel="Annuler"
                                        views={["month", "year"]}
                                        label="Changer la date:"
                                        className={classes.datePicker}
                                        value={date}
                                        onChange={(date) => handleDateChange(date)}
                                    />
                                </MuiPickersUtilsProvider>
                            </div>
                        }
                    />
                </Grid>
                <Grid item xs={12} md={6}>
                    <div className={classes.bottomMargin}>
                        <MaterialTable
                            columns={byEmployeeColumns}
                            data={byEmployee}
                            options={options}
                            localization={Localization}
                            onRowClick={(event, rowData) => setJob({ ...rowData.job })}
                            isLoading={byEmployeeLoading}
                            title={
                                <div className={classes.toolbarTop}>                                    
                                    <Autocomplete
                                        className={classes.autocomplete}
                                        options={employees}
                                        getOptionLabel={employee => employee.id === 0 ? '' : `${employee.firstName} ${employee.lastName}`}
                                        onChange={handleEmployeeChange}
                                        value={employee}
                                        renderInput={params => (
                                            <TextField
                                                {...params}
                                                variant="standard"
                                                label={`Employé total ${dateDisplayString}`}
                                                margin="dense"
                                                fullWidth
                                            />
                                        )}
                                    />
                                </div>
                            }
                        />
                    </div>
                    <MaterialTable
                        columns={byJobColumns}
                        data={byJob}
                        options={options}
                        localization={Localization}
                        isLoading={byJobLoading}
                        title={
                            <div className={classes.toolbarTop}>
                                <Autocomplete
                                    className={classes.autocomplete}
                                    options={jobs}
                                    getOptionLabel={job => job.clientName === '' ? '' : `${job.name} (${job.clientName})`}
                                    onChange={handleJobChange}
                                    value={job}
                                    renderInput={params => (
                                        <TextField
                                            {...params}
                                            variant="standard"
                                            label={`Travail total ${dateDisplayString}`}
                                            margin="dense"
                                            fullWidth
                                        />
                                    )}
                                />
                            </div>
                        }
                    />
                </Grid>
            </Grid>

        </Dashboard>
    )
}

export default Analysis;