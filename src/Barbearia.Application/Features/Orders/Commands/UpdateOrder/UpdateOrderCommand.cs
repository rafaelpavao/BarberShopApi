using MediatR;

namespace Barbearia.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : IRequest<UpdateOrderCommandResponse>
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public int Number { get; set; }
    public int Status { get; set; }
    public DateTime BuyDate { get; set; }
    public List<int> StockHistoriesOrderId { get; set; } = new();
    public List<int> ProductsId { get; set; } = new();
    public List<int> AppointmentsId { get; set; } = new();
}