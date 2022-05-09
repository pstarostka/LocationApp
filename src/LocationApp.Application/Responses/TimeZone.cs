using System.Text.Json.Serialization;

namespace LocationApp.Application.Responses;

public class TimeZone
{
    public string Id { get; set; }

    [JsonPropertyName("current_time")]
    public DateTime CurrentTime { get; set; }

    [JsonPropertyName("gmt_offset")]
    public int GmtOffset { get; set; }

    public string Code { get; set; }

    [JsonPropertyName("is_daylight_saving")]
    public bool IsDaylightSaving { get; set; }
}