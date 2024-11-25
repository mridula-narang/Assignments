// import axios from 'axios';
import axios from './axioInterceptor';

export function login(username, password) {
  return axios.post('http://localhost:5233/api/User/Login', {
    "username": username,
    "password": password
  });
}