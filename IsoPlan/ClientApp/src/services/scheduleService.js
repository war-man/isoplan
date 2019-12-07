import { authHeader, handleResponse } from '../helpers/authentication'

export const scheduleService = {
    getAll,
    create,
    update,
    deleteSchedule
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
