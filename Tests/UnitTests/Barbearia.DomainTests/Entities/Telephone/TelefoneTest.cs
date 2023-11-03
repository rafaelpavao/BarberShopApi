using FluentAssertions;
using Xunit.Abstractions;

namespace UnitTests.Barbearia.DomainTests.Entities.Telephone;
public class TelephoneTests : IClassFixture<TelephoneTestsFixture>
{
    private readonly TelephoneTestsFixture _fixture;    

    public TelephoneTests(TelephoneTestsFixture fixture)
    {
        _fixture = fixture;        
    }

    [Fact(DisplayName = "New valid Telephone")]
    [Trait("Category", "Telephone Unit Tests")]
    public void ValidateTelephone_WhenTelephoneIsValid_ShouldRaiseNoExeptions()
    {
        // Arrange
        var telephone = _fixture.GenerateValidTelephone();

        // Act        
        Action act = () => telephone.ValidateTelephone();

        // Assert        
        act.Should().NotThrow();
    }

    [Fact(DisplayName = "New invalid Telephone")]
    [Trait("Category", "Telephone Unit Tests")]
    public void ValidateTelephone_WhenTelephoneNumberNotValid_ShouldRaiseExeptions()
    {
        // Arrange
        var telephone = _fixture.GenerateInvalidTelephone();

        // Act
        Action act = () => telephone.ValidateTelephone();

        // Assert
        Assert.Throws<ArgumentException>(act);        
    }

    [Fact(DisplayName = "New valid Telephone with bogus")]
    [Trait("Category", "Telephone Unit Tests")]
    public void ValidateTelephone_WhenTelephoneNumberValidWhithBogus_ShouldRaiseNoExeptions()
    {
        // Arrange
        var Telephone = _fixture.BogusGenerateTelephonesWithValidNumber(1).FirstOrDefault();

        // Act
        Action act = () => Telephone?.ValidateTelephone();

        // Assert
        act.Should().NotThrow();
    }

    [Fact(DisplayName = "New invalid Telephone with bogus")]
    [Trait("Category", "Telephone Unit Tests")]
    public void ValidateTelephone_WhenTelephoneNumberNotValidWhithBogus_ShouldRaiseExeptions()
    {
        // Arrange
        var Telephone = _fixture.BogusGenerateTelephonesWithInvalidNumber(1).FirstOrDefault();

        // Act
        Action act = () => Telephone?.ValidateTelephone();

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory(DisplayName = "Invalid Telephone Numbers")]
    [Trait("Category", "Telephone Unit Tests")]
    [InlineData("479888877776")]
    [InlineData("1234567890")]
    [InlineData("9")]
    public void ValidateTelephone_WhenTelephonesNumberNotValid_ShouldRaiseExeptions(string phoneNumber)
    {
        // Arrange
        var telephone = _fixture.GerenateTelephonesWithTheory(phoneNumber);

        // Act
        Action act = () => telephone.ValidateTelephone();

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Theory(DisplayName = "valid Telephone Numbers")]
    [Trait("Category", "Telephone Unit Tests")]
    [InlineData("47911112222")]
    [InlineData("47988887777")]
    [InlineData("47996852467")]
    public void ValidateTelephone_WhenTelephonesNumberValid_ShouldRaiseNoExeptions(string phoneNumber)
    {
        // Arrange
        var telephone = _fixture.GerenateTelephonesWithTheory(phoneNumber);

        // Act
        Action act = () => telephone.ValidateTelephone();

        // Assert
        act.Should().NotThrow();
    }
}