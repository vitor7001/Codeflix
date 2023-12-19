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
        string value = null;

        Action action =
            () => DomainValidation.NotNull(value, "FieldName");

        action.Should().Throw<EntityValidationException>()
            .WithMessage("FieldName should not be null");
    }

    // nao ser null ou vazio

    // tamanho minimo
    // tamanho maximo


}
