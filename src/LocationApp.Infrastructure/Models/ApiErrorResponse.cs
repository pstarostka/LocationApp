namespace LocationApp.Infrastructure.Models;

public class ApiErrorResponse
{
    public bool Success { get; set; }
    public ErrorResponse? Error { get; set; }

    public class ErrorResponse
    {
        public int Code { get; set; }
        public string? Type { get; set; }
        public string? Info { get; set; }
    }
}