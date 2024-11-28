import axios from './myAxiosInterceptor';

export function RegisterUser(user) {
    return axios.post('http://localhost:5263/api/User/Register', user);
}