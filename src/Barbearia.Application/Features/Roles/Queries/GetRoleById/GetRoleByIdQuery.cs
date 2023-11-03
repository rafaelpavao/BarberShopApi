using MediatR;

namespace Barbearia.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdQuery : IRequest<GetRoleByIdDto>
{
    public int RoleId {get;set;}
}