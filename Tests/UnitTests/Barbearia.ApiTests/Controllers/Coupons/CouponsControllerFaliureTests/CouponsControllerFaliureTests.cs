using Barbearia.Api.Controllers;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Application.Features.Coupons.Queries.GetAllCoupons;
using Barbearia.Application.Features.Coupons.Queries.GetCouponById;
using Barbearia.Application.Models.Coupons;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;

namespace UnitTests.Barbearia.ApiTests.Controllers.Coupons.CouponsControllerFaliureTests;

public class CouponsControllerFaliureTests : IClassFixture<CouponsControllerFaliureTestsFixture>
{
    private CouponsControllerFaliureTestsFixture _fixture;
    private CouponsController _controller;
    private AutoMocker _mocker;

    public CouponsControllerFaliureTests(CouponsControllerFaliureTestsFixture fixture)
    {
        _fixture = fixture;
        _controller = _fixture.GenerateAndSetupCouponsController();
        _mocker = _fixture.Mocker;
    }
    
    [Fact(DisplayName = "Get Coupon By Id : When Mediator Response Is Null Test")]
    [Trait("Category", "Coupon Controller Tests")]
    public async void GetCouponById_WhenMediatorResponseIsNull_ShouldReturnNotFound()
    {
        var response = await _controller.GetCouponById(1);
        
        var actionResult = Assert.IsType<ActionResult<CouponDto>>(response);
        Assert.IsType<NotFoundResult>(actionResult.Result);
        
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<GetCouponByIdQuery>(), CancellationToken.None),
            Times.Once);
    }
    
    [Fact(DisplayName = "Get All Coupons : Db Has No Coupons Test")]
    [Trait("Category", "Coupon Controller Tests")]
    public async void GetCoupons_DbHasNoCoupons_ShouldReturnNotFound()
    {
        var response = await _controller.GetCoupons();
        
        var actionResult = Assert.IsType<ActionResult<IEnumerable<CouponDto>>>(response);
        Assert.IsType<NotFoundResult>(actionResult.Result);
        
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<GetAllCouponsQuery>(), CancellationToken.None),
            Times.Once);
    }


    // [Fact(DisplayName = "Create Coupon : When Coupon Is Invalid Test")]
    // [Trait("Category", "Coupon Controller Tests")]
    // public async Task CreateCoupon_WhenCouponIsInvalid_ShouldReturnCreatedAtRouteCoupon()
    // {
    //     var command = _fixture.GenerateInvalidCreateCouponCommand();
    //     // var responsee = _fixture.GenerateInvalidCreateCouponCommandResponse();
    //
    //     var response = await _controller.CreateCoupon(command);
    //     
    //     var actionResult = Assert.IsType<ActionResult<CreateCouponCommandResponse>>(response);
    //     var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
    //     var couponsValue = Assert.IsAssignableFrom<CreateCouponDto>(createdAtRouteResult.Value);
    //     var dictValue = createdAtRouteResult.RouteValues.Values.FirstOrDefault();
    //     
    //     Assert.Equal(dictValue, couponsValue.CouponId); // testing is created at route id is equal to value id
    //     
    //     _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<CreateCouponCommand>(), CancellationToken.None),
    //         Times.Once);
    // }
    
    // [Fact(DisplayName = "Update Coupon Faliure Test")]
    // [Trait("Category", "Coupon Controller Tests")]
    // public async Task UpdateCoupon_WhenCouponIsInvalid_ShouldReturnNoContent()
    // {
    //     var command = _fixture.GenerateInvalidUpdateCouponCommand();
    //
    //     var response = await _controller.UpdateCoupon(command.CouponId, command);
    //     
    //     var actionResult = Assert.IsType<NoContentResult>(response);
    //     _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<UpdateCouponCommand>(), CancellationToken.None),
    //         Times.Once);
    // }
    
    
    [Fact(DisplayName = "Delete Coupon : When Mediator Result Is False Test")]
    [Trait("Category", "Coupon Controller Tests")]
    public async Task DeleteCoupon_WhenMediatorResultIsFalse_ShouldReturnNotFound()
    {
        var command = new DeleteCouponCommand(){CouponId = 1};
        _mocker
            .GetMock<IMediator>()
            .Setup(m => m.Send(It.IsAny<DeleteCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(false);
        
        var response = await _controller.DeleteCoupon(command.CouponId);

        var actionResult = Assert.IsType<NotFoundResult>(response);
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<DeleteCouponCommand>(), CancellationToken.None),
            Times.Once);
    }
    
}