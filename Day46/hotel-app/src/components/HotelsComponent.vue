<template>
  <div class="hotels-page">
    <h1 class="page-title">Explore Our Hotels</h1>
    <p class="page-subtitle">
      Find the perfect place to stay from our curated selection of luxurious hotels.
    </p>

    <div class="content">
      <!-- Filter Sidebar -->
      <div class="filter-sidebar">
        <h2>Filter Rooms</h2>
        <form @submit.prevent="filterRooms">
          <label for="priceRange">Price Range: ₹{{ filters.minPrice }} - ₹{{ filters.maxPrice }}</label>
          <input type="range" id="minPrice" v-model="filters.minPrice" :min="4000" :max="filters.maxPrice - 1000"
            step="1000" />
          <input type="range" id="maxPrice" v-model="filters.maxPrice" :min="filters.minPrice + 1000" :max="10000"
            step="1000" />

          <p>Selected Range: ₹{{ filters.minPrice }} - ₹{{ filters.maxPrice }}</p>

          <label for="type">Room Type:</label>
          <select id="type" v-model="filters.type">
            <option value="">Any</option>
            <option value="0">Single</option>
            <option value="1">Double</option>
            <option value="2">Suite</option>
          </select>

          <label for="location">Location:</label>
          <input type="text" id="location" v-model="filters.location" />

          <label for="capacity">Capacity:</label>
          <input type="number" id="capacity" v-model.number="filters.capacity" />

          <button type="submit">Filter</button>
        </form>
        <a href="#" class="clear-filters">Clear All Filters</a>
      </div>

      <!-- Hotels and Rooms List -->
      <div class="main-content">
        <div v-if="!filtersApplied" class="hotels-list">
          <div class="hotel-card" v-for="hotel in hotels" :key="hotel.hotelId">
            <img :src="getImageSrc(hotel.hotelId)" alt="Hotel Image" class="hotel-image" @error="setDefaultImage" />
            <h2 class="hotel-name">{{ hotel.name }}</h2>
            <p class="hotel-location">{{ hotel.location }}</p>
            <button class="btn-view-rooms" @click="viewRooms(hotel)">View Rooms</button>
          </div>
        </div>

        <div v-else>
          <h2 class="rooms-title" v-if="selectedHotel">
            Rooms in {{ selectedHotel.name }}
          </h2>
          <h2 class="rooms-title" v-else>
            Filtered Rooms
          </h2>
          <div class="rooms-list">
            <div class="room-card" v-for="room in rooms" :key="room.roomId">
              <h3 class="room-name">{{ room.name }}</h3>
              <p class="room-type">Type: {{ getRoomType(room.type) }}</p>
              <p class="room-features">
                Features: {{ room.features }}
              </p>
              <p class="room-price">Price: ₹{{ room.price }}</p>
              <p class="room-status">
                Status: {{ room.isBooked === 0 ? 'Available' : 'Not Available' }}
              </p>

              <form @submit.prevent="bookRoom(room)">
                <label for="checkInDate">Check-In Date:</label>
                <input type="date" id="checkInDate" :value="bookingDetails[room.roomId]?.checkInDate"
                  @input="updateBookingDetail(room.roomId, 'checkInDate', $event.target.value)" :min="today" required />

                <label for="checkOutDate">Check-Out Date:</label>
                <input type="date" id="checkOutDate" :value="bookingDetails[room.roomId]?.checkOutDate"
                  @input="updateBookingDetail(room.roomId, 'checkOutDate', $event.target.value)"
                  :min="bookingDetails[room.roomId]?.checkInDate || today" required />

                <label for="numberOfGuests">Guests:</label>
                <input type="number" id="numberOfGuests" :value="bookingDetails[room.roomId]?.numberOfGuests || 1"
                  @input="updateBookingDetail(room.roomId, 'numberOfGuests', $event.target.value)" min="1" required />

                <label for="quantity">Quantity:</label>
                <input type="number" id="quantity" :value="bookingDetails[room.roomId]?.quantity || 1"
                  @input="updateBookingDetail(room.roomId, 'quantity', $event.target.value)" min="1" required />

                <button type="submit" class="btn-book-room">Book Room</button>
              </form>
            </div>
          </div>

          <button class="btn-back-to-hotels" @click="resetFilters">Back to Hotels</button>
        </div>
      </div>
    </div>

    <button class="btn-view-bookings" @click="viewUserBookings">View Bookings</button>
    <button v-if="isAdmin" class="btn-view-all-bookings" @click="viewAllBookings">
      View All Bookings
    </button>

    <button class="btn-logout" @click="logout">Logout</button>
  </div>
