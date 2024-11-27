<template>
  <div class="hotel-homepage">
    <!-- Hero Section -->
    <section class="hero-slider">
      <div class="slider-item">
        <img src="../assets/hero-slider.jpeg" alt="Slider Image" class="slider-bg" />
        <div class="slider-content">
          <h1>Welcome to Tiare</h1>
          <p>Where luxury meets comfort.</p>
          <button class="cta-btn" @click="navigateToLogin">Explore Hotels</button>
        </div>
      </div>
    </section>

    <!-- Featured Rooms -->
    <section class="featured-rooms">
      <h2>Our Featured Rooms</h2>
      <div class="room-cards">
        <div class="room-card" v-for="room in featuredRooms" :key="room.id">
          <img :src="room.image" :alt="room.name" />
          <h3>{{ room.name }}</h3>
          <p>{{ room.description }}</p>
          <span class="price">From {{ room.price }}</span>
        </div>
      </div>
    </section>

    <!-- Facilities Section -->
    <section class="facilities">
      <h2>Our Facilities</h2>
      <div class="facility-cards">
        <div class="facility-card" v-for="facility in facilities" :key="facility.id">
          <img :src="facility.image" :alt="facility.title" />
          <h3>{{ facility.title }}</h3>
          <p>{{ facility.description }}</p>
        </div>
      </div>
    </section>

    <!-- Testimonials -->
    <section class="testimonials">
      <h2>What Our Guests Say</h2>
      <div class="testimonial-cards">
        <div class="testimonial-card" v-for="testimonial in testimonials" :key="testimonial.id">
          <p>{{ testimonial.quote }}</p>
          <h4>- {{ testimonial.author }}</h4>
        </div>
      </div>
    </section>

    <!-- Footer -->
    <footer>
      <div class="footer-content">
        <p>© 2024 Tiare. All rights reserved.</p>
      </div>
    </footer>
  </div>
</template>

<script>
import { useLoginStore } from '@/stores/loginStore';
import { useRouter } from 'vue-router';
import { computed } from 'vue';

export default {
  name: 'HelloWorld',
  setup() {
    const loginStore = useLoginStore();
    const router = useRouter();

    // Computed property to get the username from the store
    const username = computed(() => loginStore.username);

    // Function to navigate to the login page
    const navigateToLogin = () => {
      router.push('/login');
    };

    return { username, navigateToLogin };
  },
  data() {
    return {
      featuredRooms: [
        {
          id: 1,
          image: require('@/assets/seaside-view-room.jpeg'), // Replace with the actual path to Room 1 image
          name: "Ocean View Suite",
          description: "Enjoy stunning ocean views.",
          price: "$300/night",
        },
        {
          id: 2,
          image: require('@/assets/mountain-room.jpeg'), // Replace with the actual path to Room 2 image
          name: "Mountain Retreat",
          description: "Peaceful and serene.",
          price: "$250/night",
        },
        {
          id: 3,
          image: require('@/assets/city-hotel-room.jpeg'),
          name: "Urban Luxury",
          description: "Stay in the heart of the city.",
          price: "$400/night",
        },
      ],
      facilities: [
        {
          id: 1,
          image: require('@/assets/pool.jpeg'), // Replace with the actual path to Facility 1 image
          title: "Pool",
          description: "Relax by the poolside.",
        },
        {
          id: 2,
          image: require('@/assets/spa.jpeg'), // Replace with the actual path to Facility 2 image
          title: "Spa",
          description: "Rejuvenate at our wellness spa.",
        },
        {
          id: 3,
          image: require('@/assets/restaurant.jpeg'), // Replace with the actual path to Facility 3 image
          title: "Restaurant",
          description: "Indulge in Culinary Excellence Where Every Meal is a Journey, Not Just a Destination!",
        },
      ],
      testimonials: [
        {
          id: 1,
          quote: "Best stay ever! Highly recommend.",
          author: "John Doe",
        },
        {
          id: 2,
          quote: "Amazing service and location.",
          author: "Jane Smith",
        },
        {
          id: 3,
          quote: "The rooms were fantastic, and the staff was incredibly friendly.",
          author: "Robert Brown",
        },
      ],
    };
  },
};

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.hotel-homepage {
  font-family: 'Open Sans', sans-serif;
}

/* Hero Section */
.hero-slider {
  position: relative;
  text-align: center;
  color: white;
}

.slider-bg {
  width: 100%;
  height: 400px;
  object-fit: cover;
  filter: brightness(70%); /* Dim the image for better contrast */
}

.hero-slider::before {
  content: "";
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.2); /* Black overlay with 50% transparency */
  z-index: 0;
}

.slider-content {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 1;
  color: #ffffff; /* White text for contrast */
  text-shadow: 0 0 5px rgba(0, 0, 0, 0.7); /* Add text shadow for better visibility */
  text-align: center;
}

.cta-btn {
  padding: 10px 20px;
  background-color: #ff6347; /* Vibrant orange button */
  color: white;
  border: none;
  cursor: pointer;
  border-radius: 5px;
  font-size: 16px;
  transition: background-color 0.3s ease;
}

.cta-btn:hover {
  background-color: #d9534f; /* Slightly darker hover effect */
}

/* Featured Rooms */
.featured-rooms {
  text-align: center;
  margin: 40px 0;
}

.room-cards {
  display: flex;
  gap: 20px;
  justify-content: center;
  flex-wrap: wrap;
}

.room-card {
  width: 300px;
  background: #fff;
  border: 1px solid #ddd;
  border-radius: 10px;
  overflow: hidden;
  text-align: center;
}

.room-card img {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.room-card h3 {
  font-size: 20px;
  margin: 10px;
}

.room-card p {
  font-size: 14px;
  margin: 10px;
  color: #555;
}

.price {
  font-weight: bold;
  font-size: 16px;
  margin: 10px;
}

/* Facilities */
.facilities {
  text-align: center;
  margin-top: 40px;
}

.facility-cards {
  display: flex;
  gap: 20px;
  justify-content: center;
}

.facility-card {
  width: 300px;
  text-align: center;
}

.facility-card img {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.facility-card h3 {
  font-size: 18px;
  margin-top: 10px;
}

.facility-card p {
  font-size: 14px;
  color: #555;
}

/* Testimonials */
.testimonials {
  background-color: #f9f9f9;
  padding: 40px 20px;
  text-align: center;
}

.testimonial-cards {
  display: flex;
  gap: 20px;
  justify-content: center;
  flex-wrap: wrap;
}

.testimonial-card {
  width: 300px;
  background: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0px 4px 6px rgba(0, 0, 0, 0.1);
}

.testimonial-card p {
  font-size: 14px;
  color: #555;
}

.testimonial-card h4 {
  margin-top: 10px;
  font-size: 16px;
  font-weight: bold;
}

footer {
  text-align: center;
  padding: 20px 0;
  background-color: #333;
  color: white;
}
</style>
