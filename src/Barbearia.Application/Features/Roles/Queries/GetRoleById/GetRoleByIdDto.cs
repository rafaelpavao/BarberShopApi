using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Features.Roles.Queries.GetRoleById;

public class GetRoleByIdDto
{
    public int RoleId{get; set;}
    public string Name{get; set;} = string.Empty;
    public IEnumerable<EmployeeDto>? Employees{get; set;}
}