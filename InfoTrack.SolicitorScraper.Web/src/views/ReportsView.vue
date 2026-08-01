<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getReport } from '../api/reportApi'
import type { Report } from '../types/Report'

const report = ref<Report | null>(null)

onMounted(async () => {
  report.value = await getReport()
})
</script>

<template>
  <div class="p-6">
    <h1 class="mb-6 text-3xl font-bold">Reports</h1>

    <div v-if="report">
      <section class="mb-8">
        <h2 class="mb-4 text-xl font-semibold">Location Breakdown</h2>

        <div v-if="report" class="mt-6 rounded-lg border bg-white p-5 shadow">
          <h2 class="mb-4 text-xl font-semibold">Solicitors by Location</h2>

          <div v-for="location in report.locationBreakdown" :key="location.location" class="mb-4">
            <div class="mb-1 flex justify-between text-sm">
              <span>
                {{ location.location }}

                <span v-if="!location.isEnabled" class="text-xs text-gray-500"> (Disabled) </span>
              </span>

              <span>
                {{ location.solicitorCount }}
              </span>
            </div>

            <div class="h-3 rounded bg-gray-200">
              <div
                class="h-3 rounded bg-blue-600"
                :style="{
                  width: `${(location.solicitorCount / report.totalSolicitors) * 100}%`,
                }"
              ></div>
            </div>
          </div>
        </div>
      </section>

      <section class="mb-8">
        <h2 class="mb-4 text-xl font-semibold">Top Rated Solicitors</h2>

        <div class="overflow-x-auto rounded-lg border shadow">
          <table class="w-full">
            <thead class="bg-gray-100">
              <tr>
                <th class="border-b p-3 text-left">Name</th>

                <th class="border-b p-3 text-left">Location</th>

                <th class="border-b p-3 text-left">Rating</th>

                <th class="border-b p-3 text-left">Reviews</th>
              </tr>
            </thead>

            <tbody>
              <tr
                v-for="solicitor in report.topRatedSolicitors"
                :key="solicitor.name + solicitor.location"
                class="hover:bg-gray-50"
              >
                <td class="border-b p-3">
                  {{ solicitor.name }}
                </td>

                <td class="border-b p-3">
                  {{ solicitor.location }}
                </td>

                <td class="border-b p-3">
                  {{ solicitor.rating }}
                </td>

                <td class="border-b p-3">
                  {{ solicitor.reviewCount }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </section>

      <section class="mb-8">
        <h2 class="mb-4 text-xl font-semibold">Verification Breakdown</h2>

        <div class="rounded-lg border bg-white p-5 shadow">
          <div class="mb-4">
            <div class="mb-1 flex justify-between text-sm">
              <span> Verified </span>

              <span>
                {{ report.verifiedSolicitors }}
              </span>
            </div>

            <div class="h-3 rounded bg-gray-200">
              <div
                class="h-3 rounded bg-green-600"
                :style="{
                  width: `${(report.verifiedSolicitors / report.totalSolicitors) * 100}%`,
                }"
              ></div>
            </div>
          </div>

          <div>
            <div class="mb-1 flex justify-between text-sm">
              <span> Unverified </span>

              <span>
                {{ report.unverifiedSolicitors }}
              </span>
            </div>

            <div class="h-3 rounded bg-gray-200">
              <div
                class="h-3 rounded bg-gray-400"
                :style="{
                  width: `${(report.unverifiedSolicitors / report.totalSolicitors) * 100}%`,
                }"
              ></div>
            </div>
          </div>
        </div>
      </section>

      <section class="mb-8">
        <h2 class="mb-4 text-xl font-semibold">Rating Breakdown</h2>

        <div class="rounded-lg border bg-white p-5 shadow">
          <div v-for="rating in report.ratingBreakdown" :key="rating.rating" class="mb-4">
            <div class="mb-1 flex justify-between text-sm">
              <span> ⭐ {{ rating.rating }} Stars </span>

              <span>
                {{ rating.count }}
              </span>
            </div>

            <div class="h-3 rounded bg-gray-200">
              <div
                class="h-3 rounded bg-yellow-500"
                :style="{
                  width: `${(rating.count / report.totalSolicitors) * 100}%`,
                }"
              ></div>
            </div>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>
