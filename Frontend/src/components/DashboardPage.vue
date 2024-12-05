<template>
  <div class="flex flex-col items-center justify-center h-screen bg-gray-100">
    <div class="bg-white p-8 rounded-lg shadow-lg w-full max-w-4xl">
      <h2 class="text-3xl font-extrabold mb-6 text-gray-900 text-center">
        Welcome to your dashboard
      </h2>

      <TotalSummary />
      <TransactionList />
      <AddTransaction />
    </div>
  </div>
</template>

<script>
import TotalSummary from "@/components/TotalSummary";
import TransactionList from "@/components/TransactionList";
import AddTransaction from "@/components/AddTransaction";

export default {
  components: {
    TotalSummary,
    TransactionList,
    AddTransaction,
  },
};
</script>

<style scoped>
</style>
