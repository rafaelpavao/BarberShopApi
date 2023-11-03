using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Payments.Commands.CreatePayment;


public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CreatePaymentCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePaymentCommandHandler> _logger;



    public CreatePaymentCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreatePaymentCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        CreatePaymentCommandResponse response = new();

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

        if (orderFromDatabase.Payment != null)
        {
            response.ErrorType = Error.ValidationProblem;
            response.Errors.Add("Payment", new[] { "Order already has a payment" });
            return response;
        }

        var validator = new CreatePaymentCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        var paymentEntity = _mapper.Map<Payment>(request);

        try
        {
            paymentEntity.ValidatePayment();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Payment_Validation", new[] { "Error in payment validation" });
            _logger.LogError(ex, "erro de validação em create payment");
            return response;
        }

        _orderRepository.AddPayment(orderFromDatabase, paymentEntity);
        await _orderRepository.SaveChangesAsync();

        response.Payment = _mapper.Map<CreatePaymentDto>(paymentEntity);
        return response;
    }
}