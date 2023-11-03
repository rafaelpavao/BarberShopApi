using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CreateCouponCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCouponCommandHandler> _logger;

    public CreateCouponCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<CreateCouponCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CreateCouponCommandResponse> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        CreateCouponCommandResponse response = new();
        var validator = new CreateCouponCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }

        if (await _orderRepository.CouponExists(request.CouponCode))
        {
            validationResult.Errors
                .Add(new ValidationFailure("Coupons",   "Coupon already exists and is still active." ));
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }
        
        var couponEntity = _mapper.Map<Coupon>(request);
        
        try
        {
            couponEntity.ValidateCoupon();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Coupon_Validation", new[] { "Error in coupon validation" });
            _logger.LogError(ex, "Erro de validação em create coupon");
            return response;
        }
        
        _orderRepository.AddCoupon(couponEntity);
        await _orderRepository.SaveChangesAsync();

        response.Coupon = _mapper.Map<CreateCouponDto>(couponEntity);
        return response;

    }
}