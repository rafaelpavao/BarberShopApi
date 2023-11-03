namespace Barbearia.Application.Features.ProductCategories.Commands.CreateProductCategory;

public class CreateProductCategoryDto
{
    public int ProductCategoryId {get;set;}
    public string Name {get;set;}= string.Empty;
}