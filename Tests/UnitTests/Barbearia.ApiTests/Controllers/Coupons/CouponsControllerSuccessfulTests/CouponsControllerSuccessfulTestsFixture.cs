using AutoMapper;
using Barbearia.Api.Controllers;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Models.Coupons;
using Bogus;
using MediatR;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Barbearia.ApiTests.Controllers.Coupons.CouponsControllerSuccessfulTests;

public class CouponsControllerSuccessfulTestsFixture
{
    public AutoMocker Mocker;
    public CouponsController CouponsController;
    
    public GetCouponByIdDto GenerateValidGetCouponByIdDto()
    {
        return  new Faker<GetCouponByIdDto>()
            .CustomInstantiator(f => new GetCouponByIdDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public CouponDto GenerateValidGetCouponDto()
    {
        return  new Faker<CouponDto>()
            .CustomInstantiator(f => new CouponDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public IEnumerable<GetAllCouponsDto> GenerateValidGetAllCouponsDtos()
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
        return getAllCouponsDto.Generate(10);
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
    
    public CreateCouponCommand GenerateValidCreateCouponCommand()
    {
        return  new Faker<CreateCouponCommand>()
            .CustomInstantiator(f => new CreateCouponCommand()
            {
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public UpdateCouponCommandResponse GenerateValidUpdateCouponCommandResponse()
    {
        return  new Faker<UpdateCouponCommandResponse>()
            .CustomInstantiator(f => new UpdateCouponCommandResponse()
            {
                Errors = new (),
                ErrorType = null
            });
    }
    
    public UpdateCouponCommand GenerateValidUpdateCouponCommand()
    {
        return new Faker<UpdateCouponCommand>()
            .CustomInstantiator(f => new UpdateCouponCommand()
            {

                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }
    
    public CouponsController GenerateAndSetupCouponsController()
    {
        Mocker = new AutoMocker();

        CouponsController = Mocker.CreateInstance<CouponsController>();

        var mediatorMock = Mocker.GetMock<IMediator>();

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetCouponByIdQuery>(), CancellationToken.None))
            .ReturnsAsync(GenerateValidGetCouponByIdDto());
        
        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllCouponsQuery>(), CancellationToken.None))
            .ReturnsAsync(GenerateValidGetAllCouponsDtos());
        
        mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(GenerateValidCreateCouponCommandResponse());
        
        mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(GenerateValidUpdateCouponCommandResponse);
        
        return CouponsController;
    }
}