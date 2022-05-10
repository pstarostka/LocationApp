using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;

namespace LocationApp.API.Endpoints;

public class GetByIpCurrentIpAddressEndpoint : EndpointWithoutRequest<GeolocationResponse>
{
    private readonly IGeolocationService _geolocationService;

    public GetByIpCurrentIpAddressEndpoint(IGeolocationService geolocationService)
    {
        _geolocationService = geolocationService;
    }


    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("geolocation/check");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = await _geolocationService.GetByCurrentIpAddress(ct);
        await SendAsync(response, cancellation: ct);
    }
}