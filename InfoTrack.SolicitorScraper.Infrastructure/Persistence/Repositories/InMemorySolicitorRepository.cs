using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Infrastructure.Persistence.Repositories;

public class InMemorySolicitorRepository : ISolicitorRepository
{
    private readonly InMemoryDataStore _dataStore;

    public InMemorySolicitorRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }

    public Task<List<SolicitorDirectoryEntry>> GetAllAsync()
    {
        return Task.FromResult(_dataStore.SolicitorEntries.ToList());
    }

    public Task<List<SolicitorDirectoryEntry>> GetByLocationAsync(Guid locationId)
    {
        var results = _dataStore.SolicitorEntries
            .Where(x => x.SearchLocationId == locationId)
            .ToList();

        return Task.FromResult(results);
    }

    public Task ReplaceAllAsync(
        IEnumerable<SolicitorDirectoryEntry> solicitors)
    {
        _dataStore.SolicitorEntries.Clear();
        _dataStore.SolicitorEntries.AddRange(solicitors);

        return Task.CompletedTask;
    }
}