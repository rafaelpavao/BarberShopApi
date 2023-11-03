using MediatR;

namespace Barbearia.Application.Features.TimesOff.Commands.DeleteTimeOff;

public class DeleteTimeOffCommand : IRequest<bool>
{
    public int TimeOffId {get;set;}
}