using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;

public class GetCustomerWithOrdersByIdDto{
    public string Name {get;set;} = string.Empty;
    public DateOnly BirthDate {get;set;}
    public int Gender { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<OrderDto> Orders {get;set;} = new List<OrderDto>();
}