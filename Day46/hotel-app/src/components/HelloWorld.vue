<template>
  <div class="hotel-homepage">
    <!-- Welcome Section -->
    <div class="welcome-section">
      <div class="welcome-content">
        <img src="../assets/logo.jpg" alt="Tiare Logo" class="logo" />
        <h1>Welcome to Tiare</h1>
        <p>Your relaxing journey begins here.</p>
        <button class="cta-btn" @click="navigateToHotels">Explore Hotels</button>
      </div>
    </div>

    <!-- Featured Rooms -->
    <section class="featured-rooms">
      <h2>Our Featured Rooms</h2>
      <p class="section-description">
        Discover luxurious spaces designed for your utmost comfort.
      </p>
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
      <p class="section-description">
        Enhance your stay with our premium amenities.
      </p>
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
        <p>© 2024 Tiare. All rights reserved. Experience serenity and luxury.</p>
      </div>
    </footer>
  </div>
</template>

<script>
import { useRouter } from 'vue-router';

export default {
  name: 'HelloWorld',
  setup() {
    const router = useRouter();

    // Function to navigate to the hotels page
    const navigateToHotels = () => {
      router.push('/login'); 
    };

    return { navigateToHotels };
  },
  data() {
    return {
      featuredRooms: [
        {
          id: 1,
          image: require('@/assets/seaside-view-room.jpeg'),
          name: "Ocean View Suite",
          description: "Enjoy stunning ocean views.",
          price: "Rs. 6000/night",
        },
        {
          id: 2,
          image: require('@/assets/mountain-room.jpeg'),
          name: "Mountain Retreat",
          description: "Peaceful and serene.",
          price: "Rs. 5000/night",
        },
        {
          id: 3,
          image: require('@/assets/city-hotel-room.jpeg'),
          name: "Urban Luxury",
          description: "Stay in the heart of the city.",
          price: "Rs. 4000/night",
        },
      ],
      facilities: [
        {
          id: 1,
          image: require('@/assets/pool.jpeg'),
          title: "Pool",
          description: "Relax by the poolside.",
        },
        {
          id: 2,
          image: require('@/assets/spa.jpeg'),
          title: "Spa",
          description: "Rejuvenate at our wellness spa.",
        },
        {
          id: 3,
          image: require('@/assets/restaurant.jpeg'),
          title: "Restaurant",
          description: "Indulge in culinary excellence.",
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

<style scoped>
.hotel-homepage {
  font-family: 'Poppins', sans-serif;
  color: #333;
  line-height: 1.6;
  background: url('../assets/homepage-bg2.jpeg') no-repeat center center/cover;
  min-height: 100vh;
}

/* Welcome Section */
.welcome-section {
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: rgba(0, 0, 0, 0.5); 
  text-align: center;
  color: white;
}

.welcome-content {
  padding: 20px;
  border-radius: 15px;
}

.logo {
  width: 100px;
  margin-bottom: 20px;
}

h1 {
  font-size: 2.5rem;
  font-weight: bold;
  margin: 10px 0;
}

p {
  font-size: 1.2rem;
  margin-bottom: 20px;
}

.cta-btn {
  padding: 12px 24px;
  background-color: #ff6f61;
  color: white;
  border: none;
  border-radius: 25px;
  font-size: 1rem;
  cursor: pointer;
  transition: all 0.3s ease;
}

.cta-btn:hover {
  background-color: #e64c3c;
  transform: scale(1.05);
}

/* Featured Rooms */
.featured-rooms,
.facilities,
.testimonials {
  padding: 50px 20px;
  text-align: center;
}

.featured-rooms h2,
.facilities h2,
.testimonials h2 {
  font-size: 2.5rem;
  margin-bottom: 10px;
  color: #333;
}

.section-description {
  font-size: 1rem;
  color: #666;
  margin-bottom: 30px;
}

.room-cards,
.facility-cards,
.testimonial-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  justify-content: center;
}

.room-card,
.facility-card,
.testimonial-card {
  width: 300px;
  padding: 15px;
  background: white;
  border-radius: 10px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

.room-card img,
.facility-card img {
  width: 100%;
  height: 200px;
  object-fit: cover;
  border-radius: 10px 10px 0 0;
}

.room-card:hover,
.facility-card:hover,
.testimonial-card:hover {
  transform: translateY(-10px);
}

/* Testimonials */
.testimonials {
  padding: 50px 20px;
  text-align: center;
  position: relative;
}

.testimonials h2 {
  font-size: 2.5rem;
  margin-bottom: 10px;
  color: white; 
  text-shadow: 2px 2px 5px rgba(0, 0, 0, 0.7); 
}

.section-description {
  font-size: 1rem;
  color: #ddd; 
  margin-bottom: 30px;
}

.testimonial-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  justify-content: center;
}

.testimonial-card {
  width: 300px;
  padding: 15px;
  background: rgba(255, 255, 255, 0.8); 
  border-radius: 10px;
  text-align: center;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

.testimonial-card:hover {
  transform: translateY(-10px);
}

/* Footer */
footer {
  text-align: center;
  padding: 20px;
  background: #333;
  color: white;
}
</style>

