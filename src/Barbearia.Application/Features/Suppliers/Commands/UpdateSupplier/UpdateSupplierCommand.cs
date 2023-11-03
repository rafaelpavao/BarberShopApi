using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierCommand : IRequest<UpdateSupplierCommandResponse>
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Status { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();
    public List<TelephoneDto> Telephones { get; set; } = new();
}