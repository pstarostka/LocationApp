using System.Linq.Expressions;
using AutoMapper;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;
using LocationApp.Domain.Entities;
using LocationApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace LocationApp.Infrastructure.Services;

internal class GeolocationPersistenceService : IGeolocationService
{
    private readonly IGeolocationService _geolocationService;
    private readonly LocationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly RestClient _restClient;
    private readonly ILogger<GeolocationPersistenceService> _logger;

    public GeolocationPersistenceService(IGeolocationService geolocationService, LocationDbContext dbContext,
        IMapper mapper, RestClient restClient, ILogger<GeolocationPersistenceService> logger)
    {
        _geolocationService = geolocationService;
        _dbContext = dbContext;
        _mapper = mapper;
        _restClient = restClient;
        _logger = logger;
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

    public async Task UpdateGeolocation(UpdateGeolocationRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Geolocations.Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (entity is null)
        {
            var errorMessage = $"Entity with id '{request.Id}' was not found";
            _logger.LogError(errorMessage);
            throw new ApplicationException(errorMessage);
        }

        _dbContext.Entry(entity).CurrentValues.SetValues(_mapper.Map<GeolocationEntity>(request));

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveGeolocation(string ipAddressOrId, CancellationToken cancellationToken = default)
    {
        var isId = int.TryParse(ipAddressOrId, out var entityId);
        Expression<Func<GeolocationEntity, bool>> predicate = isId
            ? x => x.Id == entityId
            : x => x.Ip == ipAddressOrId;

        var entity = await _dbContext.Geolocations.Where(predicate)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        if (entity is null)
        {
            var errorMessage = "Entity to delete was not found";
            _logger.LogError(errorMessage);
            throw new ApplicationException(errorMessage);
        }

        _dbContext.Geolocations.Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}