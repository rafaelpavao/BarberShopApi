using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Queries.GetProductCategoryById;

public class GetProductCategoryByIdQuery : IRequest<GetProductCategoryByIdDto>
{
    public int ProductCategoryId {get;set;}
}