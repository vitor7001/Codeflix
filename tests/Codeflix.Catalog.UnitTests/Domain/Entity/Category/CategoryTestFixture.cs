namespace Codeflix.Catalog.UnitTests.Domain.Entity.Category;

using Codeflix.Catalog.UnitTests.Common;
using Xunit;
using DomainEntity = Codeflix.Catalog.Domain.Entity;

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
{ }
public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture()
        : base() { }

    public string GetValidCategoryName()
    {
        var categoryName = "";

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescriptionn = Faker.Commerce.ProductDescription();

        if (categoryDescriptionn.Length > 10_000)
            categoryDescriptionn = categoryDescriptionn[..10_000];

        return categoryDescriptionn;
    }

    public DomainEntity.Category GetValidCategory()
        => new(GetValidCategoryName(),
                GetValidCategoryDescription());
}

