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

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMin), parameters:10)]
    public void MinLengthThrowWhenLess(string target, int minLenght)
    {
        Action action = 
           () => DomainValidation.MinLength(target, minLenght, "fieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"fieldName should not be less than {minLenght} characters long.");
    }

    public static IEnumerable<object[]> GetValuesSmallerThanMin(int numberTries = 5)
    {
        yield return new object[] { "123456", 10 };

        var faker = new Faker();
        for(int i = 0; i  < numberTries - 1; i++)
        {
            var value = faker.Commerce.ProductName();
            var minValue = value.Length + (new Random()).Next(1,20);
            yield return new object[] { value, minValue };
        }
    }



    [Theory(DisplayName = nameof(MinLengthOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMin), parameters: 10)]
    public void MinLengthOk(string target, int minLenght)
    {
        Action action =
           () => DomainValidation.MinLength(target, minLenght, "fieldName");

        action.Should().NotThrow();
            
    }

    public static IEnumerable<object[]> GetValuesGreaterThanMin(int numberTries = 5)
    {
        yield return new object[] { "123456", 6 };

        var faker = new Faker();
        for (int i = 0; i < numberTries - 1; i++)
        {
            var value = faker.Commerce.ProductName();
            var minValue = value.Length - (new Random()).Next(1, 5);
            yield return new object[] { value, minValue };
        }
    }


    // tamanho maximo


}
