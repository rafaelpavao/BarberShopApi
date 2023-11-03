using AutoMapper;
using Barbearia.Application.Features.Addresses.Commands.CreateAddress;
using Barbearia.Application.Features.Addresses.Commands.UpdateAddress;
using Barbearia.Application.Features.Addresses.Queries.GetAddress;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, GetAddressDto>().ReverseMap();
        CreateMap<CreateAddressCommand, Address>();
        CreateMap<Address, CreateAddressDto>();
        CreateMap<UpdateAddressCommand, Address>();
        CreateMap<UpdateAddressCommand, Customer>();
    }
}