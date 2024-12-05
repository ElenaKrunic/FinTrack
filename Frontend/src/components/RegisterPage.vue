<template>
  <div class="flex justify-center items-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-full max-w-md">
      <h2 class="text-3xl font-extrabold mb-6 text-gray-900 text-center">
        Create Your Account
      </h2>
      <form @submit.prevent="registerUser">
        <div class="mb-6">
          <label for="username" class="block text-sm font-medium text-gray-700">Username:</label>
          <input 
            type="text" 
            v-model="username" 
            id="username" 
            class="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-teal-500"
            required
          />
        </div>

        <div class="mb-6">
          <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
          <input 
            type="email" 
            v-model="email" 
            id="email" 
            class="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-teal-500"
            required
          />
        </div>

        <div class="mb-6">
          <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
          <input 
            type="password" 
            v-model="password" 
            id="password" 
            class="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-teal-500"
            required
          />
        </div>

        <div class="text-center">
          <button 
            type="submit" 
            class="w-full py-3 bg-teal-500 text-white rounded-md hover:bg-teal-600 focus:outline-none focus:ring-2 focus:ring-teal-400 transition duration-300"
          >
            Register
          </button>
        </div>
      </form>

      <div class="text-center mt-4">
        <p class="text-gray-600">Already have an account?</p>
        <a @click="goToLogin" class="text-teal-500 hover:text-teal-700">Login now</a>
      </div>

      <div class="text-center mt-4">
        <a @click="goToHome" class="text-teal-500 hover:text-teal-700">Go to Home</a>
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
    goToLogin() {
      this.$router.push('/login');
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

<style scoped>
.bg-teal-500 {
  background-color: #38b2ac;
}
.bg-teal-600 {
  background-color: #319795;
}
.text-teal-500 {
  color: #38b2ac;
}
.text-teal-700 {
  color: #2c7a7b;
}
</style>
