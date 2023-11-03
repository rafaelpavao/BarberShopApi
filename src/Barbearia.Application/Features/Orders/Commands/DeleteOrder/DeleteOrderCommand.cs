using MediatR;

namespace Barbearia.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommand : IRequest<bool>
{
    public int OrderId {get;set;}
}