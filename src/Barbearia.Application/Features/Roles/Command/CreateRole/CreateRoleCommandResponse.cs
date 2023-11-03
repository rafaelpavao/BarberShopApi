namespace Barbearia.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandResponse : BaseResponse
{
    public CreateRoleDto Role {get;set;} = default!;
}