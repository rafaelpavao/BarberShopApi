namespace Barbearia.Application.Features.Payments.Queries.GetPayment;

public class GetPaymentQueryResponse : BaseResponse
{
    public GetPaymentDto? Payment {get;set;}
}