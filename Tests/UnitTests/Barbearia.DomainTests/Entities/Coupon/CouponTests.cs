using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests.Barbearia.DomainTests;

public class CouponTests : IClassFixture<CouponTestsFixture>
{
    private readonly CouponTestsFixture _fixture;
    private readonly ITestOutputHelper _outputHelper;

    public CouponTests(CouponTestsFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
        _outputHelper = outputHelper;
    }

    [Fact]
    public void ValidateCoupon_WhenCouponIsValid_ShouldRaiseNoExeptions()
    {
        var coupon = _fixture.GenerateValidCoupons(1).FirstOrDefault();

        Action act = () => coupon?.ValidateCoupon();
        var exeption = Record.Exception(() => coupon?.ValidateCoupon());

        coupon?.CreationDate.Should().BeBefore(DateTime.UtcNow);
        coupon?.ExpirationDate.Should().BeAfter(DateTime.UtcNow);
        coupon?.DiscountPercent.Should().BeInRange(1, 100);
        exeption.Should().BeNull();
    }

    [Fact]
    public void ValidateCoupon_WhenExpirationDateIsInPast_ShouldRaiseExeption()
    {
        var coupon = _fixture.GenerateCouponsWithInvalidExpirationDate(1).FirstOrDefault();

        Action act = () => coupon?.ValidateCoupon();
        var exeption = Record.Exception(() => coupon?.ValidateCoupon());

        coupon?.ExpirationDate.Should().BeBefore(DateTime.UtcNow);
        Assert.Throws<ArgumentException>(act);
        exeption.Message.Should().NotBeEmpty();
        exeption.Message.Equals("A data de expiração não pode ser anterior à data de criação.");

        _outputHelper.WriteLine($"Error message: {exeption.Message}");

    }

    [Fact]
    public void ValidateCoupon_WhenCreationDateIsInFuture_ShouldRaiseExeption()
    {
        var coupon = _fixture.GenerateCouponsWithInvalidCreationDate(1).FirstOrDefault();

        Action act = () => coupon?.ValidateCoupon();
        var exeption = Record.Exception(() => coupon?.ValidateCoupon());

        coupon?.CreationDate.Should().BeAfter(DateTime.UtcNow);
        Assert.Throws<ArgumentException>(act);
        exeption.Message.Should().NotBeEmpty();
        exeption.Message.Equals("A data de criação não pode ser no futuro.");

        _outputHelper.WriteLine($"Error message: {exeption.Message}");

    }

    [Fact]
    public void ValidateCoupon_WhenDiscountPercentageIsOutOfRange_ShouldRaiseExeption()
    {
        var coupon = _fixture.GenerateCouponsWithInvalidDiscount(1).FirstOrDefault();

        Action act = () => coupon?.ValidateCoupon();
        var exeption = Record.Exception(() => coupon?.ValidateCoupon());

        coupon?.DiscountPercent.Should().NotBeInRange(1, 100);
        Assert.Throws<ArgumentException>(act);
        exeption.Message.Should().NotBeEmpty();
        exeption.Message.Equals("A taxa de desconto deve estar entre 1 e 100.");

        _outputHelper.WriteLine($"Error message: {exeption.Message}");

    }
}