using Microsoft.Extensions.DependencyInjection;
using InfoTrack.SolicitorScraper.Application.Services;

namespace InfoTrack.SolicitorScraper.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<LocationService>();
        services.AddScoped<SolicitorService>();
        services.AddScoped<ScrapeService>();
        services.AddScoped<SolicitorReportService>();

        return services;
    }
}