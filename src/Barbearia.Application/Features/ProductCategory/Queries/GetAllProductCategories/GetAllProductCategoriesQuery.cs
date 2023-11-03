using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Queries.GetAllProductCategories;

public class GetAllProductCategoriesQuery : IRequest<IEnumerable<GetAllProductCategoriesDto>>
{

}