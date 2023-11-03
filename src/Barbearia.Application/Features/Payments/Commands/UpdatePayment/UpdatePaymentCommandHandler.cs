using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Payments.Commands.UpdatePayment;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, UpdatePaymentCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<UpdatePaymentCommand> _logger;

    public UpdatePaymentCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdatePaymentCommand> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdatePaymentCommandResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        UpdatePaymentCommandResponse response = new();

        var orderFromDatabase = await _orderRepository.GetOrderByIdAsync(request.OrderId);
        if (orderFromDatabase == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("OrderId", new[] { "Order not found in the database." });
            return response;
        }

        if (request.CouponId.HasValue)
        {
            var couponFromDatabase = await _orderRepository.GetCouponByIdAsync(request.CouponId.Value);

            if (couponFromDatabase == null)
            {
                response.ErrorType = Error.NotFoundProblem;
                response.Errors.Add("CouponId", new[] { "Coupon not found in the database." });
                return response;
            }
        }

        var orderToUpdate = orderFromDatabase.Payment;

        if (orderToUpdate == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("PaymentId", new[] { "Payment not found for the specified orderId." });
            return response;
        }

        var validator = new UpdatePaymentCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var paymentEntity = _mapper.Map(request, orderToUpdate);

        try
        {
            paymentEntity.ValidatePayment();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Payment_Validation", new[] { "Error in payment validation" });
            _logger.LogError(ex, "erro de validação em update payment");
            return response;
        }

        await _orderRepository.SaveChangesAsync();

        return response;
    }
}