using AutoMapper;
using Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;
using Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;
using Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class ProductCategoryProfile : Profile
{
    public ProductCategoryProfile()
    {   

        CreateMap<UpdateProductCategoryCommand, ProductCategory>(); 
        CreateMap<ProductCategory,UpdateProductCategoryDto>();

        CreateMap<ProductCategory, GetAllProductCategoriesDto>();
        CreateMap<ProductCategory,GetProductCategoryByIdDto>();

        CreateMap<CreateProductCategoryCommand, ProductCategory>();
        CreateMap<ProductCategory, CreateProductCategoryDto>();
    }
}