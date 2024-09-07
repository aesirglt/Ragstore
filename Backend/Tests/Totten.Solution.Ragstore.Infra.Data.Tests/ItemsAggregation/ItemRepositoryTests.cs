namespace Totten.Solution.Ragstore.Infra.Data.Tests.ItemsAggregation;

using Common.Tests;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemAggregation;

[TestFixture]
public class ItemRepositoryTests
{
    private readonly ServerStoreContext _context;
    private readonly ItemRepository _repository;

    public ItemRepositoryTests()
    {
        _context = ContextObjectMother.GetInMemoryServerStore("ItemRepositoryTests");
        _repository = new(_context);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context.Items.RemoveRange(_context.Items.AsNoTrackingWithIdentityResolution().ToArray());
        _context.Dispose();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Items.RemoveRange(_context.Items.AsNoTrackingWithIdentityResolution().ToArray());
    }

    [Test]
    public async Task ItemRepositoryTests_Save_ShouldBeOk()
    {
        // Arrange
        var item = ObjectMother.ItemRedPotion;

        // Act
        var act = async () => await _repository.Save(item);

        // Assert
        await act.Should().NotThrowAsync();

    }

    [Test]
    public async Task ItemRepositoryTests_Update_ShouldBeOk()
    {
        // Arrange
        var item = ObjectMother.ItemRedPotion;
        _ = await _repository.Save(item);
        var desc = "Poção vermelha de cura de baixo nivel";
        var itemUpdated = ObjectMother.ItemRedPotion with
        {
            Description = desc
        };

        // Act
        var act = async () => await _repository.Update(itemUpdated);

        // Assert
        await act.Should().NotThrowAsync();
        _context.Items.AsNoTrackingWithIdentityResolution().ToList()[0]
            .Description
            .Should()
            .Be(desc);
    }

    [Test]
    public async Task ItemRepositoryTests_Remove_ShouldBeOk()
    {
        // Arrange
        var item = ObjectMother.ItemRedPotion;
        _ = await _repository.Save(item);

        // Act
        var act = async () => await _repository.Remove(item);

        // Assert
        await act.Should().NotThrowAsync();
        _context.Items.AsNoTrackingWithIdentityResolution().ToList()
            .Should().BeEmpty();
    }

    [Test]
    public async Task ItemRepositoryTests_GetAll_ShouldBeOk()
    {
        // Arrange
        var item = ObjectMother.ItemRedPotion;
        _ = await _repository.Save(item);

        // Act
        var result = _repository.GetAll();

        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task ItemRepositoryTests_GetAllByName_ShouldBeOk()
    {
        // Arrange
        var item = ObjectMother.ItemRedPotion;
        _ = await _repository.Save(item);

        // Act
        var result = _repository.GetAllByName("Poção").ToList();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result[0].Name.Should().Be(item.Name);
    }

    [Test]
    public async Task ItemRepositoryTests_GetById_ShouldBeOk()
    {
        // Arrange
        var itemToAdd = ObjectMother.ItemRedPotion;
        _ = await _repository.Save(itemToAdd);

        // Act
        var itemOpt = await _repository.GetById(itemToAdd.Id);

        // Assert
        itemOpt.IsNone.Should().BeFalse();
        itemOpt.IsSome.Should().BeTrue();
        Item? itemInDB = null;
        itemOpt.Then(it => itemInDB = it);
        itemInDB.Should().NotBeNull();
        itemInDB!.Name.Should().Be(itemToAdd.Name);
    }
}
