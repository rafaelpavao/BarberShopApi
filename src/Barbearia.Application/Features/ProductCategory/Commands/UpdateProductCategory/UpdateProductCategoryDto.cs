namespace Barbearia.Application.Features.ProductCategories.Commands.UpdateProductCategory;

public class UpdateProductCategoryDto
{
    public int ProductCategoryId {get;set;}
    public string Name {get;set;} = string.Empty;
}