using InfoTrack.SolicitorScraper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SolicitorScraper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitorsController : ControllerBase
{
    private readonly SolicitorService _solicitorService;


    public SolicitorsController(
        SolicitorService solicitorService)
    {
        _solicitorService = solicitorService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var solicitors = await _solicitorService.GetSolicitorsAsync();

        return Ok(solicitors);
    }


    [HttpGet("location/{locationId}")]
    public async Task<IActionResult> GetByLocation(Guid locationId)
    {
        var solicitors = await _solicitorService
            .GetSolicitorsByLocationAsync(locationId);

        return Ok(solicitors);
    }
}