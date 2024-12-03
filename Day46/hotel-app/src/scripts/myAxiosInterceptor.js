import axios from 'axios';

function requestInterceptor(config){
    const token = sessionStorage.getItem('Token');
    if(token){
        config.headers['Authorization'] = 'Bearer ' + token;
    }
    return config;
}

axios.interceptors.request.use(requestInterceptor);

export default axios;