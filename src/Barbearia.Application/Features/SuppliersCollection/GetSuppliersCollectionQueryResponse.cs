using Barbearia.Application.Models;
using MediatR;


namespace Barbearia.Application.Features.SuppliersCollection;

public class GetSuppliersCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetSuppliersCollectionDto> Suppliers{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetSuppliersCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}