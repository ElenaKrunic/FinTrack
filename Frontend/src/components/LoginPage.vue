<template>
  <div class="flex items-center justify-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-96">
      <h2 class="text-2xl font-bold mb-4">Login</h2>
      <form @submit.prevent="loginUser">
        <div class="mb-4">
          <label for="email" class="block text-sm font-medium text-gray-700">Email:</label>
          <input type="email" v-model="email" id="email" required class="w-full p-2 border border-gray-300 rounded-md" />
        </div>
        <div class="mb-4">
          <label for="password" class="block text-sm font-medium text-gray-700">Password:</label>
          <input type="password" v-model="password" id="password" required class="w-full p-2 border border-gray-300 rounded-md" />
        </div>
        <button type="submit" class="w-full py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600">Login</button>
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
      email: "",
      password: "",
    };
  },
  methods: {
   goToHome() {
      this.$router.push("/");
    },
    async loginUser() {
      try {
        const response = await axios.post(
          "http://localhost:5194/api/auth/login",
          {
            email: this.email,
            password: this.password,
          }
        );
        localStorage.setItem("token", response.data.token);
        this.$router.push("/dashboard");
      } catch (error) {
        alert("Error logging in: " + error.response.data);
      }
    },
  },
};
</script>
