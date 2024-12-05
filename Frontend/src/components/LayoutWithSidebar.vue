<template>
    <div class="flex">
      <AppSidebar /> 
      <div class="flex-1 p-8">
        <router-view /> 
      </div>
    </div>
  </template>
  
  <script>
  import AppSidebar from "@/components/AppSidebar.vue"; 
  
  export default {
    name: "LayoutWithSidebar",
    components: {
        AppSidebar, 
    },
  };
  </script>
  