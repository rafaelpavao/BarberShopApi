using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.WorkingDays.Commands.CreateWorkingDay;

public class CreateWorkingDayCommand : IRequest<CreateWorkingDayCommandResponse>
{
    public int PersonId{get; set;}
    public DateOnly WorkDate{get; set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
    // public TimeOffForCreateWorkingDayDto? TimeOff;
}