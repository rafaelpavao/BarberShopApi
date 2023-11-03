using MediatR;

namespace Barbearia.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderCommandResponse>
{
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public int PersonId { get; set; }
    public List<int> StockHistoriesOrderId { get; set; } = new();
    public List<int> ProductsId { get; set; } = new();
    public List<int> AppointmentsId { get; set; } = new();

}