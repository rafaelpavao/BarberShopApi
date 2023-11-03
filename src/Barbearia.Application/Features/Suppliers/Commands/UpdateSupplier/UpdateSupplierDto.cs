using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Suppliers.Commands.UpdateSupplier;

public class UpdateSupplierDto
{
    public int PersonId {get;set;}
    public string Name{get;set;} = string.Empty;
    public DateOnly BirthDate{get;set;}
    public string Cnpj {get;set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<AddressDto> Addresses {get; set;} = new();
    public List<TelephoneDto> Telephones {get; set;} = new();
}