using MediatR;

namespace Barbearia.Application.Features.ProductCategories.Commands.DeleteProductCategory;

public class DeleteProductCategoryCommand : IRequest<bool>
{
    public int ProductCategoryId {get;set;}
}