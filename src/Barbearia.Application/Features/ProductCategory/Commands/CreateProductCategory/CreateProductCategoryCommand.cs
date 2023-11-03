using MediatR;
namespace Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommand : IRequest<CreateProductCategoryCommandResponse>
{
    public string Name {get;set;}= string.Empty;
}