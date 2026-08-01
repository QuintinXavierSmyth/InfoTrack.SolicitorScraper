using InfoTrack.SolicitorScraper.Application.DTOs;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Application.Services;

public class SolicitorReportService
{
    private readonly ISolicitorRepository _solicitorRepository;
    private readonly ISearchLocationRepository _locationRepository;


    public SolicitorReportService(
        ISolicitorRepository solicitorRepository,
        ISearchLocationRepository locationRepository)
    {
        _solicitorRepository = solicitorRepository;
        _locationRepository = locationRepository;
    }


    public async Task<SolicitorSummaryReportDto> GetReportAsync()
    {
        var solicitors = await _solicitorRepository.GetAllAsync();

        var locations = await _locationRepository.GetAllAsync();

        return new SolicitorSummaryReportDto
        {
            TotalSolicitors = solicitors.Count,

            TotalLocations = locations.Count,

            EnabledLocations = locations.Count(x => x.IsEnabled),

            ScrapedLocations = locations.Count(x => x.LastScraped != null),

            OutOfSyncLocations = locations.Count(location =>
                !location.IsEnabled &&
                solicitors.Any(s => s.SearchLocationId == location.Id)
            ),

            VerifiedSolicitors = solicitors.Count(x => x.IsVerified),

            UnverifiedSolicitors = solicitors.Count(x => !x.IsVerified),

            AverageRating = solicitors.Any()
                ? Math.Round(
                    solicitors.Average(x => x.Rating),
                    2)
                : 0,

            RatingBreakdown = Enumerable.Range(0, 6)
                .OrderByDescending(x => x)
                .Select(rating => new RatingSummaryDto
                {
                    Rating = rating,
                    Count = solicitors.Count(x => Math.Floor(x.Rating) == rating)
                })
                .ToList(),

            LocationBreakdown = locations
                .Select(location => new LocationSummaryDto
                {
                    Location = location.Name,

                    SolicitorCount = solicitors.Count(x =>
                        x.SearchLocationId == location.Id),

                    IsEnabled = location.IsEnabled
                })
                .ToList(),


            TopRatedSolicitors = solicitors
                .Where(x => x.ReviewCount >= 10)
                .OrderByDescending(x => x.Rating)
                .ThenByDescending(x => x.ReviewCount)
                .Take(10)
                .Select(x => new TopSolicitorDto
                {
                    Name = x.Name,

                    Location = locations
                        .FirstOrDefault(l => l.Id == x.SearchLocationId)
                        ?.Name ?? "Unknown",

                    Rating = x.Rating,

                    ReviewCount = x.ReviewCount
                })
                .ToList(),

            LastScraped = locations
                .Where(x => x.LastScraped.HasValue)
                .OrderByDescending(x => x.LastScraped)
                .Select(x => x.LastScraped)
                .FirstOrDefault()
        };
    }
}