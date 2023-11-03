using MediatR;

namespace Barbearia.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<IEnumerable<GetAllCustomersDto>>
{
    
}