<template>
  <nav class="navbar">
    <div class="navbar-container">
      <!-- Logo and Website Name -->
      <div class="navbar-logo">
        <router-link to="/" class="logo-container">
          <img src="@/assets/logo.jpg" alt="Logo" class="logo" />
          <span class="website-name">Tiare</span>
        </router-link>
      </div>
      <!-- Navigation Links -->
      <div class="navbar-links">
        <router-link to="/" class="navbar-item">Home</router-link>
        <router-link to="/gallery" class="navbar-item">Gallery</router-link>
        <router-link to="/login" class="navbar-item">Login</router-link>
        <router-link v-if="isAdmin" to="/admin/add-hotel" class="navbar-item">Add Hotel</router-link>
        <router-link v-if="isAdmin" to="/admin/add-room" class="navbar-item">Add Room</router-link>
      </div>
    </div>
  </nav>
  <router-view />
</template>

<script>
import { ref } from 'vue';
import { jwtDecode } from 'jwt-decode';

export default {
  name: 'App',
  setup() {
    const isAdmin = ref(false);

    const token = sessionStorage.getItem('Token');
    if (token) {
      try {
        const decodedToken = jwtDecode(token);
        isAdmin.value = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] === 'Admin';
      } catch (error) {
        console.error("Error decoding token:", error);
      }
    }

    return { isAdmin };
  }
};
</script>

<style scoped>
/* General Navbar Styles */
.navbar {
  background-color: #8B4513; 
  padding: 10px 20px; 
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.navbar-container {
  display: flex;
  justify-content: space-between; 
  align-items: center;
}

/* Logo and Website Name */
.navbar-logo {
  display: flex;
  align-items: center;
}

.logo-container {
  display: flex;
  align-items: center;
  text-decoration: none;
}

.logo {
  width: 40px; 
  height: auto;
  border-radius: 50%;
}

.website-name {
  font-size: 1.2rem;
  color: #FFFFFF; 
  margin-left: 10px;
  font-weight: bold;
  font-family: 'Merriweather', serif; 
}

/* Navigation Links */
.navbar-links {
  display: flex;
  justify-content: center;
  align-items: center;
}

.navbar-item {
  text-decoration: none;
  color: #FFFFFF; 
  font-size: 1rem; 
  margin: 0 15px; 
  font-weight: bold;
  transition: color 0.3s ease, transform 0.3s ease;
}

/* Hover Effects */
.navbar-item:hover {
  color: #FF8C00; 
  transform: translateY(-2px); 
}

/* Active Link */
.router-link-exact-active {
  color: #40E0D0; 
  text-decoration: underline;
}

/* Mobile Responsiveness */
@media (max-width: 768px) {
  .navbar-container {
    flex-direction: column;
    align-items: center;
  }

  .navbar-links {
    margin-top: 10px;
  }

  .navbar-item {
    margin: 10px 0;
  }
}
</style>