</template>

<script>
import { GetHotels } from "@/scripts/HotelService";
import axios from "@/scripts/myAxiosInterceptor";
import { MakeBookingForUser } from "@/scripts/BookingService";

export default {
  name: "HotelsComponent",
  data() {
    return {
      hotels: [],
      selectedHotel: null,
      rooms: [],
      bookingDetails: {},
      filters: {
        minPrice: 4000,
        maxPrice: 10000,
        type: "",
        location: "",
        capacity: null,
        isAvailable: "",
      },
      filtersApplied: false,
      isAdmin: false,
    };
  },
  created() {
    GetHotels()
      .then((response) => {
        this.hotels = response.data;
      })
      .catch((err) => {
        console.error("Error fetching hotels:", err);
      });
  },
  computed: {
    today() {
      const now = new Date();
      return now.toISOString().split("T")[0];
    },
  },
  methods: {
    getImageSrc(hotelId) {
      try {
        return require(`@/assets/${hotelId}.jpeg`);
      } catch (error) {
        return require("@/assets/default-img.jpg");
      }
    },
    setDefaultImage(event) {
      event.target.src = require("@/assets/default-img.jpg");
    },
    viewRooms(hotel) {
      this.selectedHotel = hotel;
      this.fetchRooms();
    },
    fetchRooms() {
      const params = { ...this.filters };
      let url = "http://localhost:5263/api/Room/filter";
      if (this.selectedHotel) {
        url = `http://localhost:5263/api/Hotel/${this.selectedHotel.hotelId}/rooms`;
      }

      axios
        .get(url, { params })
        .then((response) => {
          this.rooms = response.data.sort((a, b) => a.price - b.price);
          this.filtersApplied = true;
        })
        .catch((err) => {
          console.error("Error fetching rooms:", err);
        });
    },
    resetFilters() {
      this.filtersApplied = false;
      this.selectedHotel = null;
      this.rooms = [];
    },
    bookRoom(room) {
      const details = this.bookingDetails[room.roomId] || {};
      if (details.checkInDate && details.checkOutDate && details.numberOfGuests && details.quantity) {
        const payload = { ...details, roomId: room.roomId, hotelId: this.selectedHotel?.hotelId };
        MakeBookingForUser(payload)
          .then(() => {
            alert("Booking successful!");
            this.bookingDetails[room.roomId] = {};
          })
          .catch((err) => {
            console.error("Error making booking:", err);
            alert("Booking failed. Please try again.");
          });
      } else {
        alert("Please fill in all booking details.");
      }
    },
    updateBookingDetail(roomId, field, value) {
      if (!this.bookingDetails[roomId]) {
        this.bookingDetails[roomId] = {
          checkInDate: '',
          checkOutDate: '',
          numberOfGuests: 1,
          quantity: 1,
        };
      }
      this.bookingDetails[roomId][field] = value;
    },

    viewUserBookings() {
      this.$router.push("/bookings");
    },
    logout() {
      sessionStorage.clear();
      this.$router.push("/");
    },
    getRoomType(type) {
      return ["Single", "Double", "Suite"][type] || "Unknown";
    },
  },
};
</script>


<style scoped>
/* General Page Styling */
.hotels-page {
  font-family: 'Arial', sans-serif;
  text-align: center;
  padding: 20px;
  background-image: url('../assets/hotels-bg.jpg');

  background-size: cover;
  background-repeat: no-repeat;
  background-attachment: fixed;
  color: #b48c34;
}


.page-title {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 10px;
  background: linear-gradient(90deg, #f6a77a, #55c9b8);

  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}

.page-subtitle {
  font-size: 1.2rem;
  margin-bottom: 20px;
  color: #7f8c8d;
}

.content {
  display: flex;
}

/* Sidebar Styling */
.filter-sidebar {
  width: 250px;
  padding: 20px;
  background-color: rgba(255, 255, 255, 0.8);
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  margin-right: 20px;
}

.filter-sidebar h2 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 20px;
  color: #000000;
}

.filter-sidebar form label {
  display: block;
  font-size: 0.9rem;
  color: #0c0d0e;
  margin-bottom: 5px;
}

.filter-sidebar form input,
.filter-sidebar form select {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 0.9rem;
}

.filter-sidebar form button {
  background-color: #55c9b8;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 10px 15px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.2s ease;
  width: 100%;
}

.filter-sidebar form button:hover {
  background-color: #3ba69c;
}

/* Main Content Styling */
.main-content {
  flex: 1;
}

.hotels-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
  gap: 20px;
}

.hotel-card {
  width: 250px;
  height: 350px;
  background: rgba(255, 255, 255, 0.9);
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
  height: 150px;
  overflow: hidden;
}

.hotel-image {
  width: 100%;
  height: 40%;
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
  background-color: #f6a77a;
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
  background-color: #e8965b;
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
  background: rgba(255, 255, 255, 0.9);
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

.btn-book-room {
  background-color: #55c9b8;
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
  background-color: #3ba69c;
}

.btn-back-to-hotels,
.btn-view-bookings,
.btn-logout {
  background-color: #f6a77a;
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
  background-color: #e8965b;
}

/* Global Reset */
body,
html {
  margin: 0;
  padding: 0;
  font-family: 'Arial', sans-serif;
  box-sizing: border-box;
  background: #f9f9f9;

}

/* Navbar Styling */
.navbar {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 20px;
  background: linear-gradient(135deg, #f6a77a, #55c9b8);
  color: #fff;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  position: sticky;
  top: 0;
  z-index: 1000;
}

.navbar .logo {
  display: flex;
  align-items: center;
  text-decoration: none;
}

.navbar .logo img {
  height: 40px;
  width: 40px;
  margin-right: 10px;
  border-radius: 50%;
  border: 2px solid #fff;
}

.navbar .logo h1 {
  font-size: 1.5rem;
  font-weight: bold;
  margin: 0;
  color: #fff;
}

.navbar ul {
  list-style: none;
  display: flex;
  margin: 0;
  padding: 0;
}

.navbar ul li {
  margin: 0 15px;
}

.navbar ul li a {
  text-decoration: none;
  color: #fff;
  font-size: 1rem;
  transition: color 0.2s ease;
}

.navbar ul li a:hover {
  color: #ffe0b5;
}

/* Filter Sidebar Styling */
.filter-sidebar {
  width: 300px;
  padding: 20px;
  background: rgba(255, 255, 255, 0.95);
  border-radius: 12px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
  border: 2px solid transparent;
  background-clip: padding-box;
  position: sticky;
  top: 20px;
  margin-right: 20px;
}

/* Gradient Border */
.filter-sidebar::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: -1;
  margin: -2px;
  border-radius: 14px;
  background: linear-gradient(135deg, #f6a77a, #55c9b8);
}

/* Sidebar Title */
.filter-sidebar h2 {
  font-size: 1.8rem;
  font-weight: bold;
  margin-bottom: 20px;
  text-align: center;
  color: #34495e;
  background: linear-gradient(90deg, #f6a77a, #55c9b8);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
}

/* Form Labels */
.filter-sidebar form label {
  display: block;
  font-size: 1rem;
  font-weight: bold;
  color: #34495e;
  margin-bottom: 8px;
}

/* Form Inputs */
.filter-sidebar form input,
.filter-sidebar form select {
  width: 100%;
  padding: 10px;
  margin-bottom: 15px;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 1rem;
  background: rgba(245, 245, 245, 0.9);
  color: #34495e;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
  transition: border-color 0.2s ease, box-shadow 0.2s ease;
}

.filter-sidebar form input:focus,
.filter-sidebar form select:focus {
  border-color: #55c9b8;
  box-shadow: 0 0 5px rgba(85, 201, 184, 0.6);
  outline: none;
}

/* Submit Button */
.filter-sidebar form button {
  background: linear-gradient(90deg, #f6a77a, #55c9b8);
  color: #fff;
  border: none;
  border-radius: 8px;
  padding: 12px 20px;
  font-size: 1.2rem;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
  width: 100%;
}

.filter-sidebar form button:hover {
  background: linear-gradient(90deg, #e8965b, #3ba69c);
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

/* Clear Filter Link */
.filter-sidebar .clear-filters {
  display: block;
  text-align: center;
  margin-top: 10px;
  font-size: 1rem;
  color: #55c9b8;
  text-decoration: none;
  transition: color 0.2s ease;
}

.filter-sidebar .clear-filters:hover {
  color: #3ba69c;
  text-decoration: underline;
}

/* Responsive Design */
@media (max-width: 768px) {
  .navbar ul {
    flex-direction: column;
    align-items: center;
  }

  .navbar ul li {
    margin: 10px 0;
  }

  .filter-sidebar {
    width: 100%;
    margin-right: 0;
    margin-bottom: 20px;
  }
}

.room-card {
  width: 300px;
  background: linear-gradient(135deg, #f6a77a, #55c9b8);
  border-radius: 12px;
  box-shadow: 0 6px 12px rgba(0, 0, 0, 0.15);
  padding: 20px;
  color: white;
  text-align: left;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.room-card:hover {
  transform: translateY(-10px);
  box-shadow: 0 10px 20px rgba(0, 0, 0, 0.25);
}

.room-card h3 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 10px;
  color: #fff;
}

.room-card p {
  font-size: 1rem;
  margin-bottom: 8px;
}

.room-card p.room-type {
  font-weight: bold;
}

.room-card p.room-status {
  font-style: italic;
}

.room-card button {
  background-color: white;
  color: #55c9b8;
  border: none;
  border-radius: 8px;
  padding: 10px 15px;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.room-card button:hover {
  background-color: #ffe0b5;
  transform: scale(1.05);
  color: #f6a77a;
}

/* Book Room Form Styling */
.room-card form {
  margin-top: 15px;
  padding: 15px;
  background-color: rgba(245, 245, 245, 0.95);
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.room-card form label {
  display: block;
  font-size: 1rem;
  font-weight: bold;
  color: #34495e;
  margin-bottom: 5px;
}

.room-card form input {
  width: 100%;
  padding: 8px;
  margin-bottom: 10px;
  border: 1px solid #ddd;
  border-radius: 8px;
  font-size: 0.9rem;
}

.room-card form input:focus {
  border-color: #55c9b8;
  outline: none;
  box-shadow: 0 0 6px rgba(85, 201, 184, 0.5);
}

.room-card form .btn-book-room {
  background: linear-gradient(90deg, #f6a77a, #55c9b8);
  color: #fff;
  font-size: 1rem;
  font-weight: bold;
  padding: 10px 15px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.2s ease;
  margin-top: 10px;
  width: 100%;
}

.room-card form .btn-book-room:hover {
  background: linear-gradient(90deg, #e8965b, #3ba69c);
  transform: translateY(-2px);
}

.room-card form .btn-book-room:active {
  transform: translateY(0);
}

.room-card form .form-section {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.room-card form .form-section div {
  flex: 1;
  min-width: 120px;
}
</style>