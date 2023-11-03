using AutoMapper;
using Barbearia.Application.Features.Services.Commands.CreateService;
using Barbearia.Application.Features.Services.Commands.UpdateService;
using Barbearia.Application.Features.Services.Queries.GetServiceById;
using Barbearia.Application.Features.ServicesCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, GetServicesCollectionDto>();

        CreateMap<Service,ServiceForServiceCategoryQueriesDto>().ReverseMap();
        
        CreateMap<UpdateServiceCommand, Service>();
        CreateMap<Service, UpdateServiceDto>();

        CreateMap<Service, GetServiceByIdDto>();

        CreateMap<CreateServiceCommand, Service>();
        CreateMap<Service, CreateServiceDto>();
    }
}