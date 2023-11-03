using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdDto
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public string Cnpj { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TypeStatus Status { get; set; }
    public List<AddressDto> Addresses { get; set; } = new();
    public List<TelephoneDto> Telephones { get; set; } = new();
    public List<ProductForSupplierDto> Products { get; set; } = new();
}