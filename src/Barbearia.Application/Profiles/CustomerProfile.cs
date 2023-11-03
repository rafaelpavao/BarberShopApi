using AutoMapper;
using Barbearia.Application.Features.Customers.Commands.CreateCustomer;
using Barbearia.Application.Features.Customers.Commands.UpdateCustomer;
using Barbearia.Application.Features.Customers.Queries.GetAllCustomers;
using Barbearia.Application.Features.Customers.Queries.GetCustomerById;
using Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;
using Barbearia.Application.Features.CustomersCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, GetCustomerByIdDto>();
        CreateMap<Customer, GetAllCustomersDto>();
        CreateMap<Customer, GetCustomerWithOrdersByIdDto>();
        CreateMap<Customer, GetCustomersCollectionDto>();

        CreateMap<CreateCustomerCommand, Customer>();        
        CreateMap<Customer, CreateCustomerDto>();  
        CreateMap<CustomerForCreationDto, Customer>();
        
        CreateMap<UpdateCustomerCommand, Customer>();     
        CreateMap<Customer,UpdateCustomerDto>();   

        CreateMap<TelephoneDto, Telephone>().ReverseMap();
        CreateMap<AddressDto, Address>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();  
    }
}