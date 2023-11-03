using Barbearia.Application.Contracts.Repositories;
using MediatR;

namespace Barbearia.Application.Features.Payments.Commands.DeletePayment;

public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, DeletePaymentCommandResponse>
{
    private readonly IOrderRepository _orderRepository;

    public DeletePaymentCommandHandler(IOrderRepository orderRepository){
        _orderRepository = orderRepository;
    }
    public async Task<DeletePaymentCommandResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        DeletePaymentCommandResponse response = new();

        var orderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if(orderFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("OrderId", new[] { "Order Not found in database"});
            return response;
        }

        var paymentFromDatabase = await _orderRepository.GetPaymentAsync(request.OrderId);
        if((paymentFromDatabase == null) || (paymentFromDatabase.PaymentId != request.PaymentId))
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("Payment", new[] { "Payment Not found in database" });
            return response;
        }

        _orderRepository.DeletePayment(orderFromDatabase, paymentFromDatabase);

        await _orderRepository.SaveChangesAsync();

        return response;
    }
}