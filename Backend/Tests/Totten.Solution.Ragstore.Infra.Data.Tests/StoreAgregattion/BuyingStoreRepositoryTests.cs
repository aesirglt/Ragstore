namespace Totten.Solution.Ragstore.Infra.Data.Tests.StoreAgregattion;

using Common.Tests;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores;
using Totten.Solution.Ragstore.Infra.Data.Seeds;

[TestFixture]
public class BuyingStoreRepositoryTests
{
    private readonly ServerStoreContext _context;
    private BuyingStoreRepository _repository;

    public BuyingStoreRepositoryTests()
    {
        _context = ContextObjectMother.GetInMemoryServerStore("BuyingStoreRepositoryTests");
        _repository = new(_context);
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _context.Accounts.AddRange(MyAccountSeed.Seed());
        _context.Characters.AddRange(MyCharacterSeed.Seed());
        //_context.Items.AddRange(MyItemSeed.Seed());
        _context.SaveChanges();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _context.Dispose();
    }

    [TearDown]
    public void TearDown()
    {
        _context.BuyingStoreItems.RemoveRange(_context.BuyingStoreItems.AsNoTrackingWithIdentityResolution().ToList());
        _context.SaveChanges();
        _context.BuyingStores.RemoveRange(_context.BuyingStores.AsNoTrackingWithIdentityResolution().ToList());
        _context.SaveChanges();
    }

    [Test]
    public async Task BuyingStoreRepositoryTests_Save_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var buyingStore = ObjectMother.GetBuyingStore(MyCharacterSeed.Seed()[0], redPotion);

        // Act
        var act = async () => await _repository.Save(buyingStore);

        // Assert
        await act.Should().NotThrowAsync();
        _context.BuyingStoreItems.AsNoTrackingWithIdentityResolution().Count().Should().Be(expected: 1);
    }

    [Test]
    public async Task BuyingStoreRepositoryTests_Save_WithTwoItem_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var obscuro = ObjectMother.ItemAnelObscuro;
        var buyingStore = ObjectMother.GetBuyingStore(MyCharacterSeed.Seed()[0], redPotion);

        // Act
        var act = async () => await _repository.Save(buyingStore);

        // Assert
        await act.Should().NotThrowAsync();
        _context.BuyingStoreItems.AsNoTrackingWithIdentityResolution().Count().Should().Be(expected: 1);
    }

    [Test]
    public async Task BuyingStoreRepositoryTests_Update_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var buyingStore = ObjectMother.GetBuyingStore(MyCharacterSeed.Seed()[0], redPotion);
        _ = await _repository.Save(buyingStore);

        var newName = "Novo nome lojinha";
        var updatedVending = buyingStore with
        {
            Name = newName
        };

        // Act
        var act = async () => await _repository.Update(updatedVending);

        // Assert
        await act.Should().NotThrowAsync();
        var buyings = _context.BuyingStores.AsNoTrackingWithIdentityResolution().ToList();
        buyings.Count.Should().Be(expected: 1);
        buyings[0].Name.Should().Be(expected: newName);
    }

    [Test]
    public async Task BuyingStoreRepositoryTests_GetAll_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var buyingStore = ObjectMother.GetBuyingStore(MyCharacterSeed.Seed()[0], redPotion);
        _ = await _repository.Save(buyingStore);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result[0].Name.Should().Be(expected: buyingStore.Name);
        result[0].BuyingStoreItem!.Name.Should().Be(redPotion.Name);
        result[0].Character.Should().NotBeNull();
        result[0].Character!.Name.Should().Be(MyCharacterSeed.Seed()[0].Name);
    }

    [Test]
    public async Task BuyingStoreRepositoryTests_GetById_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var buyingStore = ObjectMother.GetBuyingStore(MyCharacterSeed.Seed()[0], redPotion);
        _ = await _repository.Save(buyingStore);

        // Act
        var buyingOpt = await _repository.GetById(buyingStore.Id);

        // Assert
        buyingOpt.IsNone.Should().BeFalse();
        buyingOpt.IsSome.Should().BeTrue();
        BuyingStore? buying = null;
        buyingOpt.Then(vdn => buying = vdn);
        buying.Should().NotBeNull();
        buying!.Name.Should().Be(expected: buyingStore.Name);
        buying!.BuyingStoreItem!.Name.Should().Be(redPotion.Name);
        buying!.Character.Should().NotBeNull();
        buying!.Character!.Name.Should().Be(MyCharacterSeed.Seed()[0].Name);
    }
}
