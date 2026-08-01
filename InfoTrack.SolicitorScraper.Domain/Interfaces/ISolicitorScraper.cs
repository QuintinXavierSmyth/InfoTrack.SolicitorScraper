using InfoTrack.SolicitorScraper.Domain.Entities;

namespace InfoTrack.SolicitorScraper.Domain.Interfaces;

public interface ISolicitorScraper
{
    Task<List<SolicitorDirectoryEntry>> ScrapeAsync(
        IEnumerable<SearchLocation> locations);
}