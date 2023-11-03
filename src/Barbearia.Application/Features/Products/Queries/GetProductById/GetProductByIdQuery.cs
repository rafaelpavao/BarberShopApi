using MediatR;

namespace Barbearia.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<GetProductByIdDto>
{
    public int ItemId {get;set;}
}