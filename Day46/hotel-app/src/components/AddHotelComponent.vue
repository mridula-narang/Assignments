<template>
  <div class="hotel-form-container">
    <div class="form-header">
      <img src="@/assets/logo.jpg" alt="Hotel Logo" class="logo" />
      <h1>Welcome to Tiare Hotels</h1>
    </div>
    <form class="hotel-form" @submit.prevent="submitForm">
      <div class="form-group">
        <label for="name">Hotel Name:</label>
        <input v-model="hotel.name" id="name" type="text" required />
      </div>
      <div class="form-group">
        <label for="location">Location:</label>
        <input v-model="hotel.location" id="location" type="text" required />
      </div>
      <div class="form-group">
        <label for="checkIn">Standard Check-In:</label>
        <input v-model="checkInTime" id="checkIn" type="time" required />
      </div>
      <div class="form-group">
        <label for="checkOut">Standard Check-Out:</label>
        <input v-model="checkOutTime" id="checkOut" type="time" required />
      </div>
      <div class="form-group">
        <label for="rooms">Number of Rooms:</label>
        <input v-model.number="hotel.numberOfRooms" id="rooms" type="number" required />
      </div>
      <button type="submit" class="submit-button">Add Hotel</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      hotel: {
        name: '',
        location: '',
        numberOfRooms: 0,
      },
      checkInTime: '',
      checkOutTime: '',
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
        standardCheckOut: formattedCheckOut,
      };

      const AddHotel = 'http://localhost:5263/api/Hotel'; 

      axios
        .post(AddHotel, hotelData)
        .then((response) => {
          alert('Hotel added successfully');
          this.$router.push('/hotels');
          console.log('Hotel added successfully', response);
        })
        .catch((error) => {
          console.error('Error adding hotel', error);
        });
    },
  },
};
</script>

<style scoped>
.hotel-form-container {
  max-width: 500px;
  margin: 0 auto;
  padding: 20px;
  background-color: #f7f0e9; 
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.form-header {
  text-align: center;
  margin-bottom: 20px;
}

.logo {
  max-width: 100px;
  margin-bottom: 10px;
}

h1 {
  font-size: 1.5rem;
  color: #4a4a4a; 
}

.hotel-form {
  display: flex;
  flex-direction: column;
}

.form-group {
  margin-bottom: 15px;
}

label {
  font-weight: bold;
  color: #4a4a4a;
  margin-bottom: 5px;
  display: block;
}

input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
  box-sizing: border-box;
}

input:focus {
  border-color: #77c7c7; 
  outline: none;
}

.submit-button {
  padding: 10px 15px;
  background-color: #77c7c7; 
  color: white;
  border: none;
  border-radius: 4px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s;
}

.submit-button:hover {
  background-color: #5aa7a7;
}
</style>
