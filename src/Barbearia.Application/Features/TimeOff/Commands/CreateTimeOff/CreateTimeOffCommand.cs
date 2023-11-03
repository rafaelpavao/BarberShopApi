
using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;
public class CreateTimeOffCommand : IRequest<CreateTimeOffCommandResponse>
{
    public int WorkingDayId{get;set;}
    public TimeOnly StartTime{get;set;}
    public TimeOnly FinishTime{get;set;}
}