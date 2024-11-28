<script>
export default {
  name: 'HotelComponent',
  props: {
    hotel: {
      type: Object,
      required: true,
      default: () => ({
        hotelId: '',
        location: '',
        name: '',
        standardCheckIn: '',
        standardCheckOut: '',
        numberOfRooms: '',
        amenities: ''
      })
    }
  }
};
</script>

<template>
  <div>
    <h1>Hotel</h1>
    <p>Hotel Name: {{ hotel.name }}</p>
    <p>Location: {{ hotel.location }}</p>
    <p>Standard Check In: {{ hotel.standardCheckIn }}</p>
    <p>Standard Check Out: {{ hotel.standardCheckOut }}</p>
  </div>
</template>
