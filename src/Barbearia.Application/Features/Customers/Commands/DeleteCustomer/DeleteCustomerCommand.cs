using MediatR;

namespace Barbearia.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>
{
    public int PersonId {get;set;}
}