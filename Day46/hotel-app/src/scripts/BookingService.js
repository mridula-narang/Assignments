import axios from './myAxiosInterceptor';

// This function will be used to get all bookings from the server -- access granted only to admin
export function GetAllBookings(){
    return axios.get('http://localhost:5263/api/Booking');
}

export function GetBookingById(userId){
    return axios.get(`http://localhost:5263/api/Booking/${userId}`);
}

// Allow user to make a booking
export function MakeBooking(booking){
    return axios.post('http://localhost:5263/api/Booking', booking);
}

// Allow user to make a booking with userId from session
export function MakeBookingForUser(booking) {
    const userId = sessionStorage.getItem('userId');
    if (userId) {
        booking.userId = userId;
        return axios.post('http://localhost:5263/api/Booking', booking);
    } else {
        return Promise.reject('User not logged in');
    }
}