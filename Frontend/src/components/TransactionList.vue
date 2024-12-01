<template>
    <div>
      <h3 class="text-xl font-bold mb-4">Filter transactions</h3>
      <div class="flex gap-4 mb-4">
        <input 
          v-model="filters.startDate" 
          type="date" 
          class="p-2 border rounded"
        />
        <input 
          v-model="filters.endDate" 
          type="date" 
          class="p-2 border rounded"
        />
        <button @click="fetchTransactions" class="bg-teal-500 text-white p-2 rounded">
          Filter
        </button>
      </div>
  
      <ul>
        <li 
          v-for="transaction in transactions" 
          :key="transaction.id" 
          class="bg-gray-100 p-4 mb-4 rounded shadow"
        >
          <p>{{ transaction.description }} - ${{ transaction.amount }}</p>
        </li>
      </ul>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    data() {
      return {
        transactions: [],
        filters: {
          startDate: '',
          endDate: '',
        },
      };
    },
    methods: {
      async fetchTransactions() {
        try {
          const userId = localStorage.getItem("userId");
          const { startDate, endDate } = this.filters;
          const response = await axios.get('http://localhost:5194/api/transaction/filter', {
            params: {
              userId,
              startDate,
              endDate,
            },
          });
          this.transactions = response.data.$values;
        } catch (error) {
          console.error("Error fetching transactions:", error);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  </style>
  