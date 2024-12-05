<template>
  <div>
    <section class="bg-gray-200 p-8 my-8 rounded-lg">
      <h2 class="text-2xl font-semibold text-center">How FinTrack Helps You</h2>
      <p class="mt-4 text-center text-lg">
        FinTrack helps you manage your finances with ease. Track spending, set goals, and analyze your financial health.
      </p>
    </section>

    <section class="flex justify-around p-8 my-8 bg-white rounded-lg shadow-md">
      <div class="text-center">
        <i class="fas fa-chart-line text-4xl text-teal-500 mb-4"></i>
        <h3 class="font-bold">Financial Analytics</h3>
        <p>Monitor your spending and savings.</p>
      </div>
      <div class="text-center">
        <i class="fas fa-wallet text-4xl text-orange-500 mb-4"></i>
        <h3 class="font-bold">Account Management</h3>
        <p>Organize and manage your financial accounts.</p>
      </div>
    </section>

    <div class="text-center my-8">
      <button @click="goToAuthPage" class="py-3 px-6 bg-teal-500 text-white rounded-lg hover:bg-teal-600">
        Start Tracking Your Finances
      </button>
    </div>

    <div class="flex flex-wrap justify-center gap-6" v-if="showAuthPage">
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
</template>

<script>
export default {
  data() {
    return {
      showAuthPage: false, 
    };
  },
  created() {
    if (localStorage.getItem("token")) {
      this.showAuthPage = false; 
    } else {
      this.showAuthPage = true; 
    }
  },
  methods: {
    goToAuthPage() {
      this.showAuthPage = true; 
    },
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
