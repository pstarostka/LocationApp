using AutoMapper;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Domain.Entities;
using LocationApp.Infrastructure.Models;

namespace LocationApp.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<GeolocationEntity, GeolocationResponse>().ReverseMap();
        CreateMap<GeolocationApiResponse, GeolocationResponse>();
    }
}