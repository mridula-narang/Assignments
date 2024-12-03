<template>
    <div class="not-found">
      <div class="logo-container">
        <img src="@/assets/logo.jpg" alt="Logo" class="logo" />
      </div>
      <h1>404 - Page Not Found</h1>
      <p>What you’re looking for isn’t here yet. Our team might still be crafting something beautiful, or it’s coming soon!</p>
      <router-link to="/" class="btn">Return Home</router-link>
    </div>
  </template>
  
  <script>
  export default {
    name: 'NotFound',
  };
  </script>
  
  <style scoped>
  .not-found {
    background-color: #d9c0a7; /* Matching beige background */
    color: #37444f; /* Dark teal for text */
    text-align: center;
    font-family: 'Merriweather', serif; /* Elegant serif font */
    padding: 40px;
    height: 100vh;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  
  .logo-container {
    margin-bottom: 20px;
  }
  
  .logo {
    width: 120px; /* Adjust size as needed */
    height: auto;
    border-radius: 50%;
    border: 2px solid #37444f;
  }
  
  h1 {
    font-size: 2.5rem;
    color: #37444f;
  }
  
  p {
    font-size: 1.2rem;
    color: #37444f;
    margin: 20px 0;
  }
  
  .btn {
    background-color: #37444f;
    color: #d9c0a7;
    text-decoration: none;
    padding: 10px 20px;
    border-radius: 5px;
    font-size: 1rem;
    transition: background-color 0.3s ease;
  }
  
  .btn:hover {
    background-color: #2a3c47;
  }
  </style>
  