using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Models;
using MediatR;

namespace Barbearia.Application.Features.Payments.Queries.GetPayment;

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, GetPaymentQueryResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetPaymentQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<GetPaymentQueryResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        GetPaymentQueryResponse response = new();

        var orderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if(orderFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("OrderId", new[] { "Order Not found in database"});
            return response;
        }

        var paymentFromDatabase = orderFromDatabase.Payment;
        if(paymentFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("Payment", new[] { "Payment Not found in database" });
            return response;
        }

        response.Payment = _mapper.Map<GetPaymentDto>(paymentFromDatabase);
        return response;
    }
}