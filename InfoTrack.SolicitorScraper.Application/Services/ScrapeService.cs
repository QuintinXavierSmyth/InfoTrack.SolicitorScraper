using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Application.Services;

public class ScrapeService
{
    private readonly ISearchLocationRepository _locationRepository;
    private readonly ISolicitorScraper _scraper;
    private readonly ISolicitorRepository _solicitorRepository;


    public ScrapeService(
        ISearchLocationRepository locationRepository,
        ISolicitorScraper scraper,
        ISolicitorRepository solicitorRepository)
    {
        _locationRepository = locationRepository;
        _scraper = scraper;
        _solicitorRepository = solicitorRepository;
    }


    public async Task<List<SolicitorDirectoryEntry>> RunScrapeAsync()
    {
        var locations = await _locationRepository.GetEnabledAsync();


        var results = await _scraper.ScrapeAsync(locations);


        await _solicitorRepository.ReplaceAllAsync(results);


        foreach (var location in locations)
        {
            location.LastScraped = DateTime.UtcNow;
        }


        return results;
    }
}