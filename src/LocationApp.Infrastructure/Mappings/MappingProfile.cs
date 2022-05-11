using AutoMapper;
using LocationApp.Application.Contracts.Requests;
using LocationApp.Application.Contracts.Responses;
using LocationApp.Domain.Entities;
using LocationApp.Infrastructure.Models;

namespace LocationApp.Infrastructure.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateGeolocationRequest, GeolocationEntity>();
        CreateMap<GeolocationEntity, GeolocationResponse.GeolocationData>().ReverseMap();
        CreateMap<GeolocationApiResponse, GeolocationResponse.GeolocationData>().ReverseMap();

        CreateMap<GeolocationEntity, GeolocationResponse>()
            .ForMember(x => x.DataSource, opt => opt.MapFrom((_) => GeolocationResponse.DataSourceEnum.Database))
            .ForMember(src => src.Data,
                opt => opt.MapFrom(src => src))
            .ReverseMap();


        CreateMap<GeolocationApiResponse, GeolocationResponse>()
            .ForMember(x => x.DataSource, opt => opt.MapFrom((_) => GeolocationResponse.DataSourceEnum.IpStack))
            .ForMember(src => src.Data,
                opt => opt.MapFrom(src => src)).ReverseMap();
    }
}