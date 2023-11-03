using AutoMapper;
using Barbearia.Api.Controllers;
using Barbearia.Application.Features;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Models.Coupons;
using Bogus;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Barbearia.ApiTests.Controllers.Coupons.CouponsControllerFaliureTests;

public class CouponsControllerFaliureTestsFixture
{
    public AutoMocker Mocker;
    public CouponsController CouponsController;

    public GetCouponByIdDto GenerateInvalidGetCouponByIdDto()
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

    public CouponDto GenerateInvalidGetCouponDto()
    {
        return new Faker<CouponDto>()
            .CustomInstantiator(f => new CouponDto()
            {
                CouponCode = f.Commerce.Ean8(),
                CouponId = f.UniqueIndex + 1,
                CreationDate = f.Date.Past(2, DateTime.UtcNow.AddDays(-1)),
                DiscountPercent = f.Random.Int(1, 100),
                ExpirationDate = f.Date.Future(2, DateTime.UtcNow.AddDays(1))
            });
    }

    public IEnumerable<GetAllCouponsDto> GenerateInvalidGetAllCouponsDtos()
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

    public CreateCouponCommandResponse GenerateInvalidCreateCouponCommandResponse()
    {
        return new Faker<CreateCouponCommandResponse>()
            .CustomInstantiator(f => new CreateCouponCommandResponse()
            {
                Coupon = new CreateCouponDto()
                {
                    CouponCode = f.Commerce.Ean8(),
                    CouponId = f.UniqueIndex + 1,
                    CreationDate = f.Date.Future(10),
                    DiscountPercent = f.Random.Int(100, 1000),
                    ExpirationDate = f.Date.Past(10)
                },
                Errors = new Dictionary<string, string[]>
                {
                    { "ExpirationDate", new[]
                    { "Expiration Date must be after creation date.",
                        "'Expiration Date' must be greater than '07/10/2023 01:46:01'." } }
                },
                ErrorType = Error.BadRequestProblem
            });
    }

    public CreateCouponCommand GenerateInvalidCreateCouponCommand()
    {
        return new Faker<CreateCouponCommand>()
            .CustomInstantiator(f => new CreateCouponCommand()
            {
                CouponCode = f.Commerce.Ean8(),
                CreationDate = f.Date.Future(10),
                DiscountPercent = f.Random.Int(100, 1000),
                ExpirationDate = f.Date.Past(10)
            });
    }

    public UpdateCouponCommandResponse GenerateInvalidUpdateCouponCommandResponse()
    {
        return new Faker<UpdateCouponCommandResponse>()
            .CustomInstantiator(f => new UpdateCouponCommandResponse()
            {
                Errors = new(),
                ErrorType = null
            });
    }

    public UpdateCouponCommand GenerateInvalidUpdateCouponCommand()
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

        var mediatorMock = new Mock<IMediator>();

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetCouponByIdQuery>(), CancellationToken.None))
            .ReturnsAsync((GetCouponByIdDto?)null);

        mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAllCouponsQuery>(), CancellationToken.None))
            .ReturnsAsync(new List<GetAllCouponsDto>());

        mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(GenerateInvalidCreateCouponCommandResponse());

        mediatorMock
            .Setup(m => m.Send(It.IsAny<UpdateCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(GenerateInvalidUpdateCouponCommandResponse);

        // Mocker.GetMock<MainController>().Setup(c => c.HandleRequestError(It.IsAny<CreateCouponCommandResponse>()))
        //     .Returns(new BadRequestObjectResult(GenerateInvalidCreateCouponCommandResponse().Errors));

        // CouponsController = new CouponsController(mediatorMock.Object);

        return CouponsController;
    }
}