import api from './http'
import type { SearchLocation } from '../types/SearchLocation'

export async function getLocations(): Promise<SearchLocation[]> {
  const response = await api.get<SearchLocation[]>('/Locations')

  return response.data
}

export async function updateLocationStatus(id: string, isEnabled: boolean): Promise<void> {
  await api.put(`/Locations/${id}/status`, {
    isEnabled,
  })
}

export async function addLocation(name: string, urlSlug: string): Promise<void> {
  await api.post('/Locations', {
    name,
    urlSlug,
  })
}
