using MediatR;

namespace Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;

public class DeleteCouponCommand : IRequest<bool>
{
    public int CouponId { get; set; }
}