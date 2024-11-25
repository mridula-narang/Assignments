<script>
import { GetHotels } from '@/scripts/HotelService';
import axios from '@/scripts/myAxiosInterceptor';
import HotelComponent from './HotelComponent.vue';
import { MakeBookingForUser } from '@/scripts/BookingService';

export default {
  name: 'HotelsComponent',
  data() {
    return {
      hotels: [],
      selectedHotel: null, // Track the selected hotel
      rooms: [], // Rooms for the selected hotel
      bookings: [], // Bookings made by the user
      bookingDetails: {
        hotelId: null,
        roomId: null,
        checkInDate: '',
        checkOutDate: '',
        numberOfGuests: 1,
        quantity: 1, // Add quantity property
        status: 0
      }
    };
  },
  components: {
    HotelComponent
  },
  created() {
    GetHotels()
      .then(response => {
        this.hotels = response.data;
        console.log(this.hotels);
      })
      .catch(err => {
        console.error('Error fetching hotels:', err);
      });
  },
  methods: {
    viewRooms(hotel) {
      this.selectedHotel = hotel;
      // Fetch rooms for the selected hotel
      axios
        .get(`http://localhost:5263/api/Hotel/${hotel.hotelId}/rooms`)
        .then(response => {
          this.rooms = response.data;
        })
        .catch(err => {
          console.error('Error fetching rooms:', err);
        });
    },
    backToHotels() {
      this.selectedHotel = null; // Reset to show all hotels
      this.rooms = []; // Clear room data
    },
    bookRoom(room) {
      this.bookingDetails.hotelId = this.selectedHotel.hotelId;
      this.bookingDetails.roomId = room.roomId;
      if (this.bookingDetails.checkInDate && this.bookingDetails.checkOutDate && this.bookingDetails.numberOfGuests > 0 && this.bookingDetails.quantity > 0) {
        MakeBookingForUser(this.bookingDetails)
          .then(() => {
            alert('Booking successful!');
          })
          .catch(err => {
            console.error('Error making booking:', err);
          });
      } else {
        alert('Please fill in all booking details.');
      }
    },
    viewUserBookings() {
      this.$router.push('/bookings'); // Navigate to UserBookingsComponent
    }
  }
};
</script>

<template>
  <div>
    <h1>Hotels</h1>

    <!-- Show all hotels if no hotel is selected -->
    <div v-if="!selectedHotel">
      <div v-for="hotel in hotels" :key="hotel.hotelId">
        <HotelComponent :hotel="hotel" />
        <button @click="viewRooms(hotel)">View Rooms</button>
      </div>
      <button @click="viewUserBookings">View My Bookings</button> <!-- Add button to view user bookings -->
    </div>

    <!-- Show selected hotel details and rooms -->
    <div v-else>
      <HotelComponent :hotel="selectedHotel" />
      <h2>Available Rooms</h2>
      <div v-if="rooms.length">
        <div v-for="room in rooms" :key="room.roomId">
          <p>Room Name: {{ room.name }}</p>
          <p>Room Type: {{ room.type === 0 ? 'Single' : room.type === 1 ? 'Double' : 'Suite' }}</p>
          <p>Price: {{ room.price }}</p>
          <p>Availability: {{ room.isBooked === 0 ? 'Available' : 'Not Available' }}</p>
          <button @click="bookRoom(room)">Book Room</button>
        </div>
      </div>
      <div v-else>
        <p>No rooms available.</p>
      </div>
      <button @click="backToHotels">Back to Hotels</button>

      <!-- Booking form -->
      <div>
        <h3>Booking Details</h3>
        <label>Check-In Date:</label>
        <input type="date" v-model="bookingDetails.checkInDate" />
        <label>Check-Out Date:</label>
        <input type="date" v-model="bookingDetails.checkOutDate" />
        <label>Number of Guests:</label>
        <input type="number" v-model="bookingDetails.numberOfGuests" min="1" />
        <label>Quantity:</label> <!-- Add quantity input field -->
        <input type="number" v-model="bookingDetails.quantity" min="1" />
      </div>
    </div>
    <!-- Show user bookings -->
    <div v-if="bookings.length">
      <h2>My Bookings</h2>
      <div v-for="booking in bookings" :key="booking.bookingId">
        <p>Booking ID: {{ booking.bookingId }}</p>
        <p>Hotel ID: {{ booking.hotelId }}</p>
        <p>Room ID: {{ booking.roomId }}</p>
        <p>Check-In Date: {{ booking.checkInDate }}</p>
        <p>Check-Out Date: {{ booking.checkOu }}</p>
      </div>
    </div>
  </div>
</template>