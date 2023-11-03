using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features.Coupons.Commands.DeleteCoupon;
using Barbearia.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

namespace UnitTests.Barbearia.ApplicationTests.Features.Coupons.Commands.DeleteCoupon.SuccessfulTests;


[Collection(nameof(CouponsTestsCollection))]
public class DeleteCouponCommandHandlerTests : IClassFixture<CouponsTestsDataGeneratingFixture>
{
    
    private readonly CouponsTestsCollectionFixture _fixture;
    private readonly CouponsTestsDataGeneratingFixture _dataFixture;
    private Mock<IOrderRepository> _repo;
    private DeleteCouponCommandHandler _handler;

    public DeleteCouponCommandHandlerTests(CouponsTestsCollectionFixture fixture, CouponsTestsDataGeneratingFixture dataFixture)
    {
        _fixture = fixture;
        _dataFixture = dataFixture;
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _handler = _fixture.GenerateDeleteCouponCommandHandler(_repo.Object);
    }
    
    [Fact(DisplayName = "When Coupon Is In Db : Should Return True")]
    [Trait("Category", "Delete Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsInDb_ShouldReturnTrue()
    {
        var command = _dataFixture.GenerateValidDeleteCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeTrue();
    }
    
    [Fact(DisplayName = "When Coupon Is Not In Db : Should Return False")]
    [Trait("Category", "Delete Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDb_ShouldReturnFalse()
    {
        var command = _dataFixture.GenerateValidDeleteCouponCommand();
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _repo.Setup(r => r.GetCouponByIdAsync(command.CouponId)).ReturnsAsync((Coupon?)null);
        _handler = _fixture.GenerateDeleteCouponCommandHandler(_repo.Object);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeFalse();
    }
    
    [Fact(DisplayName = "When Coupon Is In Db : Should Execute Dependencies Correctly")]
    [Trait("Category", "Delete Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsInDb_ShouldExecuteDependenciesCorrectly()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _handler = _fixture.GenerateDeleteCouponCommandHandler(_repo.Object);
        var command = _dataFixture.GenerateValidDeleteCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);
        
        _repo.Verify(r => r.GetCouponByIdAsync(It.IsAny<int>()), Times.Once);
        _repo.Verify(r => r.DeleteCoupon(It.IsAny<Coupon>()), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}