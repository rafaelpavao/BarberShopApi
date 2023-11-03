
using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommand : IRequest<CreateEmployeeCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Status { get; set; }
    public List<TelephoneDto> Telephones { get; set; } = new();
    public List<AddressDto> Addresses { get; set; } = new();
    public List<int> RolesId {get;set;} = new();
    public List<int> ServicesId {get;set;} = new();
}