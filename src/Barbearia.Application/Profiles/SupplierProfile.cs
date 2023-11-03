using AutoMapper;
using Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;
using Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;
using Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;
using Barbearia.Application.Features.SuppliersCollection;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<Supplier, GetSupplierByIdDto>().ReverseMap();

        CreateMap<CreateSupplierCommand, Supplier>();        
        CreateMap<Supplier, CreateSupplierDto>();  

        CreateMap<UpdateSupplierCommand, Supplier>();     
        CreateMap<Supplier,UpdateSupplierDto>(); 

        CreateMap<Supplier, GetSuppliersCollectionDto>();
    }
}