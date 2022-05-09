using System.Text.Json.Serialization;

namespace LocationApp.Application.Responses;

public class Security
{
    [JsonPropertyName("is_proxy")]
    public bool IsProxy { get; set; }

    [JsonPropertyName("proxy_type")]
    public object ProxyType { get; set; }

    [JsonPropertyName("is_crawler")]
    public bool IsCrawler { get; set; }

    [JsonPropertyName("crawler_name")]
    public object CrawlerName { get; set; }

    [JsonPropertyName("crawler_type")]
    public object CrawlerType { get; set; }

    [JsonPropertyName("is_tor")]
    public bool IsTor { get; set; }

    [JsonPropertyName("threat_level")]
    public string ThreatLevel { get; set; }

    [JsonPropertyName("threat_types")]
    public object ThreatTypes { get; set; }
}