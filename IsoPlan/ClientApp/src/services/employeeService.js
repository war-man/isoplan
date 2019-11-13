import { authHeader, handleResponse } from '../helpers/authentication'

export const employeeService = {
    getAll,
    create,
    update,
    deleteEmployee
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
        body: JSON.stringify({employee})
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