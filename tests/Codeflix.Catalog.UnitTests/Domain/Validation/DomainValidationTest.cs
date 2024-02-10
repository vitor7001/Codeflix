using Bogus;
using Xunit;

using Codeflix.Catalog.Domain.Validation;
using FluentAssertions;
using Codeflix.Catalog.Domain.Exceptions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Text.RegularExpressions;

namespace Codeflix.Catalog.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker();

    [Fact(DisplayName = nameof(ShouldHaveSucessWithNotNullValue))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldHaveSucessWithNotNullValue()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        var value = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNull(value, fieldName);

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(ShouldHaveErrorWithNullValue))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void ShouldHaveErrorWithNullValue()
    {
        string? value = null;
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action =
            () => DomainValidation.NotNull(value, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be null");
    }

    [Theory(DisplayName = nameof(WhenNullOrEmptyShouldThrowExeption))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void WhenNullOrEmptyShouldThrowExeption(string? target)
    {

        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = 
            () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }



    [Fact(DisplayName = nameof(WhenNotNullOrNotEmptyShoulNotThrowException))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void WhenNotNullOrNotEmptyShoulNotThrowException()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        var target = Faker.Commerce.ProductName();

        Action action =
            () => DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesSmallerThanMin), parameters:10)]
    public void MinLengthThrowWhenLess(string target, int minLenght)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = 
           () => DomainValidation.MinLength(target, minLenght, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be at least {minLenght} characters long");
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
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
           () => DomainValidation.MinLength(target, minLenght, fieldName);

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

    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesGreaterThanMax), parameters: 10)]
    public void MaxLengthThrowWhenGreater(string target, int maxLength )
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action = 
            () => DomainValidation.MaxLength(target, maxLength, fieldName);


        action.Should().Throw<EntityValidationException>()
        .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");

    }

    public static IEnumerable<object[]> GetValuesGreaterThanMax(int numberTries = 5)
    {
        yield return new object[] { "123456", 5 };

        var faker = new Faker();
        for (int i = 0; i < numberTries - 1; i++)
        {
            var value = faker.Commerce.ProductName();
            var maxValue = value.Length - (new Random()).Next(1, 5);
            yield return new object[] { value, maxValue };
        }
    }


    [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
    [Trait("Domain", "DomainValidation - Validation")]
    [MemberData(nameof(GetValuesLessThanMax), parameters: 10)]
    public void MaxLengthOK(string target, int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

        Action action =
            () => DomainValidation.MaxLength(target, maxLength, fieldName);


        action.Should().NotThrow();
        
    }

    public static IEnumerable<object[]> GetValuesLessThanMax(int numberTries = 5)
    {
        yield return new object[] { "123456", 6 };

        var faker = new Faker();
        for (int i = 0; i < numberTries - 1; i++)
        {
            var value = faker.Commerce.ProductName();
            var maxValue = value.Length + (new Random()).Next(0, 5);
            yield return new object[] { value, maxValue };
        }
    }

}
