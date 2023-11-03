using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.TimesOff.Commands.UpdateTimeOff;

public class UpdateTimeOffCommand : IRequest<UpdateTimeOffCommandResponse>
{
    public int TimeOffId{get; set;}
    public int WorkingDayId{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}