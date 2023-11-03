using Barbearia.Application.Models;
using MediatR;


namespace Barbearia.Application.Features.CustomersCollection;

public class GetCustomersCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetCustomersCollectionDto> Customers{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetCustomersCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}