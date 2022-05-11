namespace LocationApp.Application.Contracts.Responses;

public class GeolocationResponse
{
    public DataSourceEnum DataSource { get; set; }

    public GeolocationData Data { get; set; } = new();

    public class GeolocationData
    {
        public int Id { get; set; }
        public string Ip { get; set; } = "";
        public string? Hostname { get; set; }
        public string? Type { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? ContinentCode { get; set; }
        public string? ContinentName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryName { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }

    public enum DataSourceEnum
    {
        IpStack,
        Database,
    }
}