using InfoTrack.SolicitorScraper.Domain.Entities;

namespace InfoTrack.SolicitorScraper.Domain.Interfaces;

public interface ISearchLocationRepository
{
    Task<List<SearchLocation>> GetAllAsync();

    Task<List<SearchLocation>> GetEnabledAsync();

    Task<SearchLocation?> GetByIdAsync(Guid id);

    Task AddAsync(SearchLocation location);

    Task UpdateAsync(SearchLocation location);

    Task DeleteAsync(Guid id);
}