using AutoMapper;
using Barbearia.Application.Features.ServiceCategories.Queries.GetAllServiceCategories;
using Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;
using Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

namespace Barbearia.Application.Profiles;

public class ServiceCategoryProfile : Profile
{
    public ServiceCategoryProfile()
    {
        CreateMap<ServiceCategory, ServiceCategoryDto>().ReverseMap();

        CreateMap<UpdateServiceCategoryCommand, ServiceCategory>(); 
        CreateMap<ServiceCategory,UpdateServiceCategoryDto>();

        CreateMap<ServiceCategory, GetAllServiceCategoriesDto>();
        CreateMap<ServiceCategory,GetServiceCategoryByIdDto>().ReverseMap();

        CreateMap<CreateServiceCategoryCommand, ServiceCategory>();
        CreateMap<ServiceCategory, CreateServiceCategoryDto>();
    }
}