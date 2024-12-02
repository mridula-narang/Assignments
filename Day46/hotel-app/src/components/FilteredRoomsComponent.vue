<template>
  <div>
      <h2>Filtered Rooms</h2>
      <h3>Hotel: {{ hotelName }}</h3>
      <div v-for="room in rooms" :key="room.roomId">
          <p>Room Name: {{ room.name }}</p>
          <p>Room Type: {{ room.type === 0 ? 'Single' : room.type === 1 ? 'Double' : 'Suite' }}</p>
          <p>Price: ${{ room.price }} per night</p>
          <p>Availability: {{ room.isBooked === 0 ? 'Available' : 'Not Available' }}</p>
      </div>
  </div>
</template>

<script>
export default {
  name: 'FilteredRoomsComponent',
  props: {
      rooms: {
          type: Array,
          required: true,
          default: () => []
      },
      hotelName: {
          type: String,
          required: true,
          default: ''
      }
  }
};
</script>

<style scoped>
/* Add any styles you need for the filtered rooms component */
</style>