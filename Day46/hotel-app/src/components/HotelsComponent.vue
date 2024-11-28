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
    },
    logout() {
      sessionStorage.clear(); // Clear session storage
      this.$router.push('/'); // Redirect to HelloWorld component
    }
  }
};
</script>

<template>
  <div class="hotels-page">
    <h1 class="page-title">Explore Our Hotels</h1>
    <p class="page-subtitle">Find the perfect place to stay from our curated selection of luxurious hotels.</p>

    <!-- Show all hotels if no hotel is selected -->
    <div v-if="!selectedHotel" class="hotels-list">
      <div class="hotel-card" v-for="hotel in hotels" :key="hotel.hotelId">
        <div class="hotel-image-container">
          <img :src="require('@/assets/' + hotel.hotelId + '.jpeg')" alt="Hotel Image" class="hotel-image" />
          alt="Hotel Image" class="hotel-image" />
        </div>
        <h2 class="hotel-name">{{ hotel.name }}</h2>
        <p class="hotel-location">{{ hotel.location }}</p>
        <!-- <p class="hotel-price">Starting at ${{ hotel.pricePerNight }} per night</p> -->
        <button class="btn-view-rooms" @click="viewRooms(hotel)">View Rooms</button>
      </div>
    </div>

    <!-- Show selected hotel details and rooms -->
    <div v-else class="selected-hotel">
      <HotelComponent :hotel="selectedHotel" />
      <h2 class="rooms-title">Available Rooms</h2>
      <div class="rooms-list">
        <div class="room-card" v-for="room in rooms" :key="room.roomId">
          <p class="room-name">{{ room.name }}</p>
          <p class="room-type">{{ room.type === 0 ? 'Single' : room.type === 1 ? 'Double' : 'Suite' }}</p>
          <p class="room-price">${{ room.price }} per night</p>
          <p class="room-status">{{ room.isBooked === 0 ? 'Available' : 'Not Available' }}</p>
          <button class="btn-book-room" @click="bookRoom(room)">Book Room</button>
        </div>
      </div>
      <button class="btn-back-to-hotels" @click="backToHotels">Back to Hotels</button>
    </div>

    <!-- Logout Button -->
    <button class="btn-logout" @click="logout">Logout</button>
  </div>
</template>

<style scoped>
/* General Page Styles */
.hotels-page {
  font-family: 'Arial', sans-serif;
  text-align: center;
  background: linear-gradient(to bottom, #f5f5f5, #e3e3e3);
  padding: 20px;
}

/* Page Titles */
.page-title {
  font-size: 2.5rem;
  font-weight: bold;
  color: #2c3e50;
  margin-bottom: 10px;
}

.page-subtitle {
  font-size: 1.2rem;
  color: #7f8c8d;
  margin-bottom: 30px;
}

/* Hotels List */
.hotels-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 20px;
  padding: 0 20px;
}

/* Hotel Cards */
.hotel-card {
  background: #ffffff;
  border-radius: 10px;
  overflow: hidden;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s, box-shadow 0.3s;
}

.hotel-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
}

.hotel-image-container {
  height: 200px;
  overflow: hidden;
}

.hotel-image {
  width: 100%;
  height: 100%;
  object-fit: contain;
}

.hotel-name {
  font-size: 1.5rem;
  font-weight: bold;
  color: #34495e;
  margin: 15px;
}

.hotel-location {
  font-size: 1rem;
  color: #7f8c8d;
  margin: 0 15px;
}

.hotel-price {
  font-size: 1.1rem;
  font-weight: bold;
  color: #27ae60;
  margin: 10px 15px 15px;
}

.btn-view-rooms {
  background-color: #3498db;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  margin-bottom: 15px;
  transition: background-color 0.3s;
}

.btn-view-rooms:hover {
  background-color: #2980b9;
}

/* Selected Hotel and Rooms */
.selected-hotel {
  padding: 20px;
  background: #ffffff;
  border-radius: 10px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.rooms-title {
  font-size: 1.8rem;
  color: #2c3e50;
  margin: 20px 0;
}

.rooms-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 20px;
}

.room-card {
  background: #ffffff;
  border-radius: 10px;
  padding: 15px;
  text-align: left;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s, box-shadow 0.3s;
}

.room-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.2);
}

.room-name {
  font-size: 1.2rem;
  font-weight: bold;
  color: #34495e;
  margin-bottom: 5px;
}

.room-type,
.room-price,
.room-status {
  font-size: 1rem;
  color: #7f8c8d;
  margin: 5px 0;
}

.btn-book-room {
  background-color: #27ae60;
  color: white;
  padding: 8px 12px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  margin-top: 10px;
  transition: background-color 0.3s;
}

.btn-book-room:hover {
  background-color: #1e8449;
}

/* Navigation Buttons */
.btn-back-to-hotels,
.btn-logout {
  background-color: #c0392b;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-weight: bold;
  margin: 20px 0;
  transition: background-color 0.3s;
}

.btn-back-to-hotels:hover,
.btn-logout:hover {
  background-color: #a93226;
}
</style>
