using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Domain.Entities;
using Bogus;

namespace UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

public class CouponsTestsDataGeneratingFixture
{
    // HANDLERS UTIL

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
    
    //---------------------
    // GET ALL COUPONS|||||
    //---------------------
    
    public List<GetAllCouponsDto> GenerateValidGetAllCouponsDtos( int quantity)
    {
        var getAllCouponsDto = new Faker<GetAllCouponsDto>()
            .CustomInstantiator(f => new GetAllCouponsDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
        return getAllCouponsDto.Generate(quantity);
    }  
    
    //---------------------
    // GET COUPON BY ID||||
    //---------------------
    
    public GetCouponByIdDto GenerateValidGetCouponByIdDto()
    {
        return new Faker<GetCouponByIdDto>()
            .CustomInstantiator(f => new GetCouponByIdDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }  
        
    //---------------------
    //CREATE COUPON||||||||
    //---------------------
    
    
    public CreateCouponDto GenerateValidCreateCouponDto()
    {
        return new Faker<CreateCouponDto>()
            .CustomInstantiator(f => new CreateCouponDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }

    public CreateCouponCommand GenerateValidCreateCouponCommand()
    {
        return new Faker<CreateCouponCommand>()
            .CustomInstantiator(f => new CreateCouponCommand()
            {
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public CreateCouponCommand GenerateInvalidCreateCouponCommand()
    {
        return new Faker<CreateCouponCommand>()
            .CustomInstantiator(f => new CreateCouponCommand()
            {
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    
    public CreateCouponCommandResponse GenerateValidCreateCouponCommandResponse()
    {
        return  new Faker<CreateCouponCommandResponse>()
            .CustomInstantiator(f => new CreateCouponCommandResponse()
            {
                Coupon = new()
                {
                    CouponCode = f.Commerce.Ean8(),
                    CouponId = f.UniqueIndex + 1,
                    CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                    DiscountPercent = f.Random.Int(1, 100),
                    ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
                },
                Errors = new (),
                ErrorType = null
            });
    }
    
    //---------------------
    //DELETE COUPON||||||||
    //---------------------
    
    public DeleteCouponCommand GenerateValidDeleteCouponCommand()
    {
        return new Faker<DeleteCouponCommand>()
            .CustomInstantiator(f => new DeleteCouponCommand()
            {
                CouponId = f.UniqueIndex+1
            });
    }
    
    
    //---------------------
    //DELETE COUPON||||||||
    //---------------------
    
    public UpdateCouponCommand GenerateValidUpdateCouponCommand()
    {
        return new Faker<UpdateCouponCommand>()
            .CustomInstantiator(f => new UpdateCouponCommand()
            {
                CouponId = f.UniqueIndex+1,
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public UpdateCouponCommand GenerateInvalidUpdateCouponCommand()
    {
        return new Faker<UpdateCouponCommand>()
            .CustomInstantiator(f => new UpdateCouponCommand()
            {
                CouponId = f.UniqueIndex+1,
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public UpdateCouponDto GenerateInvalidUpdateCouponDto()
    {
        return new Faker<UpdateCouponDto>()
            .CustomInstantiator(f => new UpdateCouponDto()
            {
                CouponId = f.UniqueIndex+1,
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(1))
            });
    }
}