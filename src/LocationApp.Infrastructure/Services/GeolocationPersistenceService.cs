using AutoMapper;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;
using LocationApp.Domain.Entities;
using LocationApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using RestSharp;

namespace LocationApp.Infrastructure.Services;

internal class GeolocationPersistenceService : IGeolocationService
{
    private readonly IGeolocationService _geolocationService;
    private readonly InMemoryDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly RestClient _restClient;

    public GeolocationPersistenceService(IGeolocationService geolocationService, InMemoryDbContext dbContext,
        IMapper mapper, RestClient restClient)
    {
        _geolocationService = geolocationService;
        _dbContext = dbContext;
        _mapper = mapper;
        _restClient = restClient;
    }


    public async Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses,
        CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext.Geolocations
            .Where(x => ipAddresses.Contains(x.Ip))
            .ToListAsync(cancellationToken: cancellationToken);

        var mappedEntities = _mapper.Map<IEnumerable<GeolocationResponse>>(entities);

        var notFoundIpAddresses = ipAddresses.Except(entities.Select(x => x.Ip)).ToArray();

        if (notFoundIpAddresses.Length <= 0) return mappedEntities;

        var apiResponse =
            await _geolocationService.GetByIpAddresses(notFoundIpAddresses, cancellationToken);

        var mappedApiResponse = _mapper.Map<IEnumerable<GeolocationEntity>>(apiResponse);

        await _dbContext.Geolocations.AddRangeAsync(mappedApiResponse, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return mappedEntities.Concat(_mapper.Map<IEnumerable<GeolocationResponse>>(mappedApiResponse));
    }

    public async Task<GeolocationResponse> GetByCurrentIpAddress(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("https://api.ipify.org/");
        var ipResponse = await _restClient.ExecuteAsync(request, cancellationToken);

        var entity = await _dbContext.Geolocations.Where(x => x.Ip == ipResponse.Content)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (entity is not null) return _mapper.Map<GeolocationResponse>(entity);

        var response = await _geolocationService.GetByCurrentIpAddress(cancellationToken);
        var mappedApiResponse = _mapper.Map<GeolocationEntity>(response);

        await _dbContext.Geolocations.AddAsync(mappedApiResponse, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GeolocationResponse>(mappedApiResponse);
    }
}