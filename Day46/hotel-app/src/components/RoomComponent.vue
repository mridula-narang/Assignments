<script>
import { GetRoomById } from '@/scripts/RoomService';

export default {
  name: 'RoomComponent',
  props: {
    id: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      room: {
        roomId: '',
        type: '',
        price: '',
        isBooked: '',
        name: '',
        features: '',
        capacity: '',
        hotelId: '',
        hotel: '',
        bookings: ''
      }
    };
  },
  created() {
    GetRoomById(this.id)
      .then(response => {
        this.room = response.data;
      })
      .catch(error => {
        console.error('Error fetching room details:', error);
      });
  },
  computed: {
    roomType() {
      switch (this.room.type) {
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
    bookingStatus() {
      return this.room.isBooked === 0 ? 'Available' : 'Not Available';
    }
  }
};
</script>

<template>
  <div>
    <h1>Room</h1>
    <div>
      <p>Room Name: {{ room.name }}</p>
      <p>Type of Room: {{ roomType }}</p>
      <p>Price of Room: {{ room.price }}</p>
      <p>Features of Room: {{ room.features }}</p>
      <p>Capacity: {{ room.capacity }}</p>
      <p>Availability: {{ bookingStatus }}</p>
    </div>
  </div>
</template>

<style>
</style>