namespace Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;

public class CreateServiceCategoryCommandResponse : BaseResponse
{
    public CreateServiceCategoryDto ServiceCategory{get;set;} = default!;
}