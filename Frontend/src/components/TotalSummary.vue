<template>
    <div class="text-center mb-6">
      <p class="text-lg text-gray-600">Total Income (all time): ${{ totalIncome }}</p>
      <p class="text-lg text-gray-600">Total Expenses (all time) : ${{ totalExpenses }}</p>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  import { transaction_api } from "@/config/api";
  
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
        const incomeResponse = await axios.get(`${transaction_api}/totalIncome/${userId}`);
        const expensesResponse = await axios.get(`${transaction_api}/totalExpenses/${userId}`);
        
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
  