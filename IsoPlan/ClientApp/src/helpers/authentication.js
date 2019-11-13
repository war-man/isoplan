export function login(user){
    localStorage.setItem('user', JSON.stringify(user))
}

export function logout() {
    localStorage.removeItem('user');
}

export function getCurrentUser() {
    return JSON.parse(localStorage.getItem('user'));
}

export function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if (response.status === 401) {
                logout()
                window.location.reload()
            }
            const error = (data && data.error) || response.statusText;
            return Promise.reject(error);
        }

        return data;
    });
}

export function authHeader() {
    // return authorization header with jwt token
    const user = getCurrentUser()
    if (user && user.token) {
        return { Authorization: `Bearer ${user.token}` };
    } else {
        return {};
    }
}