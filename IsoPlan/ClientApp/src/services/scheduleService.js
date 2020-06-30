import { authHeader, handleResponse } from '../helpers/authentication'

export const scheduleService = {
    getAll,
    create,
    update,
    deleteSchedule,
    getTotal,
    getJobsPerEmployee,
    getEmployeesPerJob
}

function getAll(date) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules?date=${date}`, requestOptions).then(handleResponse)
}

function create(schedule) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(schedule)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules`, requestOptions).then(handleResponse)        
}

function update(schedule) {
    schedule.salary = parseFloat(schedule.salary);
    schedule.multiplier = parseFloat(schedule.multiplier);
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(schedule)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules`, requestOptions).then(handleResponse)        
}

function deleteSchedule(schedule) {
    schedule.salary = parseFloat(schedule.salary);
    schedule.multiplier = parseFloat(schedule.multiplier);
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(schedule)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules`, requestOptions).then(handleResponse)        
}

function getTotal(date) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules/total?date=${date}`, requestOptions).then(handleResponse)
}

function getJobsPerEmployee(id, date) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules/employee/${id}?date=${date}`, requestOptions).then(handleResponse)
}

function getEmployeesPerJob(id, date) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Schedules/job/${id}?date=${date}`, requestOptions).then(handleResponse)
}
