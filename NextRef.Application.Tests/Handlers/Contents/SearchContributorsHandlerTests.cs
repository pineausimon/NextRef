using Moq;
using NextRef.Application.Features.Contributors.Models;
using NextRef.Application.Features.Contributors.Queries.SearchContributors;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;
using NextRef.Domain.Core.Ids;

namespace NextRef.Application.Tests.Handlers.Contents;
public class SearchContributorsHandlerTests
{
    private readonly Mock<IContributorRepository> _repositoryMock;
    private readonly SearchContributorsQueryHandler _handler;

    public SearchContributorsHandlerTests()
    {
        _repositoryMock = new Mock<IContributorRepository>();
        _handler = new SearchContributorsQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ReturnsContributorDtos_WhenContributorsFound()
    {
        // Arrange
        var contributors = new List<Contributor>
        {
            Contributor.Rehydrate(ContributorId.New(), "Alice", "Bio1"),
            Contributor.Rehydrate(ContributorId.New(), "Bob", "Bio2")
        };
        _repositoryMock
            .Setup(r => r.SearchAsync("test", It.IsAny<CancellationToken>()))
            .ReturnsAsync(contributors);

        var query = new SearchContributorsQuery("test");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, dto => Assert.IsType<ContributorDto>(dto));
        Assert.Equal(contributors[0].Id, result[0].Id);
        Assert.Equal(contributors[1].Id, result[1].Id);
    }

    [Fact]
    public async Task Handle_ReturnsEmptyList_WhenNoContributorsFound()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.SearchAsync("none", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Contributor>());

        var query = new SearchContributorsQuery("none");

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_PropagatesException_WhenRepositoryThrows()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new System.Exception("Repository error"));

        var query = new SearchContributorsQuery("error");

        // Act & Assert
        await Assert.ThrowsAsync<System.Exception>(() => _handler.Handle(query, CancellationToken.None));
    }
}
