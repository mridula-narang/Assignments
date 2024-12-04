<template>
    <div>
      <h1>Add Room</h1>
      <form @submit.prevent="submitForm">
        <div>
          <label for="hotelId">Hotel ID:</label>
          <input v-model.number="room.hotelId" id="hotelId" type="number" required />
        </div>
        <div>
          <label for="name">Room Name:</label>
          <input v-model="room.name" id="name" type="text" required />
        </div>
        <div>
          <label for="type">Room Type:</label>
          <input v-model.number="room.type" id="type" type="number" required />
        </div>
        <div>
          <label for="price">Price:</label>
          <input v-model.number="room.price" id="price" type="number" required />
        </div>
        <div>
          <label for="isBooked">Is Booked (1 for Yes, 0 for No):</label>
          <input v-model.number="room.isBooked" id="isBooked" type="number" required />
        </div>
        <div>
          <label for="features">Features:</label>
          <input v-model="room.features" id="features" type="text" required />
        </div>
        <div>
          <label for="capacity">Capacity:</label>
          <input v-model.number="room.capacity" id="capacity" type="number" required />
        </div>
        <button type="submit">Add Room</button>
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
          this.$router.push('/'); // Redirect to home or another page after success
        } catch (error) {
          console.error(error);
          alert("Failed to add room.");
        }
      },
    },
  };
  </script>
  