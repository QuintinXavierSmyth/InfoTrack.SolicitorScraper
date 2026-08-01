import http from './http'

export interface ScrapeResult {
  count: number
  results: unknown[]
}

export async function runScrape(): Promise<ScrapeResult> {
  const response = await http.post<ScrapeResult>('/Scraper/run')

  return response.data
}
