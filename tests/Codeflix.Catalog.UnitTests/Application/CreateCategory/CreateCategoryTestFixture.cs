using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Application.UseCases.Category.CreateCategory;
using Codeflix.Catalog.Domain.Repository;
using Codeflix.Catalog.UnitTests.Common;
using Moq;
using Xunit;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture>

{}

public class CreateCategoryTestFixture : BaseFixture
{

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

    public bool getRandomIsActive()
        => (new Random()).NextDouble() < 0.5;

    public CreateCategoryInput GetValidInput()
        => new(GetValidCategoryName(), GetValidCategoryDescription(), getRandomIsActive());

    public Mock<ICategoryRepository>  GetRepositoryMock() 
        => new ();
    
    public Mock<IUnitOfWork>  GetUnitOfWorkMock() 
        => new ();
}
