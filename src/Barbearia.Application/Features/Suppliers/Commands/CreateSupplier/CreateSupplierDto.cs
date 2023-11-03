
using System.Text.Json.Serialization;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;
using static Barbearia.Domain.Entities.Person;

namespace Barbearia.Application.Features.Suppliers.Commands.CreateSupplier;

public class CreateSupplierDto
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

    //acho que não faz sentido criar o supplier com essas coisas, e sim adiciona-las com o tempo.

    // public List<Order> Orders { get; set; } = new();
    // public List<Product> Products { get; set; } = new();
    // public List <StockHistory> StockHistories { get; set; } = new();
}