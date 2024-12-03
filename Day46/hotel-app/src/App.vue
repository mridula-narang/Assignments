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
      </div>
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
/* General Navbar Styles */
.navbar {
  background-color: #8B4513; /* Brown background */
  padding: 10px 20px; /* Thinner padding for a sleeker look */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.navbar-container {
  display: flex;
  justify-content: space-between; /* Space between logo and links */
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
  width: 40px; /* Smaller logo size for a thinner design */
  height: auto;
  border-radius: 50%;
}

.website-name {
  font-size: 1.2rem;
  color: #FFFFFF; /* White text for the website name */
  margin-left: 10px;
  font-weight: bold;
  font-family: 'Merriweather', serif; /* Matching logo aesthetic */
}

/* Navigation Links */
.navbar-links {
  display: flex;
  justify-content: center;
  align-items: center;
}

.navbar-item {
  text-decoration: none;
  color: #FFFFFF; /* White text */
  font-size: 1rem; /* Slightly smaller font size */
  margin: 0 15px; /* Reduced margin for a compact look */
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
