import axios from "axios";

function requestInterceptor(config) {
    const token = sessionStorage.getItem('token');
    if (token) {
        config.headers['Authorization'] = 'Bearer ' + token;
    }
    console.log('Request config:', config);
    return config;
}

function errorInterceptor(error) {
    console.error('Interceptor error:', error);
    return Promise.reject(error);
}

axios.interceptors.request.use(requestInterceptor, errorInterceptor);

export default axios;