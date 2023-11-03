using Barbearia.Application.Features.Employees.Commands.CreateEmployee;
using Barbearia.Application.Features.Employees.Commands.DeleteEmployee;
using Barbearia.Application.Features.Employees.Commands.UpdateEmployee;
using Barbearia.Application.Features.Employees.Queries.GetEmployeeById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Barbearia.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeesController : MainController
{
    private readonly IMediator _mediator;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IMediator mediator, ILogger<EmployeesController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger;
    }

    [HttpGet("{EmployeeId}", Name = "GetEmployeeById")]
    public async Task<ActionResult<GetEmployeeByIdDto>> GetEmployeeById(int employeeId)
    {
        var getEmployeeByIdQuery = new GetEmployeeByIdQuery { PersonId = employeeId };

        var employeeToReturn = await _mediator.Send(getEmployeeByIdQuery);

        if (employeeToReturn == null) return NotFound();

        return Ok(employeeToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateEmployeeCommandResponse>> CreateEmployee(CreateEmployeeCommand createEmployeeCommand)
    {
        var createEmployeeCommandResponse = await _mediator.Send(createEmployeeCommand);


        if (!createEmployeeCommandResponse.IsSuccess)
            return HandleRequestError(createEmployeeCommandResponse);

        var employeeToReturn = createEmployeeCommandResponse.Employee;


        return CreatedAtRoute
        (
            "GetEmployeeById",
            new { EmployeeId = employeeToReturn.PersonId },
            employeeToReturn
        );
    }

    [HttpPut("{EmployeeId}")]
    public async Task<ActionResult> UpdateEmployee(int employeeId, UpdateEmployeeCommand updateEmployeeCommand)
    {
        if (updateEmployeeCommand.PersonId != employeeId) return BadRequest();

        var updateEmployeeCommandResponse = await _mediator.Send(updateEmployeeCommand);

        if (!updateEmployeeCommandResponse.IsSuccess)
            return HandleRequestError(updateEmployeeCommandResponse);

        return NoContent();
    }

    [HttpDelete("{EmployeeId}")]
    public async Task<ActionResult> DeleteEmployee(int employeeId)
    {
        var result = await _mediator.Send(new DeleteEmployeeCommand { PersonId = employeeId });

        if (!result) return NotFound();

        return NoContent();
    }

}