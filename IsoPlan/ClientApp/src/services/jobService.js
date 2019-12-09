import { authHeader, handleResponse } from '../helpers/authentication'

export const jobService = {
    get,
    getAll,
    getBySchedules,
    create,
    update,
    deleteJob,
    createItem,
    updateItem,
    deleteItem,
    getFiles,
    uploadFile,
    deleteFile
}

function get(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/${id}`, requestOptions).then(handleResponse)     
}

function getAll(params) {
    var paramArray = params === undefined ? [] : params
    var paramString = '?'
    paramArray.forEach(p => paramString = (p.value !== null && p.value !== undefined) ? paramString.concat(`${p.name}=${p.value}&`) : paramString );
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs${paramString}`, requestOptions).then(handleResponse)     
}

function getBySchedules(params) {
    var paramArray = params === undefined ? [] : params
    var paramString = '?'
    paramArray.forEach(p => paramString = (p.value !== null && p.value !== undefined) ? paramString.concat(`${p.name}=${p.value}&`) : paramString );
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/bySchedules${paramString}`, requestOptions).then(handleResponse)     
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

function createItem(item) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(item)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/Items`, requestOptions).then(handleResponse)        
}

function updateItem(item) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(item)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/Items/${item.id}`, requestOptions).then(handleResponse)        
}

function deleteItem(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),            
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/Items/${id}`, requestOptions).then(handleResponse)        
}

function getFiles(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/${id}/Files`, requestOptions).then(handleResponse)     
}

function uploadFile(id, formData) {
    formData.append("jobId", id);
    const requestOptions =
    {
        method: 'POST',
        headers: authHeader(),
        body: formData
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/Files`, requestOptions).then(handleResponse)     
}

function deleteFile(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Jobs/Files/${id}`, requestOptions).then(handleResponse)     
}
