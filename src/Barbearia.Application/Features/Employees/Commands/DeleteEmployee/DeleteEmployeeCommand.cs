using MediatR;

namespace Barbearia.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommand : IRequest<bool>
{
    public int PersonId {get;set;}
}