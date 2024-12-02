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
          <label for="minPrice">Min Price:</label>
          <input type="number" id="minPrice" v-model.number="filters.minPrice" />

          <label for="maxPrice">Max Price:</label>
          <input type="number" id="maxPrice" v-model.number="filters.maxPrice" />

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

          <label for="isAvailable">Availability:</label>
          <select id="isAvailable" v-model="filters.isAvailable">
            <option value="">Any</option>
            <option value="true">Available</option>
            <option value="false">Not Available</option>
          </select>

          <button type="submit">Filter</button>
        </form>
      </div>

      <!-- Hotels and Rooms List -->
      <div class="main-content">
        <div v-if="!filtersApplied" class="hotels-list">
          <div class="hotel-card" v-for="hotel in hotels" :key="hotel.hotelId">
            <div class="hotel-image-container">
              <img :src="require('@/assets/' + hotel.hotelId + '.jpeg')" alt="Hotel Image" class="hotel-image" />
            </div>
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
              <p class="room-price">Price: {{ room.price }}</p>
              <p class="room-status">
                Status: {{ room.isBooked === 0 ? 'Available' : 'Not Available' }}
              </p>


              <form @submit.prevent="bookRoom(room)">
                <label for="checkInDate">Check-In Date:</label>
                <input type="date" id="checkInDate" v-model="bookingDetails.checkInDate" required />

                <label for="checkOutDate">Check-Out Date:</label>
                <input type="date" id="checkOutDate" v-model="bookingDetails.checkOutDate" required />

                <label for="numberOfGuests">Guests:</label>
                <input type="number" id="numberOfGuests" v-model.number="bookingDetails.numberOfGuests" min="1"
                  required />

                <label for="quantity">Quantity:</label>
                <input type="number" id="quantity" v-model.number="bookingDetails.quantity" min="1" required />

                <button type="submit" class="btn-book-room">
                  Book Room
                </button>
              </form>
            </div>
          </div>

          <button class="btn-back-to-hotels" @click="resetFilters">Back to Hotels</button>
        </div>
      </div>
    </div>

    <button class="btn-view-bookings" @click="viewUserBookings">View Bookings</button>
    <button class="btn-logout" @click="logout">Logout</button>
  </div>
</template>

<script>
import { GetHotels } from "@/scripts/HotelService";
import axios from "@/scripts/myAxiosInterceptor";
import { MakeBookingForUser, CancelBookingForUser } from "@/scripts/BookingService";

export default {
  name: "HotelsComponent",
  data() {
    return {
      hotels: [],
      selectedHotel: null,
      rooms: [],
      bookingDetails: {
        hotelId: null,
        roomId: null,
        checkInDate: "",
        checkOutDate: "",
        numberOfGuests: 1,
        quantity: 1,
        status: 0,
      },
      filters: {
        minPrice: 4000,
        maxPrice: 10000,
        type: "",
        location: "",
        capacity: null,
        isAvailable: "",
      },
      filtersApplied: false,
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
  methods: {
    viewRooms(hotel) {
      this.selectedHotel = hotel;
      this.fetchRooms();
    },
    fetchRooms() {
      const params = { ...this.filters };
      console.log("Fetching rooms with params:", params);

      let url = "http://localhost:5263/api/Room/filter";
      if (this.selectedHotel) {
        url = `http://localhost:5263/api/Hotel/${this.selectedHotel.hotelId}/rooms`;
      }

      axios
        .get(url, { params })
        .then((response) => {
          this.rooms = response.data;
          this.filtersApplied = true;
          console.log("Rooms fetched:", this.rooms);
        })
        .catch((err) => {
          console.error("Error fetching rooms:", err);
        });
    },
    fetchAvailableRooms() {
      axios
        .get("http://localhost:5263/api/Booking/available-rooms")
        .then((response) => {
          this.rooms = response.data;
          console.log("Available rooms fetched:", this.rooms);
        })
        .catch((err) => {
          console.error("Error fetching available rooms:", err);
        });
    },
    filterRooms() {
      this.selectedHotel = null; // Clear selected hotel for global filtering
      this.fetchRooms();
    },
    resetFilters() {
      this.filtersApplied = false;
      this.selectedHotel = null;
      this.rooms = [];
    },
    bookRoom(room) {
      this.bookingDetails.hotelId = this.selectedHotel?.hotelId;
      this.bookingDetails.roomId = room.roomId;

      if (
        this.bookingDetails.checkInDate &&
        this.bookingDetails.checkOutDate &&
        this.bookingDetails.numberOfGuests > 0 &&
        this.bookingDetails.quantity > 0
      ) {
        MakeBookingForUser(this.bookingDetails)
          .then(() => {
            alert("Booking successful!");
            this.resetBookingForm();
          })
          .catch((err) => {
            console.error("Error making booking:", err);
            alert("Booking failed. Please try again.");
          });
      } else {
        alert("Please fill in all booking details.");
      }
    },
    cancelBooking(room) {
      CancelBookingForUser(room.roomId)
        .then(() => {
          alert("Booking cancelled successfully!");
          this.fetchAvailableRooms();
        })
        .catch((err) => {
          console.error("Error cancelling booking:", err);
          alert("Cancellation failed. Please try again.");
        });
    },
    resetBookingForm() {
      this.bookingDetails = {
        hotelId: null,
        roomId: null,
        checkInDate: "",
        checkOutDate: "",
        numberOfGuests: 1,
        quantity: 1,
        status: 0,
      };
    },
    viewUserBookings() {
      this.$router.push("/bookings");
    },
    logout() {
      sessionStorage.clear();
      this.$router.push("/");
    },
    getRoomType(type) {
      switch (type) {
        case 0:
          return 'Single';
        case 1:
          return 'Double';
        case 2:
          return 'Suite';
        default:
          return 'Unknown';
      }
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

.content {
  display: flex;
}

.filter-sidebar {
  width: 250px;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  margin-right: 20px;
}

.filter-sidebar h2 {
  font-size: 1.5rem;
  font-weight: bold;
  margin-bottom: 20px;
  color: #2c3e50;
}

.filter-sidebar form label {
  display: block;
  font-size: 0.9rem;
  color: #34495e;
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
  background-color: #3498db;
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
  background-color: #2980b9;
}

.main-content {
  flex: 1;
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

.room-type,
.room-price,
.room-status,
.room-features {
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

form input,
form select {
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

.btn-cancel-booking {
  background-color: #e74c3c;
  color: #fff;
  border: none;
  border-radius: 4px;
  padding: 10px 15px;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.2s ease;
  width: 100%;
  margin-top: 10px;
}

.btn-cancel-booking:hover {
  background-color: #c0392b;
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
  .content {
    flex-direction: column;
    align-items: center;
  }

  .filter-sidebar {
    width: 100%;
    margin-bottom: 20px;
  }

  .hotels-list,
  .rooms-list {
    flex-direction: column;
    align-items: center;
  }

  .hotel-card,
  .room-card {
    width: 90%;
  }
}
</style>