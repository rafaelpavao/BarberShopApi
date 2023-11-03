using MediatR;

namespace Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;

public class GetServiceCategoryByIdQuery : IRequest<GetServiceCategoryByIdDto>
{
    public int ServiceCategoryId {get;set;}
}