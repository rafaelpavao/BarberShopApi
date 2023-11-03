using Barbearia.Application.Models;

namespace Barbearia.Application.Features.OrdersCollection;

public class GetOrdersCollectionDto
{
    public int OrderId {get;set;}
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public int PersonId { get; set; }
    public PersonDto? Person { get; set; }
    public PaymentDto? Payment { get; set; }
}