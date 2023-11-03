namespace Barbearia.Application.Features.ProductsCollection;
public class GetProductsCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetProductsCollectionDto> Products{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetProductsCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}