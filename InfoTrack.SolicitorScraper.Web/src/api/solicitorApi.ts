import http from './http'
import type { Solicitor } from '../types/Solicitor'

export async function getSolicitors(): Promise<Solicitor[]> {
  const response = await http.get<Solicitor[]>('/Solicitors')

  return response.data
}

export async function getSolicitorsByLocation(locationId: string): Promise<Solicitor[]> {
  const response = await http.get<Solicitor[]>(`/Solicitors/location/${locationId}`)

  return response.data
}
