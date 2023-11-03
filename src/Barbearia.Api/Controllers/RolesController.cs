using Barbearia.Application.Features.Roles.Commands.CreateRole;
using Barbearia.Application.Features.Roles.Commands.DeleteRole;
using Barbearia.Application.Features.Roles.Commands.UpdateRole;
using Barbearia.Application.Features.Roles.Queries.GetAllRoles;
using Barbearia.Application.Features.Roles.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/roles")]
public class RolesController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IMediator mediator, ILogger<RolesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllRolesDto>>> GetRoles()
    {
        var getAllRolesQuery = new GetAllRolesQuery{ };
        var rolesToReturn = await _mediator.Send(getAllRolesQuery);

        return Ok(rolesToReturn);
    }

    [HttpGet("{roleId}", Name ="GetRoleById")]
    public async Task<ActionResult<GetRoleByIdDto>> GetRoleById(int roleId){
        var getRoleById = new GetRoleByIdQuery { RoleId = roleId };
        var roleToReturn = await _mediator.Send(getRoleById);

        if(roleToReturn == null) return NotFound();

        return Ok(roleToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateRoleCommandResponse>> CreateRole (CreateRoleCommand createRoleCommand)
    {
        var CreateRoleCommandResponse = await _mediator.Send(createRoleCommand);

        if(!CreateRoleCommandResponse.IsSuccess)
            return HandleRequestError(CreateRoleCommandResponse);

        var roleForReturn = CreateRoleCommandResponse.Role;

        return CreatedAtRoute
        (
            "GetRoleById",
            new { roleId = roleForReturn.RoleId},
            roleForReturn
        );
    }

    [HttpPut("{roleId}")]
    public async Task<ActionResult> UpdateRole(int roleId, UpdateRoleCommand updateRoleCommand)
    {
        if (updateRoleCommand.RoleId != roleId) return BadRequest();

        var updateRoleCommandResponse = await _mediator.Send(updateRoleCommand);

        if (!updateRoleCommandResponse.IsSuccess)
            return HandleRequestError(updateRoleCommandResponse);
        
        return NoContent();
    }

    [HttpDelete("{roleId}")]
    public async Task<ActionResult> DeleteRole(int roleId)
    {
        var result = await _mediator.Send(new DeleteRoleCommand { RoleId = roleId });

        if (!result) return NotFound();

        return NoContent();
    }
}