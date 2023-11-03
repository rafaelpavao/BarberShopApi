using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersDto
{
    public int PersonId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public List<TelephoneDto> Telephones { get; set; } = new List<TelephoneDto>();
}