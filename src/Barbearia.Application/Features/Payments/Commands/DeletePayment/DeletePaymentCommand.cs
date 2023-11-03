using MediatR;

namespace Barbearia.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentCommand : IRequest<DeletePaymentCommandResponse>{
    public int OrderId {get;set;}
    public int PaymentId {get;set;}
}