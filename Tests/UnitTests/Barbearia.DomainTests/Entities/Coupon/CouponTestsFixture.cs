using Barbearia.Domain.Entities;
using Bogus;

namespace UnitTests.Barbearia.DomainTests;

public class CouponTestsFixture
{
    public List<Coupon> GenerateValidCoupons(int quantity)
    {
        var coupon = new Faker<Coupon>().CustomInstantiator(f => new Coupon()
        {
            CouponId = f.UniqueIndex + 1,
            CouponCode = f.Commerce.Ean8(),
            CreationDate = f.Date.Past(1, DateTime.UtcNow),
            DiscountPercent = f.Random.Int(1, 100),
            ExpirationDate = f.Date.Future(1, DateTime.UtcNow)
        });

        return coupon.Generate(quantity);
    }

    public List<Coupon> GenerateCouponsWithInvalidCreationDate(int quantity)
    {
        var coupon = new Faker<Coupon>().CustomInstantiator(f => new Coupon()
        {
            CouponId = f.UniqueIndex + 1,
            CouponCode = f.Commerce.Ean8(),
            CreationDate = f.Date.Future(5, DateTime.UtcNow.AddYears(3)),
            DiscountPercent = f.Random.Int(1, 100),
            ExpirationDate = f.Date.Future(1, DateTime.UtcNow)
        });

        return coupon.Generate(quantity);
    }

    public List<Coupon> GenerateCouponsWithInvalidExpirationDate(int quantity)
    {
        var coupon = new Faker<Coupon>().CustomInstantiator(f => new Coupon()
        {
            CouponId = f.UniqueIndex + 1,
            CouponCode = f.Commerce.Ean8(),
            CreationDate = f.Date.Past(1, DateTime.UtcNow),
            DiscountPercent = f.Random.Int(1, 100),
            ExpirationDate = f.Date.Past(4, DateTime.UtcNow.AddYears(-2))
        });

        return coupon.Generate(quantity);
    }

    public List<Coupon> GenerateCouponsWithInvalidDiscount(int quantity)
    {
        var coupon = new Faker<Coupon>().CustomInstantiator(f => new Coupon()
        {
            CouponId = f.UniqueIndex + 1,
            CouponCode = f.Commerce.Ean8(),
            CreationDate = f.Date.Past(1, DateTime.UtcNow),
            DiscountPercent = f.Random.Int(101, 110),
            ExpirationDate = f.Date.Past(1, DateTime.UtcNow)
        });

        return coupon.Generate(quantity);
    }
}