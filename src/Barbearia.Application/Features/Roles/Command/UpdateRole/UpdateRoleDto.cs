namespace Barbearia.Application.Features.Roles.Commands.UpdateRole;

public class UpdateRoleDto
{
    public int RoleId {get; set;}
    public string Name {get; set;} = string.Empty;
}