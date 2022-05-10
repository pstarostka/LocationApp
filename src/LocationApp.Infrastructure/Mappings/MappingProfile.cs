﻿using AutoMapper;
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
        CreateMap<GeolocationEntity, GeolocationResponse>().ReverseMap();
        CreateMap<GeolocationApiResponse, GeolocationResponse>();
    }
}