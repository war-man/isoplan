import { authHeader, handleResponse } from '../helpers/authentication'

export const jobService = {
    get,
    getAll,
    create,
    update,
    deleteJob,
}

function get(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/${id}`, requestOptions).then(handleResponse)     
}

function getAll() {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs`, requestOptions).then(handleResponse)     
}

function create(job) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(job)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs`, requestOptions).then(handleResponse)        
}

function update(job) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(job)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/${job.id}`, requestOptions).then(handleResponse)        
}

function deleteJob(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),            
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/${id}`, requestOptions).then(handleResponse)        
}
