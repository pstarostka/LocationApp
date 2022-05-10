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
    public LocationResponse? Location { get; set; }
    public CurrencyResponse? Currency { get; set; }
    public ConnectionResponse? Connection { get; set; }
    public SecurityResponse? Security { get; set; }

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

    [JsonPropertyName("time_zone")]
    public TimeZoneResponse? TimeZone { get; set; }

    public class LocationResponse
    {
        [JsonPropertyName("geoname_id")]
        public int GeonameId { get; set; }

        public string Capital { get; set; }
        public List<LanguageResponse>? Languages { get; set; }

        [JsonPropertyName("country_flag")]
        public string? CountryFlag { get; set; }

        [JsonPropertyName("country_flag_emoji")]
        public string? CountryFlagEmoji { get; set; }

        [JsonPropertyName("country_flag_emoji_unicode")]
        public string? CountryFlagEmojiUnicode { get; set; }

        [JsonPropertyName("calling_code")]
        public string? CallingCode { get; set; }

        [JsonPropertyName("is_eu")]
        public bool IsEu { get; set; }
    }

    public class LanguageResponse
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Native { get; set; }
    }

    public class SecurityResponse
    {
        [JsonPropertyName("is_proxy")]
        public bool IsProxy { get; set; }

        [JsonPropertyName("proxy_type")]
        public object? ProxyType { get; set; }

        [JsonPropertyName("is_crawler")]
        public bool IsCrawler { get; set; }

        [JsonPropertyName("crawler_name")]
        public object? CrawlerName { get; set; }

        [JsonPropertyName("crawler_type")]
        public object? CrawlerType { get; set; }

        [JsonPropertyName("is_tor")]
        public bool IsTor { get; set; }

        [JsonPropertyName("threat_level")]
        public string? ThreatLevel { get; set; }

        [JsonPropertyName("threat_types")]
        public object? ThreatTypes { get; set; }
    }

    public class TimeZoneResponse
    {
        public string Id { get; set; }

        [JsonPropertyName("current_time")]
        public DateTime CurrentTime { get; set; }

        [JsonPropertyName("gmt_offset")]
        public int GmtOffset { get; set; }

        public string? Code { get; set; }

        [JsonPropertyName("is_daylight_saving")]
        public bool IsDaylightSaving { get; set; }
    }

    public class CurrencyResponse
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Plural { get; set; }
        public string? Symbol { get; set; }

        [JsonPropertyName("symbol_native")]
        public string? SymbolNative { get; set; }
    }

    public class ConnectionResponse
    {
        public int Asn { get; set; }
        public string? Isp { get; set; }
    }
}