using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Commands.CreateServiceCategory;

public class CreateServiceCategoryCommand : IRequest<CreateServiceCategoryCommandResponse>
{
    public string Name{get; set;} = string.Empty;
}
