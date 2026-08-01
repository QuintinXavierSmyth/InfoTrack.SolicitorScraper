using InfoTrack.SolicitorScraper.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrack.SolicitorScraper.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly SolicitorReportService _reportService;


    public ReportsController(
        SolicitorReportService reportService)
    {
        _reportService = reportService;
    }


    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var report = await _reportService.GetReportAsync();

        return Ok(report);
    }
}