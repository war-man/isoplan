import { authHeader, handleResponse } from '../helpers/authentication'

export const employeeService = {
    get,
    getAll,
    create,
    update,
    deleteEmployee,
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
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/${id}`, requestOptions).then(handleResponse)     
}

function getAll() {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees`, requestOptions).then(handleResponse)     
}

function create(employee) {
    employee.salary = parseFloat(employee.salary);
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(employee)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees`, requestOptions).then(handleResponse)        
}

function update(employee) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(employee)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/${employee.id}`, requestOptions).then(handleResponse)        
}

function deleteEmployee(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),            
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/${id}`, requestOptions).then(handleResponse)        
}

function getFiles(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/${id}/files`, requestOptions).then(handleResponse)     
}

function uploadFile(id, formData) {
    formData.append("employeeId", id);
    const requestOptions =
    {
        method: 'POST',
        headers: authHeader(),
        body: formData
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/files`, requestOptions).then(handleResponse)     
}

function deleteFile(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Employees/files/${id}`, requestOptions).then(handleResponse)     
}