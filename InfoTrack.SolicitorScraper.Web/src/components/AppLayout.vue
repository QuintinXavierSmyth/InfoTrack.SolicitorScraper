<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="min-h-screen bg-gray-100">
    <nav class="border-b bg-white shadow-sm">
      <div class="mx-auto flex max-w-7xl items-center justify-between px-6 py-4">
        <h1 class="text-xl font-bold">Solicitor Scraper</h1>

        <div class="flex gap-6">
          <RouterLink
            to="/"
            exact-active-class="font-semibold text-blue-600 after:absolute after:left-0 after:right-0 after:-bottom-2 after:h-0.5 after:bg-blue-600"
            class="relative text-gray-600 transition hover:text-blue-600"
          >
            Dashboard
          </RouterLink>

          <RouterLink
            to="/locations"
            class="relative text-gray-600 transition hover:text-blue-600"
            active-class="font-semibold text-blue-600 after:absolute after:left-0 after:right-0 after:-bottom-2 after:h-0.5 after:bg-blue-600"
          >
            Locations
          </RouterLink>

          <RouterLink
            to="/solicitors"
            class="relative text-gray-600 transition hover:text-blue-600"
            active-class="font-semibold text-blue-600 after:absolute after:left-0 after:right-0 after:-bottom-2 after:h-0.5 after:bg-blue-600"
          >
            Solicitors
          </RouterLink>

          <RouterLink
            to="/reports"
            class="relative text-gray-600 transition hover:text-blue-600"
            active-class="font-semibold text-blue-600 after:absolute after:left-0 after:right-0 after:-bottom-2 after:h-0.5 after:bg-blue-600"
          >
            Reports
          </RouterLink>
        </div>
      </div>
    </nav>

    <main class="mx-auto max-w-7xl p-6">
      <RouterView />
    </main>
  </div>
</template>
