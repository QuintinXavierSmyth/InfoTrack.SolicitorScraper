namespace InfoTrack.SolicitorScraper.Application.DTOs;

public class SolicitorSummaryReportDto
{
    public int TotalSolicitors { get; set; }

    public int TotalLocations { get; set; }

    public int EnabledLocations { get; set; }

    public int ScrapedLocations { get; set; }

    public int OutOfSyncLocations { get; set; }

    public int VerifiedSolicitors { get; set; }

    public int UnverifiedSolicitors { get; set; }

    public decimal AverageRating { get; set; }

    public List<RatingSummaryDto> RatingBreakdown { get; set; } = new();

    public DateTime? LastScraped { get; set; }

    public List<LocationSummaryDto> LocationBreakdown { get; set; } = new();

    public List<TopSolicitorDto> TopRatedSolicitors { get; set; } = new();
}