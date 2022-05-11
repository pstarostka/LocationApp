using System.Text.Json;
using AutoMapper;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;
using LocationApp.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace LocationApp.Infrastructure.Services;

internal class IpStackGeolocationService : IGeolocationService
{
    private readonly IMapper _mapper;
    private readonly RestClient _restClient;
    private readonly ILogger<IpStackGeolocationService> _logger;
    private readonly string _apiKey;

    public IpStackGeolocationService(IMapper mapper, RestClient restClient, IConfiguration config,
        ILogger<IpStackGeolocationService> logger)
    {
        _mapper = mapper;
        _restClient = restClient;
        _logger = logger;
        _apiKey = config["IpStackApiKey"] ?? throw new Exception("IpStackApiKey is missing");
    }

    public async Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses,
        CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("{ipAddresses}")
            .AddUrlSegment("ipAddresses", string.Join(",", ipAddresses))
            .AddQueryParameter("access_key", _apiKey);

        if (ipAddresses.Count > 1)
        {
            return await BulkByIpAddresses(cancellationToken, request);
        }

        return await SingleByIpAddresses(cancellationToken, request);
    }

    private async Task<IEnumerable<GeolocationResponse>> SingleByIpAddresses(CancellationToken cancellationToken,
        RestRequest request)
    {
        var singleResponse =
            await _restClient.ExecuteAsync<GeolocationApiResponse>(request, cancellationToken: cancellationToken);

        if (singleResponse.IsSuccessful)
            return _mapper.Map<IEnumerable<GeolocationResponse>>(new[] {singleResponse.Data});

        _logger.LogError(singleResponse.ErrorMessage);
        throw new InvalidDataException(singleResponse.ErrorMessage);
    }

    private async Task<IEnumerable<GeolocationResponse>> BulkByIpAddresses(CancellationToken cancellationToken,
        RestRequest request)
    {
        var bulkResponse =
            await _restClient.ExecuteAsync<IEnumerable<GeolocationApiResponse>>(request,
                cancellationToken: cancellationToken);

        if (!bulkResponse.IsSuccessful)
        {
            _logger.LogError(bulkResponse.ErrorMessage);
            throw new InvalidDataException(bulkResponse.ErrorMessage);
        }

        if (bulkResponse.Data is not null) return _mapper.Map<IEnumerable<GeolocationResponse>>(bulkResponse.Data);

        var content = JsonSerializer.Deserialize<ApiErrorResponse>(bulkResponse.Content!, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (content is {Success: false})
        {
            _logger.LogError(content.Error?.Info);
            throw new InvalidDataException(content.Error?.Info);
        }


        return _mapper.Map<IEnumerable<GeolocationResponse>>(bulkResponse.Data);
    }

    public async Task<GeolocationResponse> GetByCurrentIpAddress(CancellationToken cancellationToken = default)
    {
        var request = new RestRequest("check")
            .AddQueryParameter("access_key", _apiKey);

        var apiResponse =
            await _restClient.ExecuteAsync<GeolocationApiResponse>(request, cancellationToken: cancellationToken);

        if (apiResponse.IsSuccessful)
            return _mapper.Map<GeolocationResponse>(apiResponse.Data);

        _logger.LogError(apiResponse.ErrorMessage);
        throw new InvalidDataException(apiResponse.ErrorMessage);
    }

    public Task UpdateGeolocation(UpdateGeolocationRequest request, CancellationToken cancellationToken = default)
    {
        // IpStack does not have a public update implementation
        throw new NotImplementedException();
    }

    public Task RemoveGeolocation(string ipAddressOrId, CancellationToken cancellationToken = default)
    {
        // IpStack does not have a public remove implementation
        throw new NotImplementedException();
    }
}