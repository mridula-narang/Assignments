<script>
import axios from "axios";

export default {
  name: "PaymentPageComponent",
  data() {
    return {
      paymentType: "", // Credit or Debit
      cardDetails: {
        cardNumber: "",
        expiryDate: "",
        cvv: "",
        nameOnCard: "",
      },
      bookingId: null,
      userId: null,
    };
  },
  created() {
    // Get bookingId and userId from query parameters or session storage
    this.bookingId = this.$route.query.bookingId || null;
    this.userId = sessionStorage.getItem("userId");
    if (!this.bookingId || !this.userId) {
      alert("Booking or user information missing.");
      this.$router.push("/bookings"); // Redirect back if invalid access
    }
  },
  methods: {
    async handlePayment() {
      if (!this.paymentType || !this.isFormValid()) {
        alert("Please complete all fields correctly.");
        return;
      }

      try {
        // Update booking status in the backend
        await axios.put(`http://localhost:5263/api/Booking/${this.userId}`, null, {
          params: {
            bookingId: this.bookingId,
            status: 1, // Booking confirmed
          },
        });

        // Simulate sending confirmation email
        alert("Payment successful! Booking confirmed. Confirmation email sent.");

        // Redirect to bookings page
        this.$router.push("/bookings");
      } catch (error) {
        console.error("Error processing payment:", error);
        alert("Payment failed. Please try again.");
      }
    },
    isFormValid() {
      const { cardNumber, expiryDate, cvv, nameOnCard } = this.cardDetails;
      return (
        /^\d{16}$/.test(cardNumber.replace(/\s/g, "")) && // 16-digit card number
        /^\d{2}\/\d{2}$/.test(expiryDate) && // MM/YY format
        /^\d{3}$/.test(cvv) && // 3-digit CVV
        nameOnCard.trim().length > 0
      );
    },
  },
};
</script>

<template>
  <div class="payment-page">
    <!-- Logo -->
    <div class="logo-container">
      <img src="/path-to-logo/logo.jpg" alt="Tiare Logo" class="logo" />
    </div>

    <h1>Payment Details</h1>
    <p class="subtitle">
      Secure your booking with ease. Enter your payment details below.
    </p>

    <form @submit.prevent="handlePayment">
      <!-- Payment Type -->
      <div class="form-group">
        <label for="paymentType">Payment Type:</label>
        <select v-model="paymentType" id="paymentType" required>
          <option value="" disabled>Select Payment Type</option>
          <option value="Credit">Credit Card</option>
          <option value="Debit">Debit Card</option>
        </select>
      </div>

      <!-- Card Number -->
      <div class="form-group">
        <label for="cardNumber">Card Number:</label>
        <div class="input-icon">
          <input
            type="text"
            id="cardNumber"
            v-model="cardDetails.cardNumber"
            maxlength="19"
            placeholder="XXXX XXXX XXXX XXXX"
            @input="cardDetails.cardNumber = cardDetails.cardNumber.replace(/[^0-9 ]/g, '')"
            required
          />
          <img src="/path-to-icons/credit-card-icon.svg" alt="Card Icon" />
        </div>
      </div>

      <!-- Expiry Date -->
      <div class="form-group">
        <label for="expiryDate">Expiry Date:</label>
        <input
          type="text"
          id="expiryDate"
          v-model="cardDetails.expiryDate"
          maxlength="5"
          placeholder="MM/YY"
          @input="cardDetails.expiryDate = cardDetails.expiryDate.replace(/[^0-9/]/g, '')"
          required
        />
      </div>

      <!-- CVV -->
      <div class="form-group">
        <label for="cvv">CVV:</label>
        <input
          type="password"
          id="cvv"
          v-model="cardDetails.cvv"
          maxlength="3"
          placeholder="XXX"
          @input="cardDetails.cvv = cardDetails.cvv.replace(/[^0-9]/g, '')"
          required
        />
      </div>

      <!-- Name on Card -->
      <div class="form-group">
        <label for="nameOnCard">Name on Card:</label>
        <input
          type="text"
          id="nameOnCard"
          v-model="cardDetails.nameOnCard"
          placeholder="Enter Name"
          required
        />
      </div>

      <!-- Pay Button -->
      <button type="submit" class="pay-button">Pay Now</button>
    </form>
  </div>
</template>

<style scoped>
/* General Page Styles */
.payment-page {
  max-width: 500px;
  margin: 50px auto;
  padding: 20px;
  background: linear-gradient(to bottom right, #fef6e4, #f2e5d5);
  border-radius: 12px;
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
  font-family: "Arial", sans-serif;
  color: #444;
  text-align: center;
}

.logo-container {
  text-align: center;
  margin-bottom: 20px;
}

.logo {
  max-width: 100px;
  border-radius: 50%;
}

h1 {
  font-size: 1.8rem;
  margin-bottom: 10px;
  color: #333;
}

.subtitle {
  font-size: 1rem;
  margin-bottom: 20px;
  color: #666;
}

/* Form Styles */
.form-group {
  margin-bottom: 15px;
  text-align: left;
}

label {
  font-weight: bold;
  margin-bottom: 5px;
  display: block;
}

input,
select {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 1rem;
  box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.05);
}

input:focus,
select:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
}

.input-icon {
  position: relative;
}

.input-icon img {
  position: absolute;
  top: 50%;
  right: 12px;
  transform: translateY(-50%);
  width: 24px;
  height: 24px;
}

/* Button Styles */
.pay-button {
  display: block;
  width: 100%;
  padding: 12px;
  font-size: 1rem;
  color: white;
  background-color: #28a745;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: background-color 0.3s ease;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
}

.pay-button:hover {
  background-color: #218838;
}

.pay-button:active {
  transform: scale(0.98);
}
</style>

