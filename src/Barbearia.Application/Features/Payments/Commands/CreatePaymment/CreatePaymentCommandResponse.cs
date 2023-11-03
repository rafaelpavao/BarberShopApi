namespace Barbearia.Application.Features.Payments.Commands.CreatePayment;
public class CreatePaymentCommandResponse : BaseResponse
{
    public CreatePaymentDto Payment {get;set;} = default!;
}