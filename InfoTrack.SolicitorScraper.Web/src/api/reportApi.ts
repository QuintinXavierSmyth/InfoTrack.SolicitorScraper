import http from './http'
import type { Report } from '../types/Report'

export async function getReport(): Promise<Report> {
  const response = await http.get<Report>('/Reports')

  return response.data
}
