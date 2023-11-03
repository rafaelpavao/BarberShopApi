namespace Barbearia.Application.Features.ServicesCollection;
public class GetServicesCollectionQueryResponse : BaseResponse
{
    public IEnumerable<GetServicesCollectionDto> Services{ get; set; } = default!;
    public PaginationMetadata? PaginationMetadata { get; set; }
    public GetServicesCollectionQueryResponse()
    {
        Errors = new Dictionary<string, string[]>();
    }
}