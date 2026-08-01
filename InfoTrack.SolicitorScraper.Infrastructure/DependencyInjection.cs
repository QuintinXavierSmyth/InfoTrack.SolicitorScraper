using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using InfoTrack.SolicitorScraper.Infrastructure.Scraping;
using InfoTrack.SolicitorScraper.Domain.Interfaces;
using InfoTrack.SolicitorScraper.Infrastructure.Persistence;
using InfoTrack.SolicitorScraper.Infrastructure.Persistence.Repositories;

namespace InfoTrack.SolicitorScraper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ScraperSettings>(
            configuration.GetSection("ScraperSettings"));


        services.AddSingleton<InMemoryDataStore>();

        services.AddSingleton<ISearchLocationRepository,
            InMemoryLocationRepository>();

        services.AddSingleton<ISolicitorRepository,
            InMemorySolicitorRepository>();

        services.AddHttpClient<ISolicitorScraper, SolicitorsComScraper>();

        services.AddSingleton<SolicitorHtmlParser>();

        return services;
    }
}