import axios from './myAxiosInterceptor';

// This function will be used to get all hotels from the server
export function GetHotels() {
    return axios.get('http://localhost:5263/api/Hotel');
}

// Add the GetHotelById function
export function GetHotelById(hotelId) {
    return axios.get(`http://localhost:5263/api/Hotel/${hotelId}/GetHotelById`);
}