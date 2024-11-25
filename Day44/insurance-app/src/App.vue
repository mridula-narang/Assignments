<template>
  <!-- <LoginComponent/>
  <AddPolicyComponent/> -->
  <nav>
    <router-link to="/">Home</router-link>|
    <router-link to="/login">Login</router-link>|
    <router-link to="/policies">Policies</router-link>
  </nav>
  <router-view/>
  <HelloWorld/>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue';

// import LoginComponent from './components/LoginComponent.vue'
// import AddPolicyComponent from './components/AddPolicy.vue'

export default {
  name: 'App',
  components: {
    HelloWorld
    // LoginComponent,
    // AddPolicyComponent
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
