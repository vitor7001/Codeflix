﻿using Codeflix.Catalog.Application.Interfaces;
using Codeflix.Catalog.Domain.Entity;
using Codeflix.Catalog.Domain.Repository;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;
using UseCases = Codeflix.Catalog.Application.UseCases.Category.CreateCategory;

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

        var input = new UseCases.CreateCategoryInput(
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

        output.Should().NotBeNull();
        output.Name.Should().Be("Nome");
        output.Description.Should().Be("Descrição");
        output.IsActive.Should().Be(true);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }
}
