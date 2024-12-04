<template>
    <div class="forbidden-container">
      <div class="forbidden-content">
        <h1>403 - Forbidden</h1>
        <p>You do not have permission to access this page.</p>
        <router-link to="/hotels" class="back-button">Go Back to Hotels</router-link>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "ForbiddenComponent",
  };
  </script>
  
  <style scoped>
  .forbidden-container {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    background-color: #f7f0e9; 
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  }
  
  .forbidden-content {
    text-align: center;
    padding: 20px;
    background-color: white;
    border-radius: 8px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }
  
  h1 {
    font-size: 2rem;
    color: #ff4d4d; 
    margin-bottom: 10px;
  }
  
  p {
    color: #4a4a4a;
    margin-bottom: 20px;
  }
  
  .back-button {
    display: inline-block;
    padding: 10px 20px;
    background-color: #77c7c7; 
    color: white;
    text-decoration: none;
    border-radius: 4px;
    font-size: 1rem;
    transition: background-color 0.3s;
  }
  
  .back-button:hover {
    background-color: #5aa7a7;
  }
  </style>
  