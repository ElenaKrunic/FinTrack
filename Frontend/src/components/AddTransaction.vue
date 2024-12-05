<template>
    <div>
      <h3 class="text-xl font-bold mb-4">Add new transaction</h3>
      <form @submit.prevent="addTransaction">
        <div class="mb-4">
          <input 
            v-model="transaction.amount" 
            type="number" 
            class="p-2 border rounded w-full"
            placeholder="Amount"
            required
          />
        </div>
  
        <div class="mb-4">
          <input 
            v-model="transaction.description" 
            type="text" 
            class="p-2 border rounded w-full"
            placeholder="Description"
            required
          />
        </div>
  
        <div class="mb-4">
          <input 
            v-model="transaction.date" 
            type="date" 
            class="p-2 border rounded w-full"
            required
          />
        </div>
  
        <div class="mb-4">
          <select 
            v-model="transaction.categoryId" 
            class="p-2 border rounded w-full"
            required
          >
            <option v-for="category in categories" :key="category.id" :value="category.id">
              {{ category.name }}
            </option>
          </select>
        </div>
  
        <button 
          type="submit" 
          class="bg-teal-500 text-white p-2 rounded"
        >
          Add Transaction
        </button>
      </form>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  import { category_api, transaction_api } from "@/config/api";

  export default {
    data() {
      return {
        transaction: {
          amount: '',
          description: '',
          categoryId: '',
          date: ''
        },
        categories: [],
      };
    },
    created() {
      this.fetchCategories();
    },
    methods: {
      async fetchCategories() {
        try {
          const response = await axios.get(`${category_api}`);
          this.categories = response.data.$values;
        } catch (error) {
          console.error("Error fetching categories:", error);
        }
      },
      async addTransaction() {
        try {
          const userId = localStorage.getItem("userId");
          await axios.post(`${transaction_api}`, {
            ...this.transaction,
            userId,
          });
          alert('Transaction added successfully!');
        } catch (error) {
          console.error("Error adding transaction:", error);
        }
      },
    },
  };
  </script>
  
  <style scoped>
  </style>
  