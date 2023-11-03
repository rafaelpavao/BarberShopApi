

namespace Barbearia.Application.Features.TimesOff.Commands.CreateTimeOff;
public class CreateTimeOffCommandResponse : BaseResponse
{
    public CreateTimeOffDto TimeOff {get; set;} = default!;    
}