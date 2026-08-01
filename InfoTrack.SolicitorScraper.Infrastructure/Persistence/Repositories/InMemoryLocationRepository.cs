using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Infrastructure.Persistence.Repositories;

public class InMemoryLocationRepository : ISearchLocationRepository
{
    private readonly InMemoryDataStore _dataStore;

    public InMemoryLocationRepository(InMemoryDataStore dataStore)
    {
        _dataStore = dataStore;
    }


    public Task<List<SearchLocation>> GetAllAsync()
    {
        return Task.FromResult(
            _dataStore.Locations.ToList()
        );
    }


    public Task<List<SearchLocation>> GetEnabledAsync()
    {
        var locations = _dataStore.Locations
            .Where(x => x.IsEnabled)
            .ToList();

        return Task.FromResult(locations);
    }


    public Task<SearchLocation?> GetByIdAsync(Guid id)
    {
        var location = _dataStore.Locations
            .FirstOrDefault(x => x.Id == id);

        return Task.FromResult(location);
    }


    public Task AddAsync(SearchLocation location)
    {
        location.Id = Guid.NewGuid();

        _dataStore.Locations.Add(location);

        return Task.CompletedTask;
    }


    public Task UpdateAsync(SearchLocation location)
    {
        var existing = _dataStore.Locations
            .FirstOrDefault(x => x.Id == location.Id);

        if (existing != null)
        {
            existing.Name = location.Name;
            existing.IsEnabled = location.IsEnabled;
        }

        return Task.CompletedTask;
    }


    public Task DeleteAsync(Guid id)
    {
        var location = _dataStore.Locations
            .FirstOrDefault(x => x.Id == id);

        if (location != null)
        {
            _dataStore.Locations.Remove(location);
        }

        return Task.CompletedTask;
    }
}