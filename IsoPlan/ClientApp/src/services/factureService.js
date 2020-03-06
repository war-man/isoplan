import { authHeader, handleResponse } from '../helpers/authentication'

export const factureService = {
    get,
    getAll,
    create,
    update,
    deleteFacture,
    uploadFile,
    deleteFile,
}

function get(id) {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures/${id}`, requestOptions).then(handleResponse)
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
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures${paramString}`, requestOptions).then(handleResponse)
}

function create(facture) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(facture)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures`, requestOptions).then(handleResponse)
}

function update(facture) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(facture)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures/${facture.id}`, requestOptions).then(handleResponse)
}

function deleteFacture(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures/${id}`, requestOptions).then(handleResponse)
}

function uploadFile(id, formData) {
    formData.append("id", id);
    const requestOptions =
    {
        method: 'POST',
        headers: authHeader(),
        body: formData
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures/Files`, requestOptions).then(handleResponse)

}

function deleteFile(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Factures/Files/${id}`, requestOptions).then(handleResponse)
}