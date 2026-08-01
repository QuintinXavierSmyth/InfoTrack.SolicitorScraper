<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { getReport } from '../api/reportApi'
import { runScrape } from '../api/scraperApi'
import type { Report } from '../types/Report'

const report = ref<Report | null>(null)

const scraping = ref(false)

const scrapeMessage = ref('')
const scrapeError = ref(false)
const ONE_DAY_MS = 24 * 60 * 60 * 1000 //24 hours in milliseconds

async function loadReport() {
  report.value = await getReport()
}

async function startScrape() {
  scraping.value = true
  scrapeMessage.value = ''
  scrapeError.value = false

  try {
    const result = await runScrape()

    scrapeMessage.value = `Scrape completed successfully. ${result.count} solicitors loaded.`

    await loadReport()
  } catch {
    scrapeMessage.value = 'Scrape failed. Please try again.'

    scrapeError.value = true
  } finally {
    scraping.value = false
  }
}

const isDataFresh = computed(() => {
  if (!report.value?.lastScraped) {
    return false
  }

  const lastScraped = new Date(report.value.lastScraped).getTime()

  const now = Date.now()

  const difference = now - lastScraped

  return difference < ONE_DAY_MS
})

onMounted(async () => {
  try {
    await loadReport()
  } catch {
    scrapeMessage.value = 'Unable to load dashboard data'
    scrapeError.value = true
  }
})
</script>

<template>
  <div class="p-6">
    <div class="mb-6 flex items-center justify-between">
      <h1 class="text-3xl font-bold">Dashboard</h1>

      <button
        @click="startScrape"
        :disabled="scraping"
        class="rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700 disabled:opacity-50"
      >
        {{ scraping ? 'Scraping...' : 'Run Scrape' }}
      </button>
    </div>

    <p
      v-if="scrapeMessage"
      class="mb-6 rounded border bg-white p-3"
      :class="scrapeError ? 'text-red-600' : 'text-green-600'"
    >
      {{ scrapeMessage }}
    </p>

    <div
      v-if="report && report.outOfSyncLocations > 0"
      class="mt-6 mb-6 rounded-lg border bg-yellow-100 p-4 text-yellow-800"
    >
      <strong>Data Notice:</strong>
      {{ report.outOfSyncLocations }}
      location(s) have stored solicitor data but are currently disabled.
    </div>

    <div v-if="report" class="grid grid-cols-1 gap-4 md:grid-cols-6">
      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Total Solicitors</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.totalSolicitors }}
        </p>
      </div>

      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Total Locations</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.totalLocations }}
        </p>
      </div>

      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Enabled Locations</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.enabledLocations }}
        </p>
      </div>

      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Scraped Locations</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.scrapedLocations }}
        </p>
      </div>

      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Verified Solicitors</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.verifiedSolicitors }}
        </p>
      </div>

      <div class="rounded-lg border bg-white p-5 shadow">
        <h2 class="text-sm text-gray-500">Average Rating</h2>

        <p class="mt-2 text-3xl font-bold">
          {{ report.averageRating }}
        </p>
      </div>
    </div>

    <div v-if="report" class="mt-6 rounded-lg border bg-white p-5 shadow">
      <h2 class="text-sm text-gray-500">Data Status</h2>

      <div class="mt-3 flex items-center gap-3">
        <span class="h-3 w-3 rounded-full" :class="isDataFresh ? 'bg-green-500' : 'bg-yellow-500'">
        </span>

        <span class="font-semibold">
          {{ isDataFresh ? 'Data is fresh' : 'Data may be outdated' }}
        </span>
      </div>

      <p class="mt-2 text-sm text-gray-500">
        Last scraped:
        {{ report.lastScraped ? new Date(report.lastScraped).toLocaleString() : 'Never' }}
      </p>
    </div>
  </div>
</template>
