﻿using Moq;
using NextRef.Application.Features.UserCollections.Commands.CreateCollection;
using NextRef.Application.Features.UserCollections.Models;
using NextRef.Domain.Core.Ids;
using NextRef.Domain.UserCollections.Models;
using NextRef.Domain.UserCollections.Repositories;

namespace NextRef.Application.Tests.Handlers.UserCollections;
public class CreateCollectionHandlerTests
{
    private readonly Mock<IUserCollectionRepository> _repositoryMock;
    private readonly CreateCollectionCommandHandler _handler;

    public CreateCollectionHandlerTests()
    {
        _repositoryMock = new Mock<IUserCollectionRepository>();
        _handler = new CreateCollectionCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCollectionAndReturnId()
    {
        // Arrange
        var userId = UserId.New();
        var name = "Ma collection";

        UserCollection? savedCollection = null;

        _repositoryMock
            .Setup(r => r.AddAsync(It.IsAny<UserCollection>(), CancellationToken.None))
            .Callback<UserCollection, CancellationToken>((uc, _) => savedCollection = uc)
            .Returns(Task.CompletedTask);

        var command = new CreateCollectionCommand(userId, name);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<UserCollection>(), CancellationToken.None), Times.Once);

        Assert.NotNull(savedCollection);
        Assert.Equal(name, savedCollection.Name);
        Assert.Equal(userId, savedCollection.UserId);
        Assert.Equal(result, UserCollectionDto.FromDomain(savedCollection));
    }
}
