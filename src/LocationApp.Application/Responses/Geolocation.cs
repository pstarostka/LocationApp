using System.Text.Json.Serialization;

namespace LocationApp.Application.Responses;

public class Geolocation
{
    public string Ip { get; set; }
    public string Hostname { get; set; }
    public string Type { get; set; }
    public string City { get; set; }
    public string Zip { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Location Location { get; set; }
    public Currency Currency { get; set; }
    public Connection Connection { get; set; }
    public Security Security { get; set; }

    [JsonPropertyName("continent_code")]
    public string ContinentCode { get; set; }

    [JsonPropertyName("continent_name")]
    public string ContinentName { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("country_name")]
    public string CountryName { get; set; }

    [JsonPropertyName("region_code")]
    public string RegionCode { get; set; }

    [JsonPropertyName("region_name")]
    public string RegionName { get; set; }

    [JsonPropertyName("time_zone")]
    public TimeZone TimeZone { get; set; }
}