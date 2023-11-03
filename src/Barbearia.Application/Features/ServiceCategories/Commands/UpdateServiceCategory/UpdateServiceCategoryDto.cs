namespace Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryDto
{
    public int ServiceCategoryId {get; set;}
    public string Name {get; set;} = string.Empty;
}