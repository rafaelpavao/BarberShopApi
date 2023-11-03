using Barbearia.Application.Models;

namespace Barbearia.Application.Features.ServiceCategories.Queries.GetServiceCategoryById;

public class GetServiceCategoryByIdDto
{
    public int ServiceCategoryId{get;set;}
    public string Name{get; set;} = string.Empty;
    public List<ServiceForServiceCategoryQueriesDto>Services{get;set;} = new();
    // public List<Role> Roles {get;set;} = new();
    // public List<RoleServiceCategoryForServiceCategoryQueriesDto> Roles {get;set;} = new();
}
