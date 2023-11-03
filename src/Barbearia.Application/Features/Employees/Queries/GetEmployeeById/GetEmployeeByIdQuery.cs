using MediatR;

namespace Barbearia.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQuery : IRequest<GetEmployeeByIdDto>
{
    public int PersonId {get; set;}
}