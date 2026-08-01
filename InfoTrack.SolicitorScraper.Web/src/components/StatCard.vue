<script setup lang="ts">
defineProps<{
  title: string
  value: string | number
}>()
</script>

<template>
  <div class="card">
    <h3>{{ title }}</h3>
    <p>{{ value }}</p>
  </div>
</template>

<style scoped>
.card {
  border: 1px solid #ddd;
  padding: 20px;
  border-radius: 8px;
  min-width: 180px;
}

h3 {
  margin: 0;
  font-size: 16px;
}

p {
  margin-top: 10px;
  font-size: 28px;
  font-weight: bold;
}
</style>
