using MediatR;

namespace Barbearia.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQuery : IRequest<GetSupplierByIdDto>
{
    public int PersonId {get; set;}
}