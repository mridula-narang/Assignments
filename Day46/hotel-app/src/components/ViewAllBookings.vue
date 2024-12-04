<template>
    <div class="admin-bookings">
      <h1>All Bookings</h1>
      <div v-if="loading">Loading bookings...</div>
      <div v-if="error" class="error">{{ error }}</div>
      <table v-if="bookings.length">
        <thead>
          <tr>
            <th>Booking ID</th>
            <th>User</th>
            <th>Room</th>
            <th>Hotel</th>
            <th>Check-In</th>
            <th>Check-Out</th>
            <th>Guests</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="booking in bookings" :key="booking.bookingId">
            <td>{{ booking.bookingId }}</td>
            <td>{{ booking.userName }}</td>
            <td>{{ booking.roomName }}</td>
            <td>{{ booking.hotelName }}</td>
            <td>{{ booking.checkInDate }}</td>
            <td>{{ booking.checkOutDate }}</td>
            <td>{{ booking.numberOfGuests }}</td>
          </tr>
        </tbody>
      </table>
      <div v-else>No bookings found.</div>
    </div>
  </template>
  
  <script>
  import axios from "@/scripts/myAxiosInterceptor";
  
  export default {
    data() { 
      return {
        name: "ViewAllBookings",
        bookings: [],
        loading: true,
        error: null,
      };
    },
    created() {
      this.fetchBookings();
    },
    methods: {
      fetchBookings() {
        axios
          .get("http://localhost:5263/api/Booking")
          .then((response) => {
            this.bookings = response.data;
            this.loading = false;
          })
          .catch((err) => {
            this.error = "Failed to load bookings.";
            this.loading = false;
            console.error(err);
          });
      },
    },
  };
  </script>
  
  <style scoped>
  .admin-bookings {
    padding: 20px;
  }
  table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
  }
  th, td {
    padding: 10px;
    border: 1px solid #ccc;
  }
  th {
    background-color: #f4f4f4;
  }
  .error {
    color: red;
    margin-top: 10px;
  }
  </style>
  