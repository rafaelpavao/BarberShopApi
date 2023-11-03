using MediatR;

namespace Barbearia.Application.Features.Orders.Queries.GetOrderByNumber;

public class GetOrderByNumberQuery : IRequest<GetOrderByNumberDto>
{
    public int Number {get;set;}
}