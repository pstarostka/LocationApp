using System.Text.Json.Serialization;

namespace LocationApp.Application.Responses;

public class Currency
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Plural { get; set; }
    public string Symbol { get; set; }

    [JsonPropertyName("symbol_native")]
    public string SymbolNative { get; set; }
}