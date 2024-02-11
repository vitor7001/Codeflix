using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.CreateCategory;

namespace Codeflix.Catalog.UnitTests.Application.CreateCategory;
public class CreateCategoryTest
{

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = new Mock<ICategoryRepository>();
        var unitOfWork = new Mock<IUnitOfWork>();

        var useCase = new UseCases.CreateCategory(
            repositoryMock.Object, 
            unitOfWork.Object);

        var input = new CreateCategoryInput(
            "Nome",
            "Descrição", 
            true
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository => 
            repository.Insert(
                It.IsAny<Category>(),
                It.IsAny<CancellationToken>()
                ),
            Times.Once
        );

        unitOfWork.Verify(unit =>
            unit.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.ShouldNotBeNull();
        output.Name.Should().Be("Nome");
        output.Description.Should().Be("Descrição");
        output.IsActive.Should().Be(true);
        (output.id != null && output.id != Guid.Empty).Should().BeTrue();
        (output.CreatedAt != null && output.CreatedAt != default(DateTime)).Should.BeTrue();
    }
}
