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

namespace UnitTests.Barbearia.ApiTests.Controllers.Coupons.CouponsControllerSuccessfulTests;

public class CouponsControllerSuccessfulTests : IClassFixture<CouponsControllerSuccessfulTestsFixture>
{
    private CouponsControllerSuccessfulTestsFixture _fixture;
    private CouponsController _controller;
    private AutoMocker _mocker;

    public CouponsControllerSuccessfulTests(CouponsControllerSuccessfulTestsFixture fixture)
    {
        _fixture = fixture;
        _controller = _fixture.GenerateAndSetupCouponsController();
        _mocker = _fixture.Mocker;
    }
    
    [Fact(DisplayName = "Get Coupon By Id : When Coupon Is In Db Test")]
    [Trait("Category", "Coupon Controller Successful Tests")]
    public async void GetCouponById_WhenCouponIsInDb_ShouldReturnOkCoupon()
    {
        var coupon = _fixture.GenerateValidGetCouponByIdDto();

        var response = await _controller.GetCouponById(coupon.CouponId);
        var actionResult = Assert.IsType<ActionResult<CouponDto>>(response);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.IsAssignableFrom<GetCouponByIdDto>(okObjectResult.Value);
        
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<GetCouponByIdQuery>(), CancellationToken.None),
            Times.Once);
    }
    
    [Fact(DisplayName = "Get All Coupons : When Db Has Any Coupons Test")]
    [Trait("Category", "Coupon Controller Successful Tests")]
    public async void GetCoupons_WhenDbHasAnyCoupons_ShouldReturnOkCoupons()
    {
        var response = await _controller.GetCoupons();
        
        var actionResult = Assert.IsType<ActionResult<IEnumerable<CouponDto>>>(response);
        var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        Assert.IsAssignableFrom<IEnumerable<GetAllCouponsDto>>(okObjectResult.Value);
        
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<GetAllCouponsQuery>(), CancellationToken.None),
            Times.Once);
    }


    [Fact(DisplayName = "Create Coupon : When Command Is Successful Test")]
    [Trait("Category", "Coupon Controller Successful Tests")]
    public async Task CreateCoupon_WhenCommandIsSuccessful_ShouldReturnCreatedAtRouteCoupon()
    {
        var command = _fixture.GenerateValidCreateCouponCommand();

        var response = await _controller.CreateCoupon(command);
        
        var actionResult = Assert.IsType<ActionResult<CreateCouponCommandResponse>>(response);
        var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(actionResult.Result);
        var couponsValue = Assert.IsAssignableFrom<CreateCouponDto>(createdAtRouteResult.Value);
        var dictValue = createdAtRouteResult.RouteValues.Values.FirstOrDefault();
        
        Assert.Equal(dictValue, couponsValue.CouponId); // testing is created at route id is equal to value id
        
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<CreateCouponCommand>(), CancellationToken.None),
            Times.Once);
    }
    
    [Fact(DisplayName = "Update Coupon : When Params Are Valid And Command Is Successful Test")]
    [Trait("Category", "Coupon Controller Successful Tests")]
    public async Task UpdateCoupon_WhenParamsAreValidAndCommandIsSuccessful_ShouldReturnNoContent()
    {
        var command = _fixture.GenerateValidUpdateCouponCommand();

        var response = await _controller.UpdateCoupon(command.CouponId, command);
        
        Assert.IsType<NoContentResult>(response);
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<UpdateCouponCommand>(), CancellationToken.None),
            Times.Once);
    }
    
    [Fact(DisplayName = "Delete Coupon : When Mediator Result Is True Test")]
    [Trait("Category", "Coupon Controller Successful Tests")]
    public async Task DeleteCoupon_WhenMediatorResultIsTrue_ShouldReturnNoContent()
    {
        var command = new DeleteCouponCommand(){CouponId = 1};
        _mocker
            .GetMock<IMediator>()
            .Setup(m => m.Send(It.IsAny<DeleteCouponCommand>(), CancellationToken.None))
            .ReturnsAsync(true);
        
        var response = await _controller.DeleteCoupon(command.CouponId);
        
        Assert.IsType<NoContentResult>(response);
        _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<DeleteCouponCommand>(), CancellationToken.None),
            Times.Once);
    }
    
}