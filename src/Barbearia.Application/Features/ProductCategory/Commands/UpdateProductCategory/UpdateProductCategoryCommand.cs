using MediatR;
namespace Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryCommand : IRequest<UpdateProductCategoryCommandResponse>
{
    public int ProductCategoryId {get;set;}
    public string Name {get;set;} = string.Empty;
}