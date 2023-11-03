namespace Barbearia.Application.Features.Coupons.Commands.CreateCoupon;

public class CreateCouponDto
{
    public int CouponId { get; set; }
    public string CouponCode { get; set; } = string.Empty;
    public int DiscountPercent { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime ExpirationDate { get; set; }
}