<template>
  <div class="flex flex-col items-center justify-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-full max-w-4xl">
      <h2 class="text-3xl font-extrabold mb-6 text-gray-900 text-center">
        Welcome to FinTrack
      </h2>
      <p class="mb-6 text-lg text-gray-600 text-center">
        Track your finances, manage your accounts, and monitor your progress easily.
      </p>

      <div class="flex flex-wrap justify-center gap-6">
        <div 
          class="bg-white p-6 rounded-lg shadow-md w-80 text-center hover:shadow-lg transition"
          @click="goToLogin"
        >
          <i class="fas fa-sign-in-alt text-4xl text-teal-500 mb-4"></i>
          <h3 class="text-xl font-bold mb-2">Login</h3>
          <p class="text-gray-600 mb-4">Access your account and manage your finances.</p>
          <button class="py-2 px-4 bg-teal-500 text-white rounded-md hover:bg-teal-600">
            Login
          </button>
        </div>
        <div 
          class="bg-white p-6 rounded-lg shadow-md w-80 text-center hover:shadow-lg transition"
          @click="goToRegister"
        >
          <i class="fas fa-user-plus text-4xl text-orange-500 mb-4"></i>
          <h3 class="text-xl font-bold mb-2">Register</h3>
          <p class="text-gray-600 mb-4">Sign up to start tracking your finances today.</p>
          <button class="py-2 px-4 bg-orange-500 text-white rounded-md hover:bg-orange-600">
            Register
          </button>
        </div>
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
    if (localStorage.getItem("token")) {
      this.isAuthenticated = true;
    }
  },
  methods: {
    goToLogin() {
      this.$router.push("/login");
    },
    goToRegister() {
      this.$router.push("/register");
    },
    goToDashboard() {
      this.$router.push("/dashboard");
    },
  },
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
