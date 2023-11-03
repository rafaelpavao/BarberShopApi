namespace Barbearia.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandResponse : BaseResponse
{
    public CreateEmployeeDto Employee {get; set;} = default!;    
}