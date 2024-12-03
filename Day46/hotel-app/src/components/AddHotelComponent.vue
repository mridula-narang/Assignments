<template>
    <div>
      <div>
        <div>
                <label for="name">Hotel Name:</label>
                <input v-model="hotel.name" id="name" type="text" required />
            </div>
            <div>
                <label for="location">Location:</label>
                <input v-model="hotel.location" id="location" type="text" required />
            </div>

        <label for="checkIn">Standard Check-In (ticks):</label>
        <input v-model="checkInTime" id="checkIn" type="time" required />
      </div>
      <div>
        <label for="checkOut">Standard Check-Out (ticks):</label>
        <input v-model="checkOutTime" id="checkOut" type="time" required />
      </div>
      <div>
        <label for="rooms">Number of Rooms:</label>
        <input v-model.number="hotel.numberOfRooms" id="rooms" type="number" required />
      </div>
      <button type="submit" @click="submitForm">Add Hotel</button>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        name:'',
        location:'',
        checkInTime: '',
        checkOutTime: '',
        hotel: {
          numberOfRooms: 0,
          // other hotel properties
        }
      };
    },
    methods: {
      formatTime(time) {
        const [hours, minutes] = time.split(':');
        return `${hours}:${minutes}:00`;
      },
      submitForm() {
        const formattedCheckIn = this.formatTime(this.checkInTime);
        const formattedCheckOut = this.formatTime(this.checkOutTime);
  
        const hotelData = {
          ...this.hotel,
          standardCheckIn: formattedCheckIn,
          standardCheckOut: formattedCheckOut
        };
  
        const AddHotel = 'http://localhost:5263/api/Hotel'; // Ensure this URL is correct
  
        console.log('Sending request to:', AddHotel);
  
        // Send hotelData to the backend API
        axios.post(AddHotel, hotelData)
          .then(response => {
            console.log('Hotel added successfully', response);
          })
          .catch(error => {
            console.error('Error adding hotel', error);
          });
      }
    }
  };
  </script>