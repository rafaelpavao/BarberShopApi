using AutoMapper;
using Barbearia.Application.Contracts.Repositories;
using Barbearia.Application.Features;
using Barbearia.Application.Features.Coupons.Commands.CreateCoupon;
using Barbearia.Domain.Entities;
using FluentAssertions;
using Moq;
using UnitTests.Barbearia.ApplicationTests.TestUtils.Coupons;

namespace UnitTests.Barbearia.ApplicationTests.Features.Coupons.Commands.CreateCoupon;

[Collection(nameof(CouponsTestsCollection))]
public class CreateCouponCommandHandlerTests : IClassFixture<CouponsTestsDataGeneratingFixture>
{
    private readonly CouponsTestsCollectionFixture _fixture;
    private readonly CouponsTestsDataGeneratingFixture _dataFixture;
    private Mock<IOrderRepository> _repo;
    private Mock<IMapper> _mapper;
    private CreateCouponCommandHandler _handler;

    public CreateCouponCommandHandlerTests(CouponsTestsCollectionFixture fixture, CouponsTestsDataGeneratingFixture dataFixture)
    {
        _fixture = fixture;
        _dataFixture = dataFixture;
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateCreateCouponCommandHandler(_repo.Object, _mapper.Object);
    }

    [Fact(DisplayName = "When Coupon Is Not In Db And Validation Passes : Should Return Create Coupon Response With Value And No Errors")]
    [Trait("Category", "Create Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDbAndValidationPasses_ShouldReturnCreateCouponResponseWithValueAndNoErrors()
    {
        var command = _dataFixture.GenerateValidCreateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<CreateCouponCommandResponse>();
        response.Coupon.Should().BeOfType<CreateCouponDto>();
        response.Errors.Should().BeEmpty();
        response.Coupon.Should().NotBeNull();
    }

    [Fact(DisplayName = "When Command Is Invalid : Should Return Coupon Command Response With Errors And Null Value")]
    [Trait("Category", "Create Coupon Command Handler Tests")]
    public async Task Handle_WhenCommandIsInvalid_ShouldReturnCouponCommandResponseWithErrorsAndNullValue()
    {
        var command = _dataFixture.GenerateInvalidCreateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<CreateCouponCommandResponse>();
        response.Coupon.Should().BeNull();
        response.Errors.Should().NotBeEmpty();
        response.ErrorType.Should().Be(Error.ValidationProblem);
        response.IsSuccess.Should().BeFalse();
    }

    [Fact(DisplayName = "When Coupon Exists In Db : Should Return Coupon Command Response With Errors And Null Value")]
    [Trait("Category", "Create Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponExistsInDb_ShouldReturnCouponCommandResponseWithErrorsAndNullValue()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateCreateCouponCommandHandler(_repo.Object, _mapper.Object);

        var command = _dataFixture.GenerateValidCreateCouponCommand();

        _repo.Setup(r => r.CouponExists(command.CouponCode)).ReturnsAsync(true);

        var response = await _handler.Handle(command, CancellationToken.None);

        response.Should().BeOfType<CreateCouponCommandResponse>();
        response.Coupon.Should().BeNull();
        response.ErrorType.Should().Be(Error.ValidationProblem);
        response.Errors.Should().NotBeEmpty();
        response.IsSuccess.Should().BeFalse();
    }

    [Fact(DisplayName = "When Coupon Is Not In Db And Validation Passes : Should Execute Dependencies Correctly")]
    [Trait("Category", "Create Coupon Command Handler Tests")]
    public async Task Handle_WhenCouponIsNotInDbAndValidationPasses_ShouldExecuteDependenciesCorrectly()
    {
        _repo = _fixture.GenerateAndSetupOrderRepository();
        _mapper = _fixture.GenerateAndSetupMapper();
        _handler = _fixture.GenerateCreateCouponCommandHandler(_repo.Object, _mapper.Object);
        var command = _dataFixture.GenerateValidCreateCouponCommand();

        var response = await _handler.Handle(command, CancellationToken.None);

        response.IsSuccess.Should().BeTrue();
        _mapper.Verify(m => m.Map<Coupon>(It.IsAny<CreateCouponCommand>()), Times.Once);
        _mapper.Verify(m => m.Map<CreateCouponDto>(It.IsAny<Coupon>()), Times.Once);
        _repo.Verify(r => r.AddCoupon(It.IsAny<Coupon>()), Times.Once);
        _repo.Verify(r => r.SaveChangesAsync(), Times.Once);

    }
}