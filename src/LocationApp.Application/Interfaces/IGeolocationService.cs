using LocationApp.Application.Contracts.Responses;

namespace LocationApp.Application.Interfaces;

public interface IGeolocationService
{
    public Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses, CancellationToken cancellationToken = default);
    public Task<GeolocationResponse> GetByCurrentIpAddress(CancellationToken cancellationToken = default);
}