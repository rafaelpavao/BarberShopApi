using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQuery : IRequest<GetCustomerByIdDto>
{
    public int PersonId {get; set;}
}