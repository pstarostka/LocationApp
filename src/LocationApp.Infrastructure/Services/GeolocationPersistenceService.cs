using AutoMapper;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;
using LocationApp.Domain.Entities;
using LocationApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LocationApp.Infrastructure.Services;

internal class GeolocationPersistenceService : IGeolocationService
{
    private readonly IGeolocationService _geolocationService;
    private readonly InMemoryDbContext _dbContext;
    private readonly IMapper _mapper;

    public GeolocationPersistenceService(IGeolocationService geolocationService, InMemoryDbContext dbContext,
        IMapper mapper)
    {
        _geolocationService = geolocationService;
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public async Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses)
    {
        var entities = await _dbContext.Geolocations
            .Where(x => ipAddresses.Contains(x.Ip))
            .ToListAsync();

        var mappedEntities = _mapper.Map<IEnumerable<GeolocationResponse>>(entities);

        var notFoundIpAddresses = ipAddresses.Except(entities.Select(x => x.Ip)).ToArray();

        if (notFoundIpAddresses.Length <= 0) return mappedEntities;

        var apiResponse =
            await _geolocationService.GetByIpAddresses(notFoundIpAddresses);

        var mappedApiResponse = _mapper.Map<IEnumerable<GeolocationEntity>>(apiResponse);

        await _dbContext.Geolocations.AddRangeAsync(mappedApiResponse);
        await _dbContext.SaveChangesAsync();
        return mappedEntities.Concat(_mapper.Map<IEnumerable<GeolocationResponse>>(mappedApiResponse));
    }

    public async Task<GeolocationResponse> GetByMyIpAddress()
    {
        return await _geolocationService.GetByMyIpAddress();
    }
}