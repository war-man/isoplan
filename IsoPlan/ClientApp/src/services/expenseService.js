import { authHeader, handleResponse } from '../helpers/authentication'

export const expenseService = {
    get,
    getAll,
    create,
    update,
    deleteExpense,
    uploadFile,
    deleteFile,
}

function get(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses/${id}`, requestOptions).then(handleResponse)
}

function getAll(params) {
    var paramArray = params === undefined ? [] : params
    var paramString = '?'
    paramArray.forEach(p => paramString = (p.value !== null && p.value !== undefined) ? paramString.concat(`${p.name}=${p.value}&`) : paramString);
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses${paramString}`, requestOptions).then(handleResponse)
}

function create(expense) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(expense)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses`, requestOptions).then(handleResponse)
}

function update(expense) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(expense)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses/${expense.id}`, requestOptions).then(handleResponse)
}

function deleteExpense(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses/${id}`, requestOptions).then(handleResponse)
}

function uploadFile(id, formData) {
    formData.append("id", id);
    const requestOptions =
    {
        method: 'POST',
        headers: authHeader(),
        body: formData
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses/Files`, requestOptions).then(handleResponse)

}

function deleteFile(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Expenses/Files/${id}`, requestOptions).then(handleResponse)
}