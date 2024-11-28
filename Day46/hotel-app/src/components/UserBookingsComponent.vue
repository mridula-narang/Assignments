<template>
  <div>
    <h1>My Bookings</h1>
    <button @click="goBack" class="back-button">Back to Hotels</button>
    <div v-if="bookings.length">
      <div v-for="booking in bookings" :key="booking.bookingId">
        <p>Booking ID: {{ booking.bookingId }}</p>
        <p>Hotel Name: {{ booking.hotelName }}</p>
        <p>Room Name: {{ booking.roomName }}</p>
        <p>Check-In Date: {{ formatDate(booking.checkInDate) }}</p>
        <p>Check-Out Date: {{ formatDate(booking.checkOutDate) }}</p>
        <p>Number of Guests: {{ booking.numberOfGuests }}</p>
        <p>Status: {{ getStatusLabel(booking.status) }}</p>
        <div v-if="booking.status === 0"> <!-- Pending -->
          <button @click="updateBookingStatus(booking.bookingId, 1)">Confirm Booking</button>
          <button @click="updateBookingStatus(booking.bookingId, 2)">Cancel Booking</button>
        </div>
        <div v-else-if="booking.status === 1"> <!-- Booked -->
          <button @click="updateBookingStatus(booking.bookingId, 2)">Cancel Booking</button>
        </div>
      </div>
    </div>
    <div v-else>
      <p>No bookings found.</p>
    </div>
  </div>
</template>

<script>
import { GetBookingById } from "@/scripts/BookingService";
import { GetHotelById } from "@/scripts/HotelService";
import { GetRoomById } from "@/scripts/RoomService";
import axios from "axios"; // Add Axios for API calls

export default {
  name: "UserBookingsComponent",
  data() {
    return {
      bookings: [], // Bookings made by the user
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

        // Fetch hotel names and room names
        const bookingPromises = bookings.map(async (booking) => {
          const hotelResponse = await GetHotelById(booking.hotelId);
          const roomResponse = await GetRoomById(booking.roomId);

          // Update booking with hotelName and roomName
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

      try {
        await axios.put(`http://localhost:5263/api/Booking/${userId}`, null, {
          params: {
            bookingId,
            status: newStatus,
          },
        });
        alert("Booking status updated successfully!");

        // Refresh bookings after the update
        await this.fetchBookings(); // Call the refactored fetch method here
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
