using LocationApp.Application.Contracts.Responses;

namespace LocationApp.Application.Interfaces;

public interface IGeolocationService
{
    public Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses);
    public Task<GeolocationResponse> GetByMyIpAddress();
}