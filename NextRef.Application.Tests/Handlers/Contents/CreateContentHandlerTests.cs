using FluentAssertions;
using Moq;
using NextRef.Application.Contents.Commands.CreateContent;
using NextRef.Domain.Contents.Models;
using NextRef.Domain.Contents.Repositories;

namespace NextRef.Application.Tests.Handlers.Contents;
public class CreateContentHandlerTests
{
    private readonly Mock<IContentRepository> _contentRepositoryMock;
    private readonly CreateContentHandler _handler;

    public CreateContentHandlerTests()
    {
        _contentRepositoryMock = new Mock<IContentRepository>();
        _handler = new CreateContentHandler(_contentRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateContentAndReturnId()
    {
        // Arrange
        var command = new CreateContentCommand("Test Title", "Test Type", DateTime.UtcNow, "Test Description");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeEmpty(); 

        _contentRepositoryMock.Verify(r => r
            .AddAsync(It.Is<Content>(c =>
                c.Title == command.Title && 
                c.Description == command.Description &&
                c.Description == command.Description && 
                c.PublishedAt == command.PublishedAt
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenTitleIsEmpty()
    {
        var command = new CreateContentCommand("", "Test Type", DateTime.UtcNow, "desc");

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>();
    }

}