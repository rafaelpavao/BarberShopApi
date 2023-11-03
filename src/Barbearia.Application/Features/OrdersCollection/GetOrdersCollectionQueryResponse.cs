namespace Barbearia.Application.Features.OrdersCollection;

public class GetOrdersCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetOrdersCollectionDto> Orders{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetOrdersCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}