<template>
  <nav class="navbar">
    <div class="navbar-container">
      <router-link to="/" class="navbar-item">Home</router-link>
      <router-link to="/gallery" class="navbar-item">Gallery</router-link>
      <router-link to="/login" class="navbar-item">Login</router-link>
    </div>
  </nav>
  <router-view />
</template>

<script>
export default {
  name: 'App',
};
</script>

<style scoped>
/* General Styles */
.navbar {
  background-color: #8B4513; /* Brown background */
  padding: 15px 20px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.navbar-container {
  display: flex;
  justify-content: center;
  align-items: center;
}

.navbar-item {
  text-decoration: none;
  color: #FFFFFF; /* White text */
  font-size: 1.2rem;
  margin: 0 20px;
  font-weight: bold;
  transition: color 0.3s ease, transform 0.3s ease;
}

/* Hover Effects */
.navbar-item:hover {
  color: #FF8C00; /* Orange on hover */
  transform: translateY(-2px); /* Slight lift effect */
}

/* Active Link */
.router-link-exact-active {
  color: #40E0D0; /* Blue color for active link */
  text-decoration: underline;
}

/* Mobile responsiveness */
@media (max-width: 768px) {
  .navbar-container {
    flex-direction: column;
    align-items: center;
  }

  .navbar-item {
    margin: 10px 0;
  }
}
</style>
