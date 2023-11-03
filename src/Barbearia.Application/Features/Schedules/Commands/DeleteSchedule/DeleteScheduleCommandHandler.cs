using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Schedules.Commands.DeleteSchedule;

public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, bool>
{
    private readonly IPersonRepository _personRepository;

    public DeleteScheduleCommandHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
    {
        var scheduleFromDatabase = await _personRepository.GetScheduleByIdAsync(request.ScheduleId);
        if(scheduleFromDatabase == null) return false;
        _personRepository.DeleteSchedule(scheduleFromDatabase);

        return await _personRepository.SaveChangesAsync();
    }
}