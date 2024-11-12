<template>
    <div class="home">
      <h1>Welcome to the Home Page</h1>
      <router-link to="/login">Login</router-link> |
      <router-link to="/register">Register</router-link>
      <button @click="logout">Logout</button> 
    </div>
  </template>
  
  <script>
  import authService from '@/services/authService';
  
  export default {
    name: 'HomePage',
    methods: {
      logout() {
        authService.logout(); 
        this.$router.push('/login'); 
      }
    }
  };
  </script>
  
  <style scoped>
  .home {
    text-align: center;
    margin-top: 50px;
  }
  </style>
  