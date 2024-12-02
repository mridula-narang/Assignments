<template>
    <div class="gallery-container">
      <h1 class="gallery-title">Hotel Gallery</h1>
      <div class="gallery-grid">
        <div v-for="(image, index) in images" :key="index" class="gallery-item">
          <img :src="image.src" :alt="image.alt" @click="openModal(image)" />
          <div class="overlay">
            <p class="image-title">{{ image.alt }}</p>
          </div>
        </div>
      </div>
  
      <!-- Modal for Enlarged View -->
      <div v-if="selectedImage" class="modal" @click.self="closeModal">
        <div class="modal-content">
          <img :src="selectedImage.src" :alt="selectedImage.alt" />
          <p class="modal-caption">{{ selectedImage.alt }}</p>
          <button class="close-button" @click="closeModal">Close</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "GalleryComponent",
    data() {
      return {
        images: [
          { src: require("@/assets/outdoor-pool.jpeg"), alt: "Outdoor Pool" },
          { src: require("@/assets/spa2.jpeg"), alt: "Spa" },
          { src: require("@/assets/coffee-shop.jpeg"), alt: "Cafe" },
          { src: require("@/assets/restaurants.jpeg"), alt: "Restaurant" },
          { src: require("@/assets/wedding-hall.jpeg"), alt: "Wedding Hall" },
        ],
        selectedImage: null,
      };
    },
    methods: {
      openModal(image) {
        this.selectedImage = image;
      },
      closeModal() {
        this.selectedImage = null;
      },
    },
  };
  </script>
  
  <style scoped>
  /* General Styles */
  .gallery-container {
    padding: 40px 20px;
    text-align: center;
    background-color: #FFF5F5; /* Light Pink */
  }
  
  .gallery-title {
    font-size: 2.5rem;
    margin-bottom: 30px;
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    color: #8B4513; /* Brown */
    text-transform: uppercase;
    font-weight: bold;
    letter-spacing: 2px;
    text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.1);
  }
  
  /* Grid Layout */
  .gallery-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 20px;
  }
  
  .gallery-item {
    position: relative;
    overflow: hidden;
    border-radius: 10px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease;
  }
  
  .gallery-item img {
    width: 100%;
    height: auto;
    border-radius: 10px;
    transition: transform 0.3s ease;
  }
  
  .gallery-item:hover {
    transform: scale(1.05);
  }
  
  .gallery-item img:hover {
    transform: scale(1.1);
  }
  
  .overlay {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(0, 0, 0, 0.6);
    display: flex;
    justify-content: center;
    align-items: center;
    opacity: 0;
    transition: opacity 0.3s ease;
  }
  
  .gallery-item:hover .overlay {
    opacity: 1;
  }
  
  .image-title {
    color: #FFFFFF; /* White */
    font-size: 1.25rem;
    font-weight: 600;
    text-shadow: 2px 2px 6px rgba(0, 0, 0, 0.3);
  }
  
  /* Modal Styles */
  .modal {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: rgba(0, 0, 0, 0.8);
    display: flex;
    justify-content: center;
    align-items: center;
    z-index: 1000;
  }
  
  .modal-content {
    background-color: #FFFFFF; /* White */
    padding: 30px;
    border-radius: 15px;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.15);
    text-align: center;
  }
  
  .modal img {
    max-width: 90%;
    max-height: 80%;
    border-radius: 10px;
    margin-bottom: 20px;
  }
  
  .modal-caption {
    font-size: 1.2rem;
    color: #008B8B; /* Teal/Blue */
    margin-bottom: 20px;
    font-weight: 600;
  }
  
  .close-button {
    background-color: #FF8C00; /* Orange */
    color: #fff;
    border: none;
    padding: 12px 25px;
    font-size: 1.1rem;
    cursor: pointer;
    border-radius: 5px;
    transition: background 0.3s ease;
  }
  
  .close-button:hover {
    background-color: #FFA500; /* Lighter Orange */
  }
  </style>
  