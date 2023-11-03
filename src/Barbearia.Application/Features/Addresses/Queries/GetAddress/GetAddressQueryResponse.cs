using Barbearia.Application.Models;

namespace Barbearia.Application.Features.Addresses.Queries.GetAddress;

public class GetAddressQueryResponse : BaseResponse
{
    public IEnumerable<GetAddressDto>? Addresses {get;set;}
}