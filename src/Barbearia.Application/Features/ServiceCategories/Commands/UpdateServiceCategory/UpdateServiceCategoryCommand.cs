using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Commands.UpdateServiceCategory;

public class UpdateServiceCategoryCommand : IRequest<UpdateServiceCategoryCommandResponse>
{
    public int ServiceCategoryId {get; set;}
    public string Name {get; set;} = string.Empty;
}