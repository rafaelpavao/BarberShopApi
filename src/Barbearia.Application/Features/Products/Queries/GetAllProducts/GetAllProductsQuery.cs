using MediatR;

namespace Barbearia.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<GetAllProductsDto>>
{

}