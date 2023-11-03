using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Application.Features.Coupons.Commands.UpdateCoupon;
using Barbearia.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

namespace UnitTests.Barbearia.ApplicationTests.Features.Coupons.Commands.UpdateCoupon;

[Collection(nameof(CouponsTestsCollection))]
public class UpdateCouponCommandHandlerTests : IClassFixture<CouponsTestsDataGeneratingFixture>
{
    private readonly CouponsTestsCollectionFixture _fixture;
    private readonly CouponsTestsDataGeneratingFixture _dataFixture;
    private Mock<IOrderRepository> _repo;
    private Mock<IMapper> _mapper;
    private UpdateCouponCommandHandler _handler;

    public UpdateCouponCommandHandlerTests(CouponsTestsCollectionFixture fixture, CouponsTestsDataGeneratingFixture dataFixture)
    {
        _fixture = fixture;
        _dataFixture = dataFixture;
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateUpdateCouponCommandHandler(_repo.Object, _mapper.Object);
    }
    
    [Fact(DisplayName = "When Coupon Is Not In Db And Validation Passes : Should Return Update Coupon Response With Value And No Errors")]
    [Trait("Category", "Update Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDbAndValidationPasses_ShouldReturnUpdateCouponResponseWithValueAndNoErrors()
    {
        var command = _dataFixture.GenerateValidUpdateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<UpdateCouponCommandResponse>();
        response.Errors.Should().BeEmpty();
    }
    
    [Fact(DisplayName = "When Command Is Invalid : Should Return Coupon Command Response With Errors And Null Value")]
    [Trait("Category", "Update Coupon Command Handler Tests")]
    public async Task Handle_WhenCommandIsInvalid_ShouldReturnCouponCommandResponseWithErrorsAndNullValue()
    {
        var command = _dataFixture.GenerateInvalidUpdateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<UpdateCouponCommandResponse>();
        response.Errors.Should().NotBeEmpty();
        response.ErrorType.Should().Be(Error.ValidationProblem);
        response.IsSuccess.Should().BeFalse();
    }
    
    [Fact(DisplayName = "When Coupon Does Not Exist In Db : Should Return Coupon Command Response With Errors And Null Value")]
    [Trait("Category", "Update Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponDoesNotExistInDb_ShouldReturnCouponCommandResponseWithErrorsAndNullValue()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateUpdateCouponCommandHandler(_repo.Object, _mapper.Object);
        
        var command = _dataFixture.GenerateValidUpdateCouponCommand();
        
        _repo.Setup(r => r.GetCouponByIdAsync(command.CouponId)).ReturnsAsync((Coupon?)null);
        
        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<UpdateCouponCommandResponse>();
        response.ErrorType.Should().Be(Error.NotFoundProblem);
        response.Errors.Should().NotBeEmpty();
        response.IsSuccess.Should().BeFalse();
    }
    
    [Fact(DisplayName = "When Coupon Is Not In Db And Validation Passes : Should Execute Dependencies Correctly")]
    [Trait("Category", "Update Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDbAndValidationPasses_ShouldExecuteDependenciesCorrectly()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateUpdateCouponCommandHandler(_repo.Object, _mapper.Object);
        var command = _dataFixture.GenerateValidUpdateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);
        
        response.IsSuccess.Should().BeTrue();
        _mapper.Verify(m => m.Map(It.IsAny<UpdateCouponCommand>(), It.IsAny<Coupon>()),Times.Once);
        _repo.Verify(r => r.GetCouponByIdAsync(It.IsAny<int>()), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
}