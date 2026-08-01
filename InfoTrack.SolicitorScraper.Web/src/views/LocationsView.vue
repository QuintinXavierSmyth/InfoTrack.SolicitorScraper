<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getLocations, updateLocationStatus, addLocation } from '../api/locationApi'
import type { SearchLocation } from '../types/SearchLocation'

const locations = ref<SearchLocation[]>([])
const showAddModal = ref(false)

const newLocationName = ref('')
const newLocationSlug = ref('')
const errorMessage = ref('')

onMounted(async () => {
  await reloadLocations()
})

async function toggleLocation(location: SearchLocation) {
  const newStatus = !location.isEnabled

  await updateLocationStatus(location.id, newStatus)

  location.isEnabled = newStatus
}

async function reloadLocations() {
  locations.value = await getLocations()
}

async function createLocation() {
  try {
    await addLocation(newLocationName.value, newLocationSlug.value)

    showAddModal.value = false

    newLocationName.value = ''
    newLocationSlug.value = ''

    await reloadLocations()
  } catch {
    errorMessage.value = 'Unable to add location'
  }
}
</script>

<template>
  <div class="p-6">
    <div class="mb-6 flex items-center justify-between">
      <h1 class="text-3xl font-bold">Locations</h1>

      <button
        @click="showAddModal = true"
        class="rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700"
      >
        Add Location
      </button>
    </div>

    <p v-if="errorMessage" class="mb-3 text-red-600">
      {{ errorMessage }}
    </p>

    <div class="overflow-x-auto rounded-lg border shadow">
      <table class="w-full text-sm">
        <thead class="bg-gray-100">
          <tr>
            <th class="border-b p-3 text-left">Name</th>

            <th class="border-b p-3 text-left">Enabled</th>

            <th class="border-b p-3 text-left">Last Scraped</th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="location in locations" :key="location.id" class="hover:bg-gray-50">
            <td class="border-b p-3 font-medium">
              {{ location.name }}
            </td>

            <td class="border-b p-3">
              <button
                @click="toggleLocation(location)"
                class="rounded px-2 py-1"
                :class="
                  location.isEnabled ? 'bg-green-100 text-green-700' : 'bg-gray-100 text-gray-600'
                "
              >
                {{ location.isEnabled ? 'Enabled' : 'Disabled' }}
              </button>
            </td>

            <td class="border-b p-3">
              <span
                v-if="location.lastScraped"
                class="rounded bg-green-100 px-2 py-1 text-green-700"
              >
                {{ new Date(location.lastScraped).toLocaleString() }}
              </span>

              <span v-else class="rounded bg-yellow-100 px-2 py-1 text-yellow-700"> Never </span>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>

  <div v-if="showAddModal" class="fixed inset-0 flex items-center justify-center bg-black/30">
    <div class="w-96 rounded-lg bg-white p-6 shadow-lg">
      <h2 class="mb-4 text-xl font-bold">Add Location</h2>

      <label class="mb-2 block text-sm"> Name </label>

      <input
        v-model="newLocationName"
        class="mb-4 w-full rounded border p-2"
        placeholder="London"
      />

      <label class="mb-2 block text-sm"> URL Slug </label>

      <input
        v-model="newLocationSlug"
        class="mb-4 w-full rounded border p-2"
        placeholder="london"
      />

      <div class="flex justify-end gap-3">
        <button @click="showAddModal = false" class="rounded border px-4 py-2">Cancel</button>

        <button
          @click="createLocation"
          :disabled="!newLocationName || !newLocationSlug"
          class="rounded bg-blue-600 px-4 py-2 text-white"
        >
          Save
        </button>
      </div>
    </div>
  </div>
</template>
