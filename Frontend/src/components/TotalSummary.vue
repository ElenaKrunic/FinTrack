<template>
    <div class="text-center mb-6">
      <p class="text-lg text-gray-600">Total Income (all time): ${{ totalIncome }}</p>
      <p class="text-lg text-gray-600">Total Expenses (all time) : ${{ totalExpenses }}</p>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    data() {
      return {
        totalIncome: 0,
        totalExpenses: 0,
      };
    },
    created() {
      this.fetchTotals();
    },
    methods: {
      async fetchTotals() {
        try {
        const userId = localStorage.getItem("userId");
        const incomeResponse = await axios.get(`http://localhost:5194/api/transaction/totalIncome/${userId}`);
        const expensesResponse = await axios.get(`http://localhost:5194/api/transaction/totalExpenses/${userId}`);
        
        this.totalIncome = incomeResponse.data;
        this.totalExpenses = expensesResponse.data;
      } catch (error) {
        console.error("Error fetching totals:", error);
      }
      },
    },
  };
  </script>
  
  <style scoped>
  </style>
  