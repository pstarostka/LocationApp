using System.Text.Json.Serialization;

namespace LocationApp.Application.Responses;

public class Location
{
    [JsonPropertyName("geoname_id")]
    public int GeonameId { get; set; }

    public string Capital { get; set; }
    public List<Language> Languages { get; set; }

    [JsonPropertyName("country_flag")]
    public string CountryFlag { get; set; }

    [JsonPropertyName("country_flag_emoji")]
    public string CountryFlagEmoji { get; set; }

    [JsonPropertyName("country_flag_emoji_unicode")]
    public string CountryFlagEmojiUnicode { get; set; }

    [JsonPropertyName("calling_code")]
    public string CallingCode { get; set; }

    [JsonPropertyName("is_eu")]
    public bool IsEu { get; set; }
}