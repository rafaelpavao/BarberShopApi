using AutoMapper;
using Barbearia.Application.Features.Products.Commands.CreateProduct;
using Barbearia.Application.Features.Products.Commands.UpdateProduct;
using Barbearia.Application.Features.Products.Queries.GetAllProducts;
using Barbearia.Application.Features.Products.Queries.GetProductById;
using Barbearia.Application.Features.ProductsCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, GetProductsCollectionDto>();    
        CreateMap<Product, ProductForSupplierDto>();    

        CreateMap<Product, ProductForStockHistoryDto>();  

        CreateMap<UpdateProductCommand, Product>(); 
        CreateMap<Product,UpdateProductDto>();

        CreateMap<Product, GetAllProductsDto>();
        CreateMap<Product,GetProductByIdDto>();

        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductDto>();

        CreateMap<ProductCategoryDto, ProductCategory>().ReverseMap(); 
    }
}