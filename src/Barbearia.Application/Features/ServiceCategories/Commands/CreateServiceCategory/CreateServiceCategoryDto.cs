namespace Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;

public class CreateServiceCategoryDto
{
    public int ServiceCategoryId {get; set;}
    public string Name {get; set;} = string.Empty;
}