import { createRouter, createWebHistory } from 'vue-router'

import DashboardView from '../views/DashboardView.vue'
import LocationsView from '../views/LocationsView.vue'
import SolicitorsView from '../views/SolicitorsView.vue'
import ReportsView from '../views/ReportsView.vue'

const router = createRouter({
  history: createWebHistory(),

  routes: [
    {
      path: '/',
      component: DashboardView,
    },
    {
      path: '/locations',
      component: LocationsView,
    },
    {
      path: '/solicitors',
      component: SolicitorsView,
    },
    {
      path: '/reports',
      component: ReportsView,
    },
  ],
})

export default router
