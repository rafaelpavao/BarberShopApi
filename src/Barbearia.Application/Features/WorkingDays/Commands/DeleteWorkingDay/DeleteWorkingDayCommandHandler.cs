using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Commands.DeleteWorkingDay;

public class DeleteWorkingDayCommandHandler : IRequestHandler<DeleteWorkingDayCommand, DeleteWorkingDayCommandResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeleteWorkingDayCommandHandler(IPersonRepository personRepository){
        _personRepository = personRepository;
    }
    public async Task<DeleteWorkingDayCommandResponse> Handle(DeleteWorkingDayCommand request, CancellationToken cancellationToken)
    {
        DeleteWorkingDayCommandResponse response = new();

        var employeeFromDatabase = await _personRepository.GetEmployeeByIdAsync(request.PersonId);
        if(employeeFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PersonId", new[] { "Employee Not found in database" });
            return response;
        } 

        var workingDayFromDatabase = employeeFromDatabase.WorkingDays.FirstOrDefault(w => w.WorkingDayId == request.WorkingDayId);
        if(workingDayFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("WorkingDay", new[] { "WorkingDay Not found in database" });
            return response;
        } 

        _personRepository.DeleteWorkingDay(employeeFromDatabase, workingDayFromDatabase);

        await _personRepository.SaveChangesAsync();

        return response;
    }
}