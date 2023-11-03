using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResponse>
{
    public int PersonId{get;set;}
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<TelephoneDto> Telephones { get; set; } = new();
    public List<AddressDto> Addresses { get; set; } = new();
}