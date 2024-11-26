<template>
  <div class="flex justify-center items-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-96">
      <h2 class="text-2xl font-bold text-center mb-6">Register</h2>
      <form @submit.prevent="registerUser">
        <div class="mb-4">
          <label for="username" class="block text-sm font-medium text-gray-700">Username:</label>
          <input 
            type="text" 
            v-model="username" 
            id="username" 
            class="mt-2 p-2 w-full border border-gray-300 rounded-lg" 
            required 
          />
        </div>

        <div class="mb-4">
          <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
          <input 
            type="email" 
            v-model="email" 
            id="email" 
            class="mt-2 p-2 w-full border border-gray-300 rounded-lg" 
            required 
          />
        </div>

        <div class="mb-4">
          <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
          <input 
            type="password" 
            v-model="password" 
            id="password" 
            class="mt-2 p-2 w-full border border-gray-300 rounded-lg" 
            required 
          />
        </div>

        <div class="text-center">
          <button 
            type="submit" 
            class="w-full bg-blue-500 text-white py-2 rounded-lg hover:bg-blue-600 transition duration-300"
          >
            Register
          </button>
        </div>
      </form>
      <div class="text-center mt-4">
        <a @click="goToHome" class="text-blue-500 hover:text-blue-700"> Go to Home </a>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      username: "",
      email: "",
      password: "",
    };
  },
  methods: {
    goToHome() {
      this.$router.push('/');
    },
    async registerUser() {
      try {
        const response = await axios.post(
          "http://localhost:5194/api/auth/register",
          {
            username: this.username,
            email: this.email,
            password: this.password,
          }
        );
        this.$router.push("/dashboard");
        console.log(response);
      } catch (error) {
        alert("Error registering user: " + error.response.data);
      }
    },
  },
};
</script>
