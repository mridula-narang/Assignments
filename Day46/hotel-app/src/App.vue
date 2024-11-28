<template>
  <!-- <img alt="Vue logo" src="./assets/logo.png"> -->
  <!-- <HelloWorld /> -->
   <nav>
    <router-link to="/">Home</router-link> 
    <!-- <router-link to="/login">Login</router-link> | -->
    <!-- <router-link to="/rooms">Rooms</router-link> | -->
    <!-- <router-link to="/hotels">Hotels</router-link> -->
    <!-- <router-link to="/register">Register</router-link> -->
   </nav>
  <router-view/>
</template>

<script>
// import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'App',
  components: {
    // HelloWorld
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
