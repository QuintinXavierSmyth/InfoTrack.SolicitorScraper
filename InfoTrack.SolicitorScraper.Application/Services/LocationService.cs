using InfoTrack.SolicitorScraper.Application.DTOs;
using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Application.Services;

public class LocationService
{
    private readonly ISearchLocationRepository _locationRepository;


    public LocationService(
        ISearchLocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }


    public async Task<List<SearchLocation>> GetLocationsAsync()
    {
        return await _locationRepository.GetAllAsync();
    }


    public async Task<SearchLocation?> GetLocationAsync(Guid id)
    {
        return await _locationRepository.GetByIdAsync(id);
    }


    public async Task AddLocationAsync(SearchLocation location)
    {
        await _locationRepository.AddAsync(location);
    }


    public async Task UpdateLocationAsync(SearchLocation location)
    {
        await _locationRepository.UpdateAsync(location);
    }


    public async Task DeleteLocationAsync(Guid id)
    {
        await _locationRepository.DeleteAsync(id);
    }

    public async Task AddLocationAsync(CreateLocationDto dto)
    {
        var location = new SearchLocation
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            UrlSlug = dto.UrlSlug,
            IsEnabled = true
        };

        await _locationRepository.AddAsync(location);
    }


    public async Task UpdateLocationStatusAsync(Guid id, bool isEnabled)
    {
        var location = await _locationRepository.GetByIdAsync(id);

        if (location == null)
        {
            return;
        }


        location.IsEnabled = isEnabled;


        if (!isEnabled)
        {
            location.LastScraped = null;
        }


        await _locationRepository.UpdateAsync(location);
    }
}