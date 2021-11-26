using System;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Controllers;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Catalog.UnitTests;

public class ItemsControllerTests
{
    private readonly Mock<IItemRepository> _repository = new Mock<IItemRepository>();
    private readonly Mock<ILogger<ItemsController>> _logger = new Mock<ILogger<ItemsController>>();

    // Method name pattern: UnitOfWork_StateUnderTest_ExpectedBehavior

    [Fact]
    public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
    {
        // Arrange
        _repository
            .Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Item)null);

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        // Assert
        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetItemAsync_WithExistingItem_ReturnsExpectedItem()
    {
        // Arrange
        var expectedItem = CreateAnItem();
        _repository
            .Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(expectedItem);

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var result = await controller.GetItemAsync(Guid.NewGuid());

        // Assert
        result.Value.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<Item>());
    }

    [Fact]
    public async Task GetItemsAsync_WithExistingItems_ReturnsAllItem()
    {
        // Arrange
        var expectedItems = Faker.Extensions.EnumerableExtensions.Times<Item>(5, _ => CreateAnItem()).ToList();
        _repository
            .Setup(repo => repo.GetItemsAsync())
            .ReturnsAsync(expectedItems);

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var actualItems = await controller.GetItemsAsync();

        // Assert
        actualItems.Should().BeEquivalentTo(expectedItems, options => options.ComparingByMembers<Item>());
    }

    [Fact]
    public async Task CreateItemAsync_WithItemToCreate_ReturnsCreatedItem()
    {
        // Arrange
        var itemToCreate = new CreateItemDto()
        {
            Name = Faker.Name.First(),
            Price = Faker.RandomNumber.Next(100)
        };

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var result = await controller.CreateItemAsync(itemToCreate);

        // Assert
        var createdItem = (result.Result as CreatedAtActionResult).Value as ItemDto;
        itemToCreate.Should().BeEquivalentTo(createdItem, 
            options => options.ComparingByMembers<ItemDto>().ExcludingMissingMembers());
        createdItem.Id.Should().NotBeEmpty();
        createdItem.CreatedDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task UpdateItemAsync_WithExistingItem_ReturnsNoContent()
    {
        // Arrange
        var existingItem = CreateAnItem();
        _repository
            .Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingItem);

        var itemId = existingItem.Id;
        var itemToUpdate = new UpdateItemDto()
        {
            Id = itemId,
            Name = Faker.Name.First(),
            Price = Faker.RandomNumber.Next(100)
        };

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var result = await controller.UpdateItemAsync(itemId, itemToUpdate);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteItemAsync_WithExistingItem_ReturnsNoContent()
    {
        // Arrange
        var existingItem = CreateAnItem();
        _repository
            .Setup(repo => repo.GetItemAsync(It.IsAny<Guid>()))
            .ReturnsAsync(existingItem);

        var controller = new ItemsController(_repository.Object, _logger.Object);

        // Act
        var result = await controller.DeleteItemAsync(existingItem.Id);

        // Assert
        result.Should().BeOfType<NoContentResult>();
    }


    private Item CreateAnItem()
    {
        return new Item()
        {
            Id = Guid.NewGuid(),
            Name = Faker.Name.First(),
            Price = Faker.RandomNumber.Next(100),
            CreatedDate = DateTimeOffset.UtcNow
        };
    }
}