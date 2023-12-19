using Bogus;
using Xunit;

using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using Codeflix.Catalog.Domain.Exceptions;

namespace Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(ShouldHaveSucessWithNotNullValue))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldHaveSucessWithNotNullValue()
    {
        var value = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNull(value, "value");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(ShouldHaveErrorWithNullValue))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldHaveErrorWithNullValue()
    {
        string? value = null;

        Action action =
            () => DomainValidation.NotNull(value, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null");
    }

    [Theory(DisplayName = nameof(WhenNullOrEmptyShouldThrowExeption))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void WhenNullOrEmptyShouldThrowExeption(string? target)
    {
        Action action = 
            () => DomainValidation.NotNullOrEmpty(target, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null or empty");
    }



    [Fact(DisplayName = nameof(WhenNotNullOrNotEmptyShoulNotThrowException))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void WhenNotNullOrNotEmptyShoulNotThrowException()
    {
        var target = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNullOrEmpty(target, "FieldName");

        action.Should().NotThrow();
    }
    // tamanho minimo
    // tamanho maximo


}
