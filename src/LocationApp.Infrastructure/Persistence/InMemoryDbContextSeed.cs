using LocationApp.Domain.Entities;

namespace LocationApp.Infrastructure.Persistence;

internal static class InMemoryDbContextSeed
{
    public static void SeedData(LocationDbContext context)
    {
        context.Geolocations.AddRange(new List<GeolocationEntity>
        {
            //TODO: add "real" data to save up on api calls
            new GeolocationEntity
            {
                Ip = "196.168.0.1",
                City = "Test",
                Hostname = "",
                Latitude = 12.0,
                Longitude = 34.0,
                Type = "",
                Zip = "43-100",
                ContinentCode = "eu",
                ContinentName = "Europe",
                CountryCode = "PL",
                CountryName = "Poland",
                CreatedAt = DateTime.UtcNow,
                RegionCode = "sl",
                RegionName = "Slaskie"
            }
        });
        context.SaveChanges();
    }
}