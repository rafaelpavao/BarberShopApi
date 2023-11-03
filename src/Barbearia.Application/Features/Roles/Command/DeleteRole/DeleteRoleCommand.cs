using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbearia.Application.Features.Roles.Commands.DeleteRole;

public class DeleteRoleCommand : IRequest<bool>
{
    public int RoleId {get;set;}
}
