<script setup>
import { login } from '@/scripts/LoginService';
import { useLoginStore } from '@/stores/loginStore';
import {ref} from 'vue';
import { useRouter } from 'vue-router';

const username = ref('');
const password = ref('');
const loginStore = useLoginStore();
const router = useRouter();

const logon = (event) => {
  event.preventDefault();
  login(username.value, password.value)
    .then(response => {
      sessionStorage.setItem('Token', response.data.token);
      sessionStorage.setItem('userId', response.data.userId);
      loginStore.login(response.data.username);
      router.push('/hotels');
    })
    .catch(error => {
      console.error('Login failed:', error);
      alert('Login failed. Please check your credentials and try again.');
    });
}
</script>
<template>
  <div class="login-page">
    <div class="login-form-container">
      <img src="@/assets/logo.jpg" alt="Tiare Logo" class="logo" />
      <h1>Welcome Back!</h1>
      <p class="subheading">Please login to continue your relaxing journey with Tiare.</p>
      <form @submit="logon" class="login-form">
        <div class="input-group">
          <label for="username">UserName</label>
          <input type="text" id="username" v-model="username" required />
        </div>
        <div class="input-group">
          <label for="password">Password</label>
          <input type="password" id="password" v-model="password" required />
        </div>
        <button type="submit" class="login-btn">Login</button>
      </form>
      <p class="register-link">
        Don't have an account? <router-link to="/register">Register here</router-link>
      </p>
    </div>
  </div>
</template>

<style scoped>
  .login-page {
    height: 100vh;
    background: url('../../assets/bg-login.jpeg')  no-repeat center center fixed;
    background-size: cover;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .login-form-container {
    background: rgba(255, 255, 255, 0.9);
    padding: 40px;
    border-radius: 15px;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
    text-align: center;
    width: 100%;
    max-width: 400px;
  }

  .logo {
    width: 100px;
    margin-bottom: 20px;
  }

  h1 {
    font-size: 2rem;
    color: #333;
    margin-bottom: 10px;
  }

  .subheading {
    color: #777;
    font-size: 1rem;
    margin-bottom: 30px;
  }

  .login-form {
    display: flex;
    flex-direction: column;
    gap: 20px;
  }

  .input-group {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
  }

  .input-group label {
    font-size: 1rem;
    color: #333;
    margin-bottom: 5px;
  }

  .input-group input {
    width: 100%;
    padding: 12px;
    border: 1px solid #ddd;
    border-radius: 8px;
    font-size: 1rem;
    outline: none;
    transition: border 0.3s;
  }

  .input-group input:focus {
    border-color: #ffa07a;
  }

  .login-btn {
    background-color: #ffa07a;
    color: white;
    padding: 15px;
    border: none;
    border-radius: 8px;
    font-size: 1.1rem;
    cursor: pointer;
    transition: background-color 0.3s;
  }

  .login-btn:hover {
    background-color: #ff7f50;
  }

  .register-link {
    margin-top: 15px;
    color: #333;
  }

  .register-link a {
    color: #ffa07a;
    text-decoration: none;
    font-weight: bold;
  }

  .register-link a:hover {
    text-decoration: underline;
  }
</style>
