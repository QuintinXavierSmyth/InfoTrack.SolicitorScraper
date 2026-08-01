export interface Report {
  totalSolicitors: number
  totalLocations: number
  enabledLocations: number
  scrapedLocations: number
  outOfSyncLocations: number
  verifiedSolicitors: number
  unverifiedSolicitors: number
  averageRating: number
  ratingBreakdown: {
    rating: number
    count: number
  }[]
  lastScraped: string | null
  locationBreakdown: LocationBreakdown[]
  topRatedSolicitors: TopRatedSolicitor[]
}

export interface LocationBreakdown {
  location: string
  solicitorCount: number
  isEnabled: boolean
}

export interface TopRatedSolicitor {
  name: string
  location: string
  rating: number
  reviewCount: number
}
