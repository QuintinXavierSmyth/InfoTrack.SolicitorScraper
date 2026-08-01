<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { getSolicitors } from '../api/solicitorApi'
import type { Solicitor } from '../types/Solicitor'

const solicitors = ref<Solicitor[]>([])

const searchTerm = ref('')
const selectedLocation = ref('All')
const verifiedOnly = ref(false)

onMounted(async () => {
  solicitors.value = await getSolicitors()
})

const locations = computed(() => {
  return ['All', ...new Set(solicitors.value.map((x) => x.location))]
})

const filteredSolicitors = computed(() => {
  return solicitors.value.filter((solicitor) => {
    const search = searchTerm.value.toLowerCase()

    const matchesSearch =
      solicitor.name.toLowerCase().includes(search) ||
      solicitor.location.toLowerCase().includes(search)

    const matchesLocation =
      selectedLocation.value === 'All' || solicitor.location === selectedLocation.value

    const matchesVerified = !verifiedOnly.value || solicitor.isVerified

    return matchesSearch && matchesLocation && matchesVerified
  })
})

function clearFilters() {
  searchTerm.value = ''
  selectedLocation.value = 'All'
  verifiedOnly.value = false
}
</script>

<template>
  <div class="p-6">
    <div class="mb-6">
      <h1 class="mb-4 text-3xl font-bold">Solicitors</h1>

      <div class="flex flex-wrap gap-4">
        <input
          v-model="searchTerm"
          placeholder="Search name or location..."
          class="rounded border p-2"
        />

        <select v-model="selectedLocation" class="rounded border p-2">
          <option v-for="location in locations" :key="location">
            {{ location }}
          </option>
        </select>

        <label class="flex items-center gap-2">
          <input v-model="verifiedOnly" type="checkbox" />

          Verified only
        </label>

        <button @click="clearFilters()" class="rounded border px-3 py-2">Clear Filters</button>
      </div>
    </div>

    <p class="mb-3 text-sm text-gray-600">
      Showing {{ filteredSolicitors.length }} of {{ solicitors.length }} solicitors
    </p>

    <div class="overflow-x-auto rounded-lg border shadow">
      <table class="w-full text-sm">
        <thead class="bg-gray-100">
          <tr>
            <th class="border-b p-3 text-left">Name</th>

            <th class="border-b p-3 text-left">Location</th>

            <th class="border-b p-3 text-left">Rating</th>

            <th class="border-b p-3 text-left">Reviews</th>

            <th class="border-b p-3 text-left">Verified</th>

            <th class="border-b p-3 text-left">Phone</th>

            <th class="border-b p-3 text-left">Address</th>

            <th class="border-b p-3 text-left">Website</th>

            <th class="border-b p-3 text-left">View More</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="solicitor in filteredSolicitors" :key="solicitor.id" class="hover:bg-gray-50">
            <td class="border-b p-3 font-medium">
              {{ solicitor.name }}
            </td>

            <td class="border-b p-3">
              {{ solicitor.location }}
            </td>

            <td class="border-b p-3">⭐ {{ solicitor.rating }}</td>

            <td class="border-b p-3">
              {{ solicitor.reviewCount }}
            </td>

            <td class="border-b p-3">
              <span
                v-if="solicitor.isVerified"
                class="rounded bg-green-100 px-2 py-1 text-green-700"
              >
                Verified
              </span>

              <span v-else class="rounded bg-gray-100 px-2 py-1 text-gray-600"> No </span>
            </td>

            <td class="border-b p-3">
              {{ solicitor.phoneNumber }}
            </td>

            <td class="border-b p-3">
              {{ solicitor.address }}
            </td>

            <td class="border-b p-3">
              <a :href="solicitor.websiteUrl" target="_blank" class="text-blue-600 hover:underline">
                Website
              </a>
            </td>

            <td class="border-b p-3">
              <a
                :href="solicitor.viewMoreUrl"
                target="_blank"
                class="text-blue-600 hover:underline"
              >
                View
              </a>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
