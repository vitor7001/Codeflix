namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;
public class CategoryTestFixture
{
    public DomainEntity.Category GetValidCategory()
        => new("Valid Name", "Valid Description");
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{ }