using InfoTrack.SolicitorScraper.Application.DTOs;
using InfoTrack.SolicitorScraper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SolicitorScraper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly LocationService _locationService;


    public LocationsController(
        LocationService locationService)
    {
        _locationService = locationService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _locationService.GetLocationsAsync();

        return Ok(locations);
    }

    [HttpPost]
    public async Task<IActionResult> AddLocation(
    CreateLocationDto dto)
    {
        await _locationService.AddLocationAsync(dto);

        return Ok();
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
    Guid id,
    [FromBody] UpdateLocationStatusDto dto)
    {
        await _locationService.UpdateLocationStatusAsync(
            id,
            dto.IsEnabled);

        return NoContent();
    }
}