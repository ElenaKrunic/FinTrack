<template>
  <div class="flex flex-col items-center justify-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-96 text-center">
      <h2 class="text-3xl font-extrabold mb-6 text-gray-900">Welcome to FinTrack</h2>  

      <p class="mb-6 text-lg text-gray-600">
        Track your finances, manage your accounts, and monitor your progress easily.
      </p>

      <div v-if="!isAuthenticated">
        <button @click="goToLogin" class="w-full py-2 bg-teal-500 text-white rounded-md hover:bg-teal-600 mb-4">
          Login
        </button>
        <button @click="goToRegister" class="w-full py-2 bg-orange-500 text-white rounded-md hover:bg-orange-600">
          Register
        </button>
      </div>

      <div v-else>
        <p class="mb-4 text-green-500">You are logged in!</p>
        <button @click="goToDashboard" class="w-full py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600">
          Go to Dashboard
        </button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isAuthenticated: false, 
    };
  },
  created() {
    if (localStorage.getItem('token')) {
      this.isAuthenticated = true;
    }
  },
  methods: {
    goToLogin() {
      this.$router.push('/login'); 
    },
    goToRegister() {
      this.$router.push('/register'); 
    },
    goToDashboard() {
      this.$router.push('/dashboard');  
    }
  }
};
</script>

<style scoped>
.bg-teal-500 {
  background-color: #38b2ac; 
}

.bg-orange-500 {
  background-color: #ed8936; 
}

.text-green-500 {
  color: #48bb78; 
}
</style>
