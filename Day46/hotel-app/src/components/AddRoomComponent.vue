<template>
  <div class="room-form-container">
    <div class="form-header">
      <h1>Add Room</h1>
    </div>
    <form class="room-form" @submit.prevent="submitForm">
      <div class="form-group">
        <label for="hotelId">Hotel ID:</label>
        <input v-model.number="room.hotelId" id="hotelId" type="number" required />
      </div>
      <div class="form-group">
        <label for="name">Room Name:</label>
        <input v-model="room.name" id="name" type="text" required />
      </div>
      <div class="form-group">
        <label for="type">Room Type:</label>
        <input v-model.number="room.type" id="type" type="number" required />
      </div>
      <div class="form-group">
        <label for="price">Price:</label>
        <input v-model.number="room.price" id="price" type="number" required />
      </div>
      <div class="form-group">
        <label for="isBooked">Is Booked (1 for Yes, 0 for No):</label>
        <input v-model.number="room.isBooked" id="isBooked" type="number" required />
      </div>
      <div class="form-group">
        <label for="features">Features:</label>
        <input v-model="room.features" id="features" type="text" required />
      </div>
      <div class="form-group">
        <label for="capacity">Capacity:</label>
        <input v-model.number="room.capacity" id="capacity" type="number" required />
      </div>
      <button type="submit" class="submit-button">Add Room</button>
    </form>
  </div>
</template>

<script>
import { AddRoom } from '@/scripts/RoomService';

export default {
  name: "AddRoomComponent",
  data() {
    return {
      room: {
        hotelId: 0,
        name: "",
        type: 0,
        price: 0,
        isBooked: 0,
        features: "",
        capacity: 0,
      },
    };
  },
  methods: {
    async submitForm() {
      try {
        await AddRoom(this.room);
        alert("Room added successfully!");
        this.$router.push('/hotels');
      } catch (error) {
        console.error(error);
        alert("Failed to add room.");
      }
    },
  },
};
</script>

<style scoped>
.room-form-container {
  max-width: 500px;
  margin: 0 auto;
  padding: 20px;
  background-color: #f7f0e9; /* Matches the warm beige tone */
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.form-header {
  text-align: center;
  margin-bottom: 20px;
}

h1 {
  font-size: 1.5rem;
  color: #4a4a4a; /* Neutral complementary color */
}

.room-form {
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
  border-color: #77c7c7; /* Matches the teal in the logo */
  outline: none;
}

.submit-button {
  padding: 10px 15px;
  background-color: #77c7c7; /* Teal from the logo */
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
