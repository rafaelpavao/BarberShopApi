namespace Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryCommandResponse : BaseResponse
{
    public CreateProductCategoryDto ProductCategory{get; set;} = default!;    
}