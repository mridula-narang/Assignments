<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { RegisterUser } from '@/scripts/RegisterService';

const user = ref({
  username: '',
  password: '',
  email: '',
  phoneNumber: '',
  role: 1 // Default role is customer
});
const errorMessage = ref('');
const router = useRouter();

const register = async () => {
  errorMessage.value = '';

  // Frontend validation
  if (user.value.username.length < 4) {
    errorMessage.value = 'Username must be at least 4 characters long.';
    alert(errorMessage.value);
    return;
  }
  if (user.value.password.length < 6) {
    errorMessage.value = 'Password must be at least 6 characters long.';
    alert(errorMessage.value);
    return;
  }

  try {
    const response = await RegisterUser(user.value);
    console.log(response.data.username + ' registered successfully.');
    router.push('/login');
  } catch (error) {
    console.error('Registration failed:', error);
    errorMessage.value = 'Registration failed. Please try again.';
    alert(errorMessage.value);
  }
};
</script>

<template>
  <div class="registration-page">
    <div class="form-overlay">
      <div class="logo-container">
        <img src="@/assets/logo.jpg" alt="Tiare Logo" />
      </div>
      <h1>Join Us</h1>
      <p>Create your account and start your relaxing journey with Tiare.</p>
      <form class="register-form" @submit.prevent="register">
        <div class="form-group">
          <label for="username">Username</label>
          <input
            type="text"
            id="username"
            v-model="user.username"
            placeholder="Choose a username"
          />
        </div>
        <div class="form-group">
          <label for="password">Password</label>
          <input
            type="password"
            id="password"
            v-model="user.password"
            placeholder="Create a password"
          />
        </div>
        <div class="form-group">
          <label for="email">Email</label>
          <input
            type="email"
            id="email"
            v-model="user.email"
            placeholder="Your email address"
          />
        </div>
        <div class="form-group">
          <label for="phoneNumber">Phone Number</label>
          <input
            type="text"
            id="phoneNumber"
            v-model="user.phoneNumber"
            placeholder="Your phone number"
          />
        </div>
        <button type="submit" class="btn-register">Register</button>
        <p class="login-link">
          Already have an account? <router-link to="/login">Login here</router-link>.
        </p>
      </form>
      <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
    </div>
  </div>
</template>

<style scoped>
.registration-page {
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  background: url('../../assets/bg-register.jpeg') center/cover no-repeat;
  position: relative;
}

.form-overlay {
  background: rgba(255, 255, 255, 0.85);
  padding: 2rem;
  border-radius: 12px;
  box-shadow: 0px 8px 20px rgba(0, 0, 0, 0.2);
  text-align: center;
  max-width: 400px;
  width: 100%;
}

.logo-container img {
  width: 80px;
  margin-bottom: 1rem;
}

h1 {
  font-size: 2rem;
  color: #5a3931;
  margin-bottom: 0.5rem;
}

p {
  font-size: 1rem;
  color: #5a3931;
  margin-bottom: 1.5rem;
}

.register-form {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.form-group {
  text-align: left;
}

label {
  font-size: 0.9rem;
  color: #5a3931;
}

input {
  width: 100%;
  padding: 0.7rem;
  border: 1px solid #e0c3a7;
  border-radius: 8px;
}

input:focus {
  outline: none;
  border-color: #d27d4d;
}

.btn-register {
  width: 100%;
  background-color: #d27d4d;
  color: white;
  padding: 0.7rem;
  border: none;
  border-radius: 8px;
  font-size: 1rem;
  cursor: pointer;
}

.btn-register:hover {
  background-color: #b7683b;
}

.login-link {
  margin-top: 1rem;
}

.login-link a {
  color: #d27d4d;
  text-decoration: none;
}

.login-link a:hover {
  text-decoration: underline;
}

.error-message {
  color: #d9534f;
  margin-top: 1rem;
}
</style>
