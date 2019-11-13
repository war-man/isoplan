import { authHeader, handleResponse } from '../helpers/authentication'

export const userService = {
    getAll,
    create,
    update,
    deleteUser
}

function getAll() {
    const requestOptions =
    {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Users`, requestOptions).then(handleResponse)        
}

function create(user) {
    const requestOptions =
    {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Users/register`, requestOptions).then(handleResponse)        
}

function update(user) {
    const requestOptions =
    {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(user)
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Users/${user.id}`, requestOptions).then(handleResponse)        
}

function deleteUser(id) {
    const requestOptions =
    {
        method: 'DELETE',
        headers: {
            ...authHeader(),            
        },
    };
    return fetch(`${process.env.REACT_APP_API_URL}api/Users/${id}`, requestOptions).then(handleResponse)        
}

