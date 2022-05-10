using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Interfaces;

namespace LocationApp.API.Endpoints;

public class UpdateGeolocationEndpoint : Endpoint<UpdateGeolocationRequest>
{
    private readonly IGeolocationService _geolocationService;

    public UpdateGeolocationEndpoint(IGeolocationService geolocationService)
    {
        _geolocationService = geolocationService;
    }


    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("geolocation");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateGeolocationRequest req, CancellationToken ct)
    {
        await _geolocationService.UpdateGeolocation(req, ct);
        await SendNoContentAsync(ct);
    }
}