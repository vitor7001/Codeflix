using Codeflix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;
public class CategoryTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Category - Aggregates")]
    public void Instantiate()
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description);

        var dateTimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt > dateTimeBefore).Should().BeTrue();
        (category.CreatedAt < dateTimeAfter).Should().BeTrue();
        (category.IsActive).Should().BeTrue();

    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool isActive)
    {
        //Arrange
        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var dateTimeBefore = DateTime.Now;

        //Act
        var category = new DomainEntity.Category(validData.Name, validData.Description, isActive);

        var dateTimeAfter = DateTime.Now;

        //Assert
        category.Should().NotBeNull();
        category.Name.Should().Be(validData.Name);
        category.Description.Should().Be(validData.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt > dateTimeBefore).Should().BeTrue();
        (category.CreatedAt < dateTimeAfter).Should().BeTrue();
        (category.IsActive).Should().Be(isActive);
    }

    [Theory(DisplayName = nameof(InstantiateThrowNewErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateThrowNewErrorWhenNameIsEmpty(string? name)
    {
        Action action = () => new DomainEntity.Category(name!, "Valid description");

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");

    }

    [Fact(DisplayName = nameof(InstantiateThrowNewErrorWhenDescriptionIsNull))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateThrowNewErrorWhenDescriptionIsNull()
    {
        Action action = () => new DomainEntity.Category("Valid name", null!);


        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should not be empty or null");
    }


    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("na")]
    [InlineData("va")]
    [InlineData("a")]
    public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
    {

        Action action = () => new DomainEntity.Category(invalidName, "Valid description");


        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category(invalidName, "Valid description");


        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

        Action action = () => new DomainEntity.Category("Valid name", invalidDescription);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal 10.000 characters long");
    }

    [Fact(DisplayName = nameof(ActivateCategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void ActivateCategory()
    {

        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var category = new DomainEntity.Category(validData.Name, validData.Description, false);

        category.Activate();

        category.IsActive.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(DeactivateCategory))]
    [Trait("Domain", "Category - Aggregates")]
    public void DeactivateCategory()
    {

        var validData = new
        {
            Name = "category name",
            Description = "category description"
        };

        var category = new DomainEntity.Category(validData.Name, validData.Description, true);

        category.Deactivate();

        category.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = nameof(UpdateNameAndDescription))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateNameAndDescription()
    {
        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        var updatedValues = new { Name = "Updated Name", Description = "Updated Description" };

        category.Update(updatedValues.Name, updatedValues.Description);

        category.Name.Should().Be(updatedValues.Name);
        category.Description.Should().Be(updatedValues.Description);

    }


    [Fact(DisplayName = nameof(UpdateName))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateName()
    {
        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        var updatedValues = new { Name = "Updated Name" };

        var currentDescription = category.Description;

        category.Update(updatedValues.Name);

        category.Name.Should().Be(updatedValues.Name);
        category.Description.Should().Be(currentDescription);
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        Action action = () => category.Update(name!);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");

    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("na")]
    [InlineData("va")]
    [InlineData("a")]
    public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
    {
        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        Action action = () => category.Update(invalidName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be at least 3 characters long");
    }


    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        Action action = () => category.Update(invalidName);
        action.Should().Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal 255 characters long");
    }

    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
    [Trait("Domain", "Category - Aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
    {
        var invalidDescription = String.Join(null, Enumerable.Range(1, 10_001).Select(_ => "a").ToArray());

        var category = new DomainEntity.Category("Valid Name", "Valid Description");

        Action action = () => category.Update("Valid Name", invalidDescription);

        action.Should().Throw<EntityValidationException>()
            .WithMessage("Description should be less or equal 10.000 characters long");
    }


}

