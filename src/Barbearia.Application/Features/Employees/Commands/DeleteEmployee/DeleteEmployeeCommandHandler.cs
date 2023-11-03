using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public DeleteEmployeeCommandHandler(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);

        if (employeeFromDatabase == null) return false;

        _personRepository.DeleteEmployee(employeeFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}