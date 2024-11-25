import axios from './axioInterceptor';

export function AddPolicy(policyName,policyType,userName){
    console.log(policyName,policyType,userName);
    return axios.post('http://localhost:5233/api/Policy/AddPolicy',{
        "policyName": policyName,
        "policyType": policyType,
        "userName": userName
    });
}

export function GetPolicy(){
    return axios.get('http://localhost:5233/api/Policy');
}