<template>
  <div class="flex items-center justify-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-full max-w-md">
      <h2 class="text-3xl font-extrabold mb-6 text-gray-900 text-center">
        Login to FinTrack
      </h2>
      <form @submit.prevent="loginUser">
        <div class="mb-6">
          <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
          <input type="email" v-model="email" id="email" required class="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-teal-500" />
        </div>
        <div class="mb-6">
          <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
          <input type="password" v-model="password" id="password" required class="w-full p-3 border border-gray-300 rounded-md focus:outline-none focus:ring-2 focus:ring-teal-500" />
        </div>
        <button type="submit" class="w-full py-3 bg-teal-500 text-white rounded-md hover:bg-teal-600 focus:outline-none focus:ring-2 focus:ring-teal-400">
          Login
        </button>
      </form>
      <div class="text-center mt-4">
        <p class="text-gray-600">Don't have an account?</p>
        <a @click="goToRegister" class="text-teal-500 hover:text-teal-700">Register now</a>
      </div>
      <div class="text-center mt-4">
        <a @click="goToHome" class="text-teal-500 hover:text-teal-700">Go to Home</a>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import { auth_api } from "@/config/api";

export default {
  data() {
    return {
      email: "",
      password: "",
    };
  },
  methods: {
    goToHome() {
      this.$router.push("/");
    },
    goToRegister() {
      this.$router.push("/register");
    },
    async loginUser() {
      try {
        const response = await axios.post(
          `${auth_api}/login`,
          {
            email: this.email,
            password: this.password,
          }
        );
        localStorage.setItem("token", response.data.token);
        localStorage.setItem("userId", response.data.userId);
        this.$router.push("/dashboard");
      } catch (error) {
        alert("Error logging in: " + error.response.data);
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
