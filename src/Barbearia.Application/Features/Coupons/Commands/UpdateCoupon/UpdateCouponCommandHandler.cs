using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;

public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, UpdateCouponCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCouponCommandHandler> _logger;

    public UpdateCouponCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateCouponCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<UpdateCouponCommandResponse> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var response = new UpdateCouponCommandResponse();

        var couponToUpdate = await _orderRepository.GetCouponByIdAsync(request.CouponId);

        if (couponToUpdate == null)
        {
            response.ErrorType = Error.NotFoundProblem;
            response.Errors.Add("CouponId", new[] { "Coupon not found for the specified CouponId." });
            return response;
        }

        var validator = new UpdateCouponCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            response.ErrorType = Error.ValidationProblem;
            response.FillErrors(validationResult);
            return response;
        }
        
        _mapper.Map(request, couponToUpdate);

        try
        {
            couponToUpdate.ValidateCoupon();
        }
        catch (Exception ex)
        {
            response.ErrorType = Error.InternalServerErrorProblem;
            response.Errors.Add("Coupon_Validation", new[] { "Error in coupon validation" });
            _logger.LogError(ex, "Erro de validação em update coupon");
            return response;
        }

        await _orderRepository.SaveChangesAsync();

        return response;
    }
}