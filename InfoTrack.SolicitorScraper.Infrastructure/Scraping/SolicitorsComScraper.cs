using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InfoTrack.SolicitorScraper.Infrastructure.Scraping;

public class SolicitorsComScraper : ISolicitorScraper
{
    private readonly HttpClient _httpClient;
    private readonly SolicitorHtmlParser _parser;
    private readonly ScraperSettings _settings;
    private readonly ILogger<SolicitorsComScraper> _logger;


    public SolicitorsComScraper(
        HttpClient httpClient,
        SolicitorHtmlParser parser,
        IOptions<ScraperSettings> settings,
        ILogger<SolicitorsComScraper> logger)
    {
        _httpClient = httpClient;
        _parser = parser;
        _settings = settings.Value;
        _logger = logger;

        _httpClient.DefaultRequestHeaders.Add(
            "User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 Chrome/120 Safari/537.36");

        _httpClient.DefaultRequestHeaders.Add(
            "Accept",
            "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

        _httpClient.DefaultRequestHeaders.Add(
            "Accept-Language",
            "en-GB,en;q=0.9");
    }


    public async Task<List<SolicitorDirectoryEntry>> ScrapeAsync(
        IEnumerable<SearchLocation> locations)
    {
        var results = new List<SolicitorDirectoryEntry>();

        foreach (var location in locations)
        {
            var url = BuildSearchUrl(location);

            _logger.LogInformation(
                "Scraping solicitors for {Location} from {Url}",
                location.Name,
                url);


            var response = await _httpClient.GetAsync(url);

            var html = await response.Content.ReadAsStringAsync();


            _logger.LogInformation(
                "Status: {StatusCode}",
                response.StatusCode);

            _logger.LogInformation(
                "Content length: {Length}",
                html.Length);


            var solicitors = _parser.Parse(
                html,
                location);


            _logger.LogInformation(
                "Found {Count} solicitors for {Location}",
                solicitors.Count,
                location.Name);


            results.AddRange(solicitors);
        }

        return results;
    }


    private string BuildSearchUrl(SearchLocation location)
    {
        return $"{_settings.BaseUrl}/{location.UrlSlug}-solicitors.html";
    }
}