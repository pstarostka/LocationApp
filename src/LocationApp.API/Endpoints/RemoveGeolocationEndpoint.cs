using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Interfaces;

namespace LocationApp.API.Endpoints;

public class RemoveGeolocationEndpoint : Endpoint<RemoveGeolocationRequest>
{
    private readonly IGeolocationService _geolocationService;

    public RemoveGeolocationEndpoint(IGeolocationService geolocationService)
    {
        _geolocationService = geolocationService;
    }


    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("geolocation");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RemoveGeolocationRequest req, CancellationToken ct)
    {
        await _geolocationService.RemoveGeolocation(req.IpAddressOrId, ct);
        await SendNoContentAsync(ct);
    }
}