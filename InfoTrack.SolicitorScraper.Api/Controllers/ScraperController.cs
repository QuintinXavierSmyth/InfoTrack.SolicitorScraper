using InfoTrack.SolicitorScraper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SolicitorScraper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScraperController : ControllerBase
{
    private readonly ScrapeService _scrapeService;


    public ScraperController(
        ScrapeService scrapeService)
    {
        _scrapeService = scrapeService;
    }


    [HttpPost("run")]
    public async Task<IActionResult> Run()
    {
        var results = await _scrapeService.RunScrapeAsync();

        return Ok(new
        {
            Count = results.Count,
            Results = results
        });
    }
}