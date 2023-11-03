using MediatR;

namespace Barbearia.Application.Features.TimesOff.Queries.GetTimeOffById;

public class GetTimeOffByIdQuery : IRequest<GetTimeOffByIdDto>
{
    public int TimeOffId {get; set;}
}