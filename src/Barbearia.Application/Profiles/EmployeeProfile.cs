using AutoMapper;
using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Application.Features.Employees.Commands.UpdateEmployee;
using Barbearia.Application.Features.Employees.Queries.GetEmployeeById;
using Barbearia.Application.Features.EmployeesCollection;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, GetEmployeeByIdDto>();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<CreateEmployeeCommand, Employee>();
        CreateMap<Employee, CreateEmployeeDto>();
        CreateMap<UpdateEmployeeCommand, Employee>();
        CreateMap<Employee,UpdateEmployeeDto>();
        CreateMap<Employee, GetEmployeesCollectionDto>();
        CreateMap<TelephoneDto, Telephone>().ReverseMap();
        CreateMap<AddressDto, Address>().ReverseMap();        
    }
}