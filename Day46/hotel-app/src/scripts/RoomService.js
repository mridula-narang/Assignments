import axios from './myAxiosInterceptor';

export function GetRooms(){
    return axios.get('http://localhost:5263/api/Room');
}

export function GetRoomById(roomId) {
    return axios.get(`http://localhost:5263/api/Room/${roomId}/GetRoomById`);
}