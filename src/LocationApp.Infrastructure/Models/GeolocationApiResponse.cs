using System.Text.Json.Serialization;

namespace LocationApp.Infrastructure.Models;

internal class GeolocationApiResponse
{
    public string? Ip { get; set; }
    public string? Hostname { get; set; }
    public string? Type { get; set; }
    public string? City { get; set; }
    public string? Zip { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    [JsonPropertyName("continent_code")]
    public string? ContinentCode { get; set; }

    [JsonPropertyName("continent_name")]
    public string? ContinentName { get; set; }

    [JsonPropertyName("country_code")]
    public string? CountryCode { get; set; }

    [JsonPropertyName("country_name")]
    public string? CountryName { get; set; }

    [JsonPropertyName("region_code")]
    public string? RegionCode { get; set; }

    [JsonPropertyName("region_name")]
    public string? RegionName { get; set; }
}