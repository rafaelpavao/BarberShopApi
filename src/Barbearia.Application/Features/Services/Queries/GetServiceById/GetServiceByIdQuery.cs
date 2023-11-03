using MediatR;

namespace Barbearia.Application.Features.Services.Queries.GetServiceById;

public class GetServiceByIdQuery : IRequest<GetServiceByIdDto>
{
    public int ItemId {get;set;}
}