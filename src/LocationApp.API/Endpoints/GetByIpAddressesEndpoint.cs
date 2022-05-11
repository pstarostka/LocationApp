using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;

namespace LocationApp.API.Endpoints;

public class GetByIpAddressesEndpoint : Endpoint<GeolocationByIpAddressesRequest, List<GeolocationResponse>>
{
    private readonly IGeolocationService _geolocationService;

    public GetByIpAddressesEndpoint(IGeolocationService geolocationService)
    {
        _geolocationService = geolocationService;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("geolocation/{ipAddresses}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GeolocationByIpAddressesRequest req, CancellationToken ct)
    {
        var ipAddresses = req.IpAddresses.Split(",");

        var response = await _geolocationService.GetByIpAddresses(ipAddresses, ct);
        await SendAsync(response.ToList(), cancellation: ct);
    }
}