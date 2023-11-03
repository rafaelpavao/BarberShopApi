using AutoMapper;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Domain.Entities;

namespace Barbearia.Application.Profiles;

public class CouponProfile : Profile
{

    public CouponProfile()
    {
        CreateMap<Coupon, GetCouponByIdDto>().ReverseMap();
        CreateMap<Coupon, GetAllCouponsDto>().ReverseMap();
        CreateMap<CreateCouponCommand, Coupon>();
        CreateMap<Coupon, CreateCouponDto>();
        CreateMap<UpdateCouponCommand, Coupon>();
    }
    
}