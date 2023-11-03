using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerWithOrdersById;

public class GetCustomerWithOrdersByIdQuery : IRequest<GetCustomerWithOrdersByIdDto>
{
    public int PersonId {get; set;}
}