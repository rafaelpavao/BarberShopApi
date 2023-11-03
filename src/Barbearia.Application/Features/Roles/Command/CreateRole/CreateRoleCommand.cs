using MediatR;

namespace Barbearia.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommand : IRequest<CreateRoleCommandResponse>
{
    public string Name{get; set;} = string.Empty;
}
