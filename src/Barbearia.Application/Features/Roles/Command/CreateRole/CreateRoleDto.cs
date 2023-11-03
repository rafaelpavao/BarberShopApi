namespace Barbearia.Application.Features.Roles.Commands.CreateRole;

public class CreateRoleDto
{
    public int RoleId {get; set;}
    public string Name {get; set;} = string.Empty;
}