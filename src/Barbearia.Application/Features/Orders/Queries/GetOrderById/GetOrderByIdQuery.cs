using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQuery : IRequest<GetOrderByIdDto>
{
    public int OrderId {get;set;}
}