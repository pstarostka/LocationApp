using LocationApp.Domain.Entities;

namespace LocationApp.Infrastructure.Persistence;

internal static class InMemoryDbContextSeed
{
    public static void SeedData(LocationDbContext context)
    {
        context.Geolocations.AddRange(new List<GeolocationEntity>
        {
            new()
            {
                Ip = "185.31.24.68",
                City = "Poznań",
                Hostname = "",
                Latitude = 52.40496063232422,
                Longitude = 16.839109420776367,
                Type = "ipv4",
                Zip = "60-166",
                ContinentCode = "EU",
                ContinentName = "Europe",
                CountryCode = "PL",
                CountryName = "Poland",
                CreatedAt = DateTime.UtcNow,
                RegionCode = "WP",
                RegionName = "Greater Poland"
            },
            new()
            {
                Ip = "212.77.98.9",
                City = "Gdańsk",
                Hostname = "",
                Latitude = 54.31930923461914,
                Longitude = 18.63736915588379,
                Type = "ipv4",
                Zip = "80-009",
                ContinentCode = "EU",
                ContinentName = "Europe",
                CountryCode = "PL",
                CountryName = "Poland",
                CreatedAt = DateTime.UtcNow,
                RegionCode = "PM",
                RegionName = "Pomerania"
            },
        });
        context.SaveChanges();
    }
}