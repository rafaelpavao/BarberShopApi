using FluentAssertions;
using Xunit.Abstractions;

namespace UnitTests.Barbearia.DomainTests.Entities.Payment;

public class PaymentTests : IClassFixture<PaymentTestsFixture>
{
    private PaymentTestsFixture _fixture;

    public PaymentTests(PaymentTestsFixture fixture, ITestOutputHelper outputHelper)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "New valid Payment")]
    [Trait("Category", "Payment Unit Tests")]
    public void ValidatePayment_WhenPaymentIsValid_ShouldRaiseNoExeptions()
    {
        // Arrange
        var payment = _fixture.GenerateValidPayment();

        // Act        
        Action act = () => payment.ValidatePayment();

        // Assert        
        act.Should().NotThrow();
    }

    [Fact(DisplayName = "New invalid Payment")]
    [Trait("Category", "Payment Unit Tests")]
    public void ValidatePayment_WhenPaymentNumberNotValid_ShouldRaiseExeptions()
    {
        // Arrange
        var payment = _fixture.GenerateInvalidPayment();

        // Act
        Action act = () => payment.ValidatePayment();

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory(DisplayName = "valid Payment Numbers")]
    [Trait("Category", "Payment Unit Tests")]
    [InlineData("Crédito")]
    [InlineData("Dinheiro")]
    [InlineData("Débito")]
    public void ValidatePayment_WhenPaymentsMethodValid_ShouldRaiseNoExeptions(string paymentMethod)
    {
        // Arrange
        var Payment = _fixture.GerenatePaymentsWithTheory(paymentMethod);

        // Act
        Action act = () => Payment.ValidatePayment();

        // Assert
        act.Should().NotThrow();
    }

    [Theory(DisplayName = "Invalid Payment Numbers")]
    [Trait("Category", "Payment Unit Tests")]
    [InlineData("Boleto")]
    [InlineData("Pix")]
    [InlineData("Criptomoeda")]
    public void ValidatePayment_WhenPaymentsMethodNotValid_ShouldRaiseExeptions(string paymentMethod)
    {
        // Arrange
        var Payment = _fixture.GerenatePaymentsWithTheory(paymentMethod);

        // Act
        Action act = () => Payment.ValidatePayment();

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    // [Fact(DisplayName = "New invalid Payment with bogus")]
    // [Trait("Category", "Payment Unit Tests")]
    // public void ValidatePayment_WhenPaymentNumberNotValidWhithBogus_ShouldRaiseExeptions()
    // {
    //     // Arrange
    //     var Payment = _fixture.BogusGeneratePaymentsWithInvalidNumber(1).FirstOrDefault();

    //     // Act
    //     Action act = () => Payment?.ValidatePayment();

    //     // Assert
    //     Assert.Throws<ArgumentException>(act);
    // }   

}

