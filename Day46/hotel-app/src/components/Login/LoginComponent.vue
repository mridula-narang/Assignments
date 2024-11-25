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
      sessionStorage.setItem('token', response.data.token);
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
  <div>
    <h1>Login</h1>
    <div class="formdiv">
      <form @submit="logon">
      <div>
        <label class="form-control" for="username">UserName</label>
        <input class="form-control" type="text" id="username" v-model="username">
      </div>
      <div>
        <label class="form-control" for="password">Password</label>
        <input class="form-control" type="password" id="password" v-model="password">
      </div>
      <button class="btn btn-success" type="submit">Login</button>
    </form>
    </div>
  </div>
</template>
<style scoped>
  .formdiv{
    width: 30%;
    position: relative;
    left: 35%;
  }
  </style>