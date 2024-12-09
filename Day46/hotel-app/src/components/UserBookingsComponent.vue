<script>
import { GetBookingById } from "@/scripts/BookingService";
import { GetHotelById } from "@/scripts/HotelService";
import { GetRoomById } from "@/scripts/RoomService";
import axios from "axios"; 

export default {
  name: "UserBookingsComponent",
  data() {
    return {
      bookings: [], 
    };
  },
  async created() {
    await this.fetchBookings();
  },
  methods: {
    async fetchBookings() {
      const userId = sessionStorage.getItem("userId");
      if (!userId) {
        alert("User not logged in");
        return;
      }

      try {
        const response = await GetBookingById(userId);
        const bookings = response.data.sort((a, b) => b.bookingId - a.bookingId);

        const bookingPromises = bookings.map(async (booking) => {
          const hotelResponse = await GetHotelById(booking.hotelId);
          const roomResponse = await GetRoomById(booking.roomId);

          return {
            ...booking,
            hotelName: hotelResponse.data.name,
            roomName: roomResponse.data.name,
          };
        });

        this.bookings = await Promise.all(bookingPromises);
      } catch (err) {
        console.error("Error fetching user bookings:", err);
      }
    },
    async updateBookingStatus(bookingId, newStatus) {
      const userId = sessionStorage.getItem("userId");
      if (!userId) {
        alert("User not logged in");
        return;
      }

      if (newStatus === 1) { 
        this.$router.push({
          path: "/payment/:bookingid", 
          query: { bookingId }, 
        });
        return;
      }

      try {
        await axios.put(`http://localhost:5263/api/Booking/${userId}`, null, {
          params: {
            bookingId,
            status: newStatus,
          },
        });
        alert("Booking status updated successfully!");

        await this.fetchBookings();
      } catch (err) {
        console.error("Error updating booking status:", err);
        alert("Failed to update booking status.");
      }
    },
    formatDate(date) {
      const d = new Date(date);
      return d.getHours() === 0 && d.getMinutes() === 0 && d.getSeconds() === 0
        ? d.toISOString().split("T")[0]
        : d.toISOString().replace("T", " ").split(".")[0];
    },
    getStatusLabel(status) {
      switch (status) {
        case 0:
          return "Pending";
        case 1:
          return "Booked";
        case 2:
          return "Cancelled";
        default:
          return "Unknown";
      }
    },
    goBack() {
      this.$router.push("/hotels");
    },
  },
};
</script>


<template>
  <div class="bookings-page">
    <header class="header">
      <img src="../assets/logo.jpg" alt="Tiare Logo" class="logo" />
      <h1>My Bookings</h1>
    </header>
    <button @click="goBack" class="back-button">Back to Hotels</button>
    <div v-if="bookings.length" class="bookings-container">
      <div v-for="booking in bookings" :key="booking.bookingId" class="booking-card">
        <div class="booking-details">
          <p><strong>Booking ID:</strong> {{ booking.bookingId }}</p>
          <p><strong>Hotel Name:</strong> {{ booking.hotelName }}</p>
          <p><strong>Room Name:</strong> {{ booking.roomName }}</p>
          <p><strong>Check-In Date:</strong> {{ formatDate(booking.checkInDate) }}</p>
          <p><strong>Check-Out Date:</strong> {{ formatDate(booking.checkOutDate) }}</p>
          <p><strong>Number of Guests:</strong> {{ booking.numberOfGuests }}</p>
          <p><strong>Status:</strong> <span :class="`status status-${booking.status}`">{{ getStatusLabel(booking.status)
              }}</span></p>
          <p><strong>Quantity</strong> {{ booking.quantity }}</p>
          <p><strong>Total Price</strong> {{ booking.totalPrice }}</p>
        </div>
        <div class="booking-actions">
          <button v-if="booking.status === 0" @click="updateBookingStatus(booking.bookingId, 1)"
            class="action-button confirm-button">Confirm Booking</button>
          <button v-if="booking.status === 0 || booking.status === 1" @click="updateBookingStatus(booking.bookingId, 2)"
            class="action-button cancel-button">Cancel Booking</button>
        </div>
      </div>
    </div>
    <div v-else class="no-bookings">
      <p>No bookings found.</p>
    </div>
  </div>
</template>

<style scoped>
/* General Styling */
body {
  font-family: 'Arial', sans-serif;
  margin: 0;
  padding: 0;
  background-color: #f9f9f9;
  color: #333;
}

.bookings-page {
  padding: 20px;
  background-image: url('../assets/booking-bg.jpeg');

  background-size: cover;

  background-position: center;

  background-attachment: fixed;

  color: #333;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
}

/* Header Styling */
.header {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 20px;
}

.logo {
  width: 50px;
  height: 50px;
  border-radius: 50%;
}

.header h1 {
  font-size: 1.8rem;
  color: #2c3e50;
}

/* Button Styling */
.back-button {
  display: inline-block;
  margin-bottom: 20px;
  padding: 10px 20px;
  font-size: 1rem;
  color: #fff;
  background-color: #3498db;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.back-button:hover {
  background-color: #2980b9;
}

/* Booking Cards */
.bookings-container {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
  width: 100%;
  max-width: 1200px;
}

.booking-card {
  background: rgba(255, 255, 255, 0.85);
  border: 1px solid rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  padding: 15px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s, box-shadow 0.2s;
}

.booking-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.booking-details p {
  margin: 8px 0;
}

.booking-details p strong {
  color: #34495e;
}

.booking-actions {
  margin-top: 15px;
  display: flex;
  gap: 10px;
}

/* Buttons */
.action-button {
  padding: 8px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 0.9rem;
  transition: all 0.3s;
}

.confirm-button {
  background-color: #2ecc71;
  color: #fff;
}

.confirm-button:hover {
  background-color: #27ae60;
}

.cancel-button {
  background-color: #e74c3c;
  color: #fff;
}

.cancel-button:hover {
  background-color: #c0392b;
}

/* Status Styling */
.status {
  font-weight: bold;
  padding: 5px 10px;
  border-radius: 15px;
}

.status-0 {
  color: #f39c12;
  background: #fdf2e9;
}

.status-1 {
  color: #27ae60;
  background: #eafaf1;
}

.status-2 {
  color: #e74c3c;
  background: #fdecea;
}

/* No Bookings Styling */
.no-bookings {
  text-align: center;
  font-size: 1.2rem;
  color: #7f8c8d;
}
</style>
