<script setup>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { RegisterUser } from '@/scripts/RegisterService';

const user = ref({
  username: '',
  password: ''
});
const errorMessage = ref('');
const router = useRouter();

const register = async () => {
  errorMessage.value = '';

  // Frontend validation
  if (user.value.username.length < 4) {
    errorMessage.value = 'Username must be at least 4 characters long.';
    return;
  }
  if (user.value.password.length < 6) {
    errorMessage.value = 'Password must be at least 6 characters long.';
    return;
  }

  try {
    const response = await RegisterUser(user.value);
    console.log(response.data.username + ' registered successfully.');
    router.push('/login');
  } catch (error) {
    console.error('Registration failed:', error);
    errorMessage.value = 'Registration failed. Please try again.';
  }
};
</script>

<template>
  <div>
    <h1>Register</h1>
    <div class="formdiv">
      <form @submit.prevent="register">
        <div>
          <label class="form-control" for="username">Username</label>
          <input class="form-control" type="text" id="username" v-model="user.username">
        </div>
        <div>
          <label class="form-control" for="password">Password</label>
          <input class="form-control" type="password" id="password" v-model="user.password">
        </div>
        <button class="btn btn-success" type="submit">Register</button>
      </form>
      <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    </div>
  </div>
</template>

<style scoped>
.formdiv {
  width: 30%;
  position: relative;
  left: 35%;
}
.error {
  color: red;
}
</style>