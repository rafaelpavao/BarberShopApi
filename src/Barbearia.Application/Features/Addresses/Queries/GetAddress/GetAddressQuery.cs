using MediatR;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQuery : IRequest<GetAddressQueryResponse>
{
    public int PersonId { get; set; }
}