<template>
    <div class="flex">
      <AppSidebar />
      <div class="flex-1 p-8 bg-gray-50">
        <h2 class="text-2xl font-bold mb-6">Categories</h2>
        
        <button @click="showCreateModal = true" class="bg-teal-500 text-white py-2 px-4 rounded hover:bg-teal-600 mb-4">Add Category</button>
        
        <div v-if="categories.length > 0" class="space-y-4">
          <div v-for="category in categories" :key="category.id" class="flex justify-between items-center bg-white p-4 rounded shadow-sm">
            <div>
              <h3 class="font-semibold text-lg">{{ category.name }}</h3>
            </div>
            <div class="flex space-x-2">
              <button @click="editCategory(category)" class="text-teal-500 hover:text-teal-700">Edit</button>
              <button @click="deleteCategory(category.id)" class="text-red-500 hover:text-red-700">Delete</button>
            </div>
          </div>
        </div>
        
        <div v-else class="text-gray-500">No categories found.</div>
  
        <div v-if="showCreateModal" class="fixed inset-0 bg-gray-700 bg-opacity-50 flex justify-center items-center">
          <div class="bg-white p-6 rounded-lg shadow-lg w-96">
            <h3 class="text-xl font-semibold mb-4">{{ editMode ? 'Edit' : 'Add' }} Category</h3>
            <form @submit.prevent="submitCategory">
              <div class="mb-4">
                <label for="categoryName" class="block text-sm font-medium text-gray-700">Category Name:</label>
                <input 
                  v-model="categoryForm.name" 
                  id="categoryName" 
                  type="text" 
                  class="w-full p-2 border border-gray-300 rounded-md" 
                  required
                />
              </div>
              <div class="flex justify-end">
                <button type="button" @click="showCreateModal = false" class="mr-2 bg-gray-300 text-black py-2 px-4 rounded">Cancel</button>
                <button type="submit" class="bg-teal-500 text-white py-2 px-4 rounded">Save</button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  import AppSidebar from "../components/AppSidebar.vue";
  import { category_api } from "@/config/api";
  
  export default {
    name: "CategoriesPage",
    components: {
      AppSidebar
    },
    data() {
      return {
        categories: [],
        showCreateModal: false,
        categoryForm: {
          name: ""
        },
        editMode: false,
        currentCategoryId: null
      };
    },
    methods: {
      async fetchCategories() {
        try {
          const response = await axios.get(category_api);
          this.categories = response.data.$values;
        } catch (error) {
          console.error("Error fetching categories", error);
        }
      },
      async submitCategory() {
        try {
          if (this.editMode) {
            await axios.put(`${category_api}/${this.currentCategoryId}`, this.categoryForm);
          } else {
            await axios.post(`${category_api}`, this.categoryForm);
          }
          this.showCreateModal = false;
          this.categoryForm.name = "";
          this.fetchCategories();
        } catch (error) {
          console.error("Error saving category", error);
        }
      },
      editCategory(category) {
        this.categoryForm.name = category.name;
        this.currentCategoryId = category.id;
        this.editMode = true;
        this.showCreateModal = true;
      },
      async deleteCategory(id) {
        try {
          await axios.delete(`${category_api}/${id}`);
          this.fetchCategories();
        } catch (error) {
          console.error("Error deleting category", error);
        }
      }
    },
    mounted() {
      this.fetchCategories();
    }
  };
  </script>
  
  <style scoped>
  </style>
  