<script>
import { GetRooms } from '@/scripts/RoomService';

export default {
  name: 'RoomsComponent',
  data() {
    return {
      rooms: []
    };
  },
  created() {
    GetRooms()
      .then(response => {
        this.rooms = response.data;
        console.log(this.rooms);
      });
  },
  methods: {
    viewRoom(roomId) {
      this.$router.push({ path: `/room/${roomId}` });
    }
  }
};
</script>

<template>
  <div>
    <h1>Rooms</h1>
    <div v-for="room in rooms" :key="room.id" @click="viewRoom(room.id)">
      <p>Room Name: {{ room.name }}</p>
      <p>Room Type: {{ room.type }}</p>
      <p>Price: {{ room.price }}</p>
      <p>Availability: {{ room.isBooked ? 'Booked' : 'Available' }}</p>
    </div>
  </div>
</template>

<style>
div {
  cursor: pointer;
}
</style>