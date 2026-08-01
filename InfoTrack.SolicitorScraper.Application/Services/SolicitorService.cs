using InfoTrack.SolicitorScraper.Application.DTOs;
using InfoTrack.SolicitorScraper.Domain.Interfaces;

namespace InfoTrack.SolicitorScraper.Application.Services;

public class SolicitorService
{
    private readonly ISolicitorRepository _solicitorRepository;
    private readonly ISearchLocationRepository _locationRepository;


    public SolicitorService(
        ISolicitorRepository solicitorRepository,
        ISearchLocationRepository locationRepository)
    {
        _solicitorRepository = solicitorRepository;
        _locationRepository = locationRepository;
    }


    public async Task<List<SolicitorDto>> GetSolicitorsAsync()
    {
        var solicitors = await _solicitorRepository.GetAllAsync();

        var locations = await _locationRepository.GetAllAsync();


        return solicitors.Select(s => new SolicitorDto
        {
            Name = s.Name,

            Location = locations
                .FirstOrDefault(l => l.Id == s.SearchLocationId)?.Name
                ?? "Unknown",

            IsVerified = s.IsVerified,

            Rating = s.Rating,

            ReviewCount = s.ReviewCount,

            PhoneNumber = s.PhoneNumber,

            Address = s.Address,

            Description = s.Description,

            WebsiteUrl = s.WebsiteUrl,

            ViewMoreUrl = s.ViewMoreUrl

        }).ToList();
    }


    public async Task<List<SolicitorDto>> GetSolicitorsByLocationAsync(Guid locationId)
    {
        var solicitors = await _solicitorRepository.GetByLocationAsync(locationId);

        var location = await _locationRepository.GetByIdAsync(locationId);


        return solicitors.Select(s => new SolicitorDto
        {
            Name = s.Name,

            Location = location?.Name ?? "Unknown",

            IsVerified = s.IsVerified,

            Rating = s.Rating,

            ReviewCount = s.ReviewCount,

            PhoneNumber = s.PhoneNumber,

            Address = s.Address,

            Description = s.Description,

            WebsiteUrl = s.WebsiteUrl,

            ViewMoreUrl = s.ViewMoreUrl

        }).ToList();
    }
}