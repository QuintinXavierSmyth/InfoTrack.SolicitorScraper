using InfoTrack.SolicitorScraper.Domain.Entities;
using InfoTrack.SolicitorScraper.Infrastructure.Persistence;

namespace InfoTrack.SolicitorScraper.Infrastructure.Seed;

public static class LocationSeeder
{
    public static void Seed(InMemoryDataStore dataStore)
    {
        if (dataStore.Locations.Any())
            return;


        dataStore.Locations.AddRange(
            new[]
            {
                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "London",
                    UrlSlug = "london",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Birmingham",
                    UrlSlug = "birmingham",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Leeds",
                    UrlSlug = "leeds",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Manchester",
                    UrlSlug = "manchester",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Sheffield",
                    UrlSlug = "sheffield",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Bradford",
                    UrlSlug = "bradford",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Liverpool",
                    UrlSlug = "liverpool",
                    IsEnabled = true
                },

                new SearchLocation
                {
                    Id = Guid.NewGuid(),
                    Name = "Bristol",
                    UrlSlug = "bristol",
                    IsEnabled = true
                }
            });
    }
}