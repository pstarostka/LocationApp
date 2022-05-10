using System.Text.Json;
using AutoMapper;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Application.Interfaces;
using LocationApp.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace LocationApp.Infrastructure.Services;

internal class IpStackGeolocationService : IGeolocationService
{
    private readonly IMapper _mapper;

    private readonly string _apiKey;
    private readonly RestClient _restClient;

    public IpStackGeolocationService(IMapper mapper, RestClient restClient, IConfiguration config)
    {
        _mapper = mapper;
        _restClient = restClient;
        _apiKey = config["ApiKey"] ?? throw new Exception("ApiKey is missing");
    }


    public async Task<IEnumerable<GeolocationResponse>> GetByIpAddresses(ICollection<string> ipAddresses)
    {
        var request = new RestRequest("{ipAddresses}")
            .AddUrlSegment("ipAddresses", string.Join(",", ipAddresses))
            .AddQueryParameter("access_key", _apiKey);

        if (ipAddresses.Count > 1)
        {
            var bulkResponse = await _restClient.ExecuteAsync<IEnumerable<GeolocationApiResponse>>(request);

            if (!bulkResponse.IsSuccessful)
                throw new InvalidDataException(bulkResponse.ErrorMessage);

            if (bulkResponse.Data is not null) return _mapper.Map<IEnumerable<GeolocationResponse>>(bulkResponse.Data);

            var content = JsonSerializer.Deserialize<ApiErrorResponse>(bulkResponse.Content!, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (content is {Success: false})
                throw new InvalidDataException(content.Error?.Info);


            return _mapper.Map<IEnumerable<GeolocationResponse>>(bulkResponse.Data);
        }

        var singleResponse = await _restClient.ExecuteAsync<GeolocationApiResponse>(request);
        if (!singleResponse.IsSuccessful)
            throw new InvalidDataException(singleResponse.ErrorMessage);

        return _mapper.Map<IEnumerable<GeolocationResponse>>(new[] {singleResponse.Data});
    }

    public async Task<GeolocationResponse> GetByMyIpAddress()
    {
        var request = new RestRequest("check")
            .AddQueryParameter("access_key", _apiKey);

        var singleResponse = await _restClient.ExecuteAsync<GeolocationApiResponse>(request);
        return _mapper.Map<GeolocationResponse>(singleResponse.Data);
    }
}