namespace Barbearia.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponCommandResponse : BaseResponse
{
    public CreateCouponDto Coupon { get; set; } = default!;
}