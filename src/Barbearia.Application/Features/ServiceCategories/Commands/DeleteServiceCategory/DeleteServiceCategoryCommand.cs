using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Commands.DeleteServiceCategory;

public class DeleteServiceCategoryCommand: IRequest<bool>
{
    public int ServiceCategoryId {get;set;}
}
