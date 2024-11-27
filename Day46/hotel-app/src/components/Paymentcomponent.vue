<template>
    <div class="payment-page">
      <h1>Payment Page</h1>
      <p>Processing payment for Booking ID: {{ bookingId }}</p>
      <button @click="processPayment" class="pay-button">Pay Now</button>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    name: "DummyPaymentPage",
    data() {
      return {
        bookingId: this.$route.params.bookingId, // Get bookingId from the route
      };
    },
    methods: {
      async processPayment() {
        try {
          // Simulate a delay for the payment process
          await new Promise((resolve) => setTimeout(resolve, 2000));
  
          // Update the booking status to "Booked" (1) after payment
          const userId = sessionStorage.getItem("userId");
          if (!userId) {
            alert("User not logged in");
            return;
          }
  
          await axios.put(`http://localhost:5263/api/Booking/${userId}`, null, {
            params: {
              bookingId: this.bookingId,
              status: 1, // Booked
            },
          });
  
          alert("Payment successful! Booking confirmed.");
          this.$router.push("/my-bookings"); // Redirect to the bookings page
        } catch (err) {
          console.error("Error processing payment:", err);
          alert("Payment failed. Please try again.");
        }
      },
    },
  };
  </script>
  
  <style scoped>
  .payment-page {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    height: 100vh;
    text-align: center;
  }
  
  .pay-button {
    background-color: #4caf50;
    color: white;
    padding: 10px 20px;
    font-size: 16px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
  }
  
  .pay-button:hover {
    background-color: #45a049;
  }
  </style>
  