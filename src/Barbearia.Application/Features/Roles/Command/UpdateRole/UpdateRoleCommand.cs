using MediatR;

namespace Barbearia.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleCommand : IRequest<UpdateRoleCommandResponse>
{
    public int RoleId {get; set;}
    public string Name {get; set;} = string.Empty;
}
