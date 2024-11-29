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
      selectedHotel: null,
      rooms: [],
      bookings: [],
      bookingDetails: {
        hotelId: null,
        roomId: null,
        checkInDate: '',
        checkOutDate: '',
        numberOfGuests: 1,
        quantity: 1,
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
      })
      .catch(err => {
        console.error('Error fetching hotels:', err);
      });
  },
  methods: {
    viewRooms(hotel) {
      this.selectedHotel = hotel;
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
      this.selectedHotel = null;
      this.rooms = [];
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
      this.$router.push('/bookings');
    },
    logout() {
      sessionStorage.clear();
      this.$router.push('/');
    }
  }
};
</script>

<template>
  <div class="hotels-page">
    <h1 class="page-title">Explore Our Hotels</h1>
    <p class="page-subtitle">Find the perfect place to stay from our curated selection of luxurious hotels.</p>

    <div v-if="!selectedHotel" class="hotels-list">
      <div class="hotel-card" v-for="hotel in hotels" :key="hotel.hotelId">
        <div class="hotel-image-container">
          <img :src="require('@/assets/' + hotel.hotelId + '.jpeg')" alt="Hotel Image" class="hotel-image" />
        </div>
        <h2 class="hotel-name">{{ hotel.name }}</h2>
        <p class="hotel-location">{{ hotel.location }}</p>
        <button class="btn-view-rooms" @click="viewRooms(hotel)">View Rooms</button>
      </div>
    </div>

    <div v-else class="selected-hotel">
      <HotelComponent :hotel="selectedHotel" />
      <h2 class="rooms-title">Available Rooms</h2>
      <div class="rooms-list">
        <div class="room-card" v-for="room in rooms" :key="room.roomId">
          <p class="room-name">{{ room.name }}</p>
          <p class="room-type">{{ room.type === 0 ? 'Single' : room.type === 1 ? 'Double' : 'Suite' }}</p>
          <p class="room-price">${{ room.price }} per night</p>
          <p class="room-status">{{ room.isBooked === 0 ? 'Available' : 'Not Available' }}</p>
          <form @submit.prevent="bookRoom(room)">
            <label for="checkInDate">Check-in Date:</label>
            <input type="date" id="checkInDate" v-model="bookingDetails.checkInDate" required />
            <label for="checkOutDate">Check-out Date:</label>
            <input type="date" id="checkOutDate" v-model="bookingDetails.checkOutDate" required />
            <label for="numberOfGuests">Number of Guests:</label>
            <input type="number" id="numberOfGuests" v-model.number="bookingDetails.numberOfGuests" min="1" required />
            <label for="quantity">Quantity:</label>
            <input type="number" id="quantity" v-model.number="bookingDetails.quantity" min="1" required />
            <button class="btn-book-room" type="submit">Book Room</button>
          </form>
        </div>
      </div>
      <button class="btn-back-to-hotels" @click="backToHotels">Back to Hotels</button>
    </div>

    <button class="btn-view-bookings" @click="viewUserBookings">View Bookings</button>
    <button class="btn-logout" @click="logout">Logout</button>
  </div>
</template>

<style scoped>
/* General Page Styling */
.hotels-page {
  font-family: 'Arial', sans-serif;
  text-align: center;
  padding: 20px;
  background-color: #f9f9f9;
  color: #333;
}

.page-title {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 10px;
  color: #2c3e50;
}

.page-subtitle {
  font-size: 1.2rem;
  margin-bottom: 20px;
  color: #7f8c8d;
}

/* Hotel Cards */
.hotels-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 20px;
}

.hotel-card {
  width: 300px;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.hotel-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.hotel-image-container {
  height: 200px;
  overflow: hidden;
}

.hotel-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.hotel-name {
  font-size: 1.5rem;
  font-weight: bold;
  margin: 10px 0;
  color: #34495e;
}

.hotel-location {
  font-size: 1rem;
  color: #7f8c8d;
}

.btn-view-rooms {
  background-color: #3498db;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 10px 20px;
  font-size: 1rem;
  cursor: pointer;
  margin: 10px 0 20px;
  transition: background-color 0.2s ease;
}

.btn-view-rooms:hover {
  background-color: #2980b9;
}

/* Selected Hotel Details */
.selected-hotel {
  margin-top: 20px;
}

.rooms-title {
  font-size: 1.8rem;
  font-weight: bold;
  color: #2c3e50;
  margin-bottom: 20px;
}

/* Room Cards */
.rooms-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 20px;
}

.room-card {
  width: 250px;
  background: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  padding: 20px;
  text-align: left;
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.room-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
}

.room-name {
  font-size: 1.2rem;
  font-weight: bold;
  margin-bottom: 5px;
  color: #34495e;
}

.room-type, .room-price, .room-status {
  font-size: 0.9rem;
  color: #7f8c8d;
  margin-bottom: 5px;
}

form {
  margin-top: 10px;
}

form label {
  display: block;
  font-size: 0.9rem;
  color: #34495e;
  margin-bottom: 5px;
}

form input {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 0.9rem;
}

.btn-book-room {
  background-color: #27ae60;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 10px 15px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.2s ease;
  width: 100%;
}

.btn-book-room:hover {
  background-color: #219150;
}

/* Back and View Bookings Buttons */
.btn-back-to-hotels,
.btn-view-bookings,
.btn-logout {
  background-color: #e74c3c;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 10px 20px;
  font-size: 1rem;
  cursor: pointer;
  margin: 10px;
  transition: background-color 0.2s ease;
}

.btn-back-to-hotels:hover,
.btn-view-bookings:hover,
.btn-logout:hover {
  background-color: #c0392b;
}

/* Responsive Design */
@media (max-width: 768px) {
  .hotels-list, .rooms-list {
    flex-direction: column;
    align-items: center;
  }

  .hotel-card, .room-card {
    width: 90%;
  }
}
</style>

