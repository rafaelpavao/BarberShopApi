using MediatR;

namespace Barbearia.Application.Features.Suppliers.Commands.DeleteSupplier;

public class DeleteSupplierCommand : IRequest<bool>
{
    public int PersonId {get;set;}
}