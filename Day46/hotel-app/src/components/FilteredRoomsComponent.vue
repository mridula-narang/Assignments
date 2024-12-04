<template>
    <div class="filtered-rooms">
      <h2 class="title">Filtered Rooms</h2>
      <h3 class="hotel-name">Hotel: {{ hotelName }}</h3>
      <div class="room-card" v-for="room in rooms" :key="room.roomId">
        <p class="room-name">Room Name: <span>{{ room.name }}</span></p>
        <p class="room-type">Room Type: <span>{{ room.type === 0 ? 'Single' : room.type === 1 ? 'Double' : 'Suite' }}</span></p>
        <p class="room-price">Price: <span>${{ room.price }} per night</span></p>
        <p class="room-availability">Availability: 
          <span :class="room.isBooked === 0 ? 'available' : 'not-available'">
            {{ room.isBooked === 0 ? 'Available' : 'Not Available' }}
          </span>
        </p>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "FilteredRoomsComponent",
    props: {
      rooms: {
        type: Array,
        required: true,
        default: () => [],
      },
      hotelName: {
        type: String,
        required: true,
        default: "",
      },
    },
  };
  </script>
  
  <style scoped>
  .filtered-rooms {
    max-width: 800px;
    margin: 0 auto;
    padding: 20px;
    background-color: #f9f9f9;
    border-radius: 10px;
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  }
  
  .title {
    text-align: center;
    font-size: 24px;
    color: #333;
    margin-bottom: 10px;
  }
  
  .hotel-name {
    text-align: center;
    font-size: 18px;
    color: #555;
    margin-bottom: 20px;
  }
  
  .room-card {
    border: 1px solid #ddd;
    border-radius: 8px;
    padding: 15px;
    margin-bottom: 15px;
    background-color: #fff;
    transition: box-shadow 0.3s ease;
  }
  
  .room-card:hover {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
  }
  
  .room-card p {
    font-size: 14px;
    color: #444;
    margin: 5px 0;
  }
  
  .room-card p span {
    font-weight: bold;
  }
  
  .room-name {
    font-size: 16px;
    color: #222;
  }
  
  .room-type {
    font-size: 15px;
    color: #555;
  }
  
  .room-price {
    font-size: 15px;
    color: #555;
  }
  
  .room-availability span {
    padding: 3px 8px;
    border-radius: 5px;
    font-weight: bold;
  }
  
  .room-availability .available {
    background-color: #d4edda;
    color: #155724;
  }
  
  .room-availability .not-available {
    background-color: #f8d7da;
    color: #721c24;
  }
  </style>
  