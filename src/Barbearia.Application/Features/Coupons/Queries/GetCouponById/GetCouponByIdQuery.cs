using MediatR;

namespace Barbearia.Application.Features.Coupons.Queries.GetCouponById;

public class GetCouponByIdQuery : IRequest<GetCouponByIdDto>
{
    public int CouponId { get; set; }
}