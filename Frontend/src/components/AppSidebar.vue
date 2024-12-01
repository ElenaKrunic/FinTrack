<template>
    <div class="flex">
      <div class="w-64 h-screen bg-teal-500 text-white p-4">
        <h2 class="text-2xl font-bold mb-6">Overview </h2>
        <ul class="space-y-4">
          <li>
            <router-link to="/" class="flex items-center space-x-2 hover:bg-teal-600 p-2 rounded">
              <span class="text-xl">🏠</span>
              <span>Home</span>
            </router-link>
          </li>
          <li>
            <router-link to="/transactions" class="flex items-center space-x-2 hover:bg-teal-600 p-2 rounded">
              <span class="text-xl">💰</span>
              <span>Transactions</span>
            </router-link>
          </li>
          <li>
            <router-link to="/categories" class="flex items-center space-x-2 hover:bg-teal-600 p-2 rounded">
              <span class="text-xl">📊</span>
              <span>Categories</span>
            </router-link>
          </li>
          <li>
            <router-link to="/analytics" class="flex items-center space-x-2 hover:bg-teal-600 p-2 rounded">
              <span class="text-xl">📈</span>
              <span>Analytics</span>
            </router-link>
          </li>
          <li>
            <router-link to="/settings" class="flex items-center space-x-2 hover:bg-teal-600 p-2 rounded">
              <span class="text-xl">⚙️</span>
              <span>Settings</span>
            </router-link>
          </li>
        </ul>
      </div>
  
      <div class="flex-1 p-8 bg-gray-50">
      </div>
    </div>
  </template>
  
  <script>
  export default {
    name: "AppSidebar",
  };
  </script>
  
  <style scoped>
  </style>
  