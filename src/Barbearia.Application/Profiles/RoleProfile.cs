using AutoMapper;
using Barbearia.Application.Features.Roles.Commands.CreateRole;
using Barbearia.Application.Features.Roles.Commands.UpdateRole;
using Barbearia.Application.Features.Roles.Queries.GetAllRoles;
using Barbearia.Application.Features.Roles.Queries.GetRoleById;
using Barbearia.Application.Models;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {  
        CreateMap<UpdateRoleCommand, Role>(); 
        CreateMap<Role,UpdateRoleDto>();

        CreateMap<Role, GetAllRolesDto>();
        CreateMap<Role,GetRoleByIdDto>().ReverseMap();

        CreateMap<CreateRoleCommand, Role>();
        CreateMap<Role, CreateRoleDto>();

        CreateMap<Role, RoleDto>();
    }
}