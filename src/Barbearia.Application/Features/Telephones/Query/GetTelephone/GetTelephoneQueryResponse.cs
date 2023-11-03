namespace Barbearia.Application.Features.Telephones.Query.GetTelephone;

public class GetTelephoneQueryResponse : BaseResponse
{
    public IEnumerable<GetTelephoneDto>? Telephones {get;set;}
}