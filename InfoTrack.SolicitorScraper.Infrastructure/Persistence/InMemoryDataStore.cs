using InfoTrack.SolicitorScraper.Domain.Entities;

namespace InfoTrack.SolicitorScraper.Infrastructure.Persistence;

public class InMemoryDataStore
{
    public List<SearchLocation> Locations { get; set; } = new();

    public List<SolicitorDirectoryEntry> SolicitorEntries { get; set; } = new();
}