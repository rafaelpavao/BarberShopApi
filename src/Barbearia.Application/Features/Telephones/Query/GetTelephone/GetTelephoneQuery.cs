using MediatR;

namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQuery : IRequest<GetTelephoneQueryResponse>
{
    public int PersonId {get; set;}
}