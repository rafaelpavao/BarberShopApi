using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQuery : IRequest<IEnumerable<GetAllOrdersDto>>
{
    
}