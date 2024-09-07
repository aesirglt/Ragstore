namespace Totten.Solution.Ragstore.Infra.Data.Tests.StoreAgregattion;

using Common.Tests;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.VendingStores;
using Totten.Solution.Ragstore.Infra.Data.Seeds;

[TestFixture]
public class VendingStoreRepositoryTests
{
    private readonly ServerStoreContext _context;
    private VendingStoreRepository _repository;

    public VendingStoreRepositoryTests()
    {
        _context = ContextObjectMother.GetInMemoryServerStore("VendingStoreRepositoryTests");
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
        _context.VendingStoreItems.RemoveRange(_context.VendingStoreItems.AsNoTrackingWithIdentityResolution().ToList());
        _context.SaveChanges();
        _context.VendingStores.RemoveRange(_context.VendingStores.AsNoTrackingWithIdentityResolution().ToList());
        _context.SaveChanges();
    }

    [Test]
    public async Task VendingStoreRepositoryTests_Save_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var vendingStore = ObjectMother.GetVendingStore(MyCharacterSeed.Seed()[0], [redPotion]);

        // Act
        var act = async () => await _repository.Save(vendingStore);

        // Assert
        await act.Should().NotThrowAsync();
        _context.VendingStoreItems.AsNoTrackingWithIdentityResolution().Count().Should().Be(expected: 1);
    }

    [Test]
    public async Task VendingStoreRepositoryTests_Save_WithTwoItem_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var obscuro = ObjectMother.ItemAnelObscuro;
        var vendingStore = ObjectMother.GetVendingStore(MyCharacterSeed.Seed()[0], [redPotion, obscuro]);

        // Act
        var act = async () => await _repository.Save(vendingStore);

        // Assert
        await act.Should().NotThrowAsync();
        _context.VendingStoreItems.AsNoTrackingWithIdentityResolution().Count().Should().Be(expected: 2);
    }

    [Test]
    public async Task VendingStoreRepositoryTests_Update_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var vendingStore = ObjectMother.GetVendingStore(MyCharacterSeed.Seed()[0], [redPotion]);
        _ = await _repository.Save(vendingStore);

        var newName = "Novo nome lojinha";
        var updatedVending = vendingStore with
        {
            Name = newName
        };

        // Act
        var act = async () => await _repository.Update(updatedVending);

        // Assert
        await act.Should().NotThrowAsync();
        var vendings = _context.VendingStores.AsNoTrackingWithIdentityResolution().ToList();
        vendings.Count.Should().Be(expected: 1);
        vendings[0].Name.Should().Be(expected: newName);
    }

    [Test]
    public async Task VendingStoreRepositoryTests_GetAll_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var vendingStore = ObjectMother.GetVendingStore(MyCharacterSeed.Seed()[0], [redPotion]);
        _ = await _repository.Save(vendingStore);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        result.Should().NotBeNullOrEmpty();
        result[0].Name.Should().Be(expected: vendingStore.Name);
        result[0].VendingStoreItems[0].Name.Should().Be(redPotion.Name);
        result[0].Character.Should().NotBeNull();
        result[0].Character!.Name.Should().Be(MyCharacterSeed.Seed()[0].Name);
    }

    [Test]
    public async Task VendingStoreRepositoryTests_GetById_ShouldBeOk()
    {
        // Arrange
        var redPotion = ObjectMother.ItemRedPotion;
        var vendingStore = ObjectMother.GetVendingStore(MyCharacterSeed.Seed()[0], [redPotion]);
        _ = await _repository.Save(vendingStore);

        // Act
        var vendingOpt = await _repository.GetById(vendingStore.Id);

        // Assert
        vendingOpt.IsNone.Should().BeFalse();
        vendingOpt.IsSome.Should().BeTrue();
        VendingStore? vending = null;
        vendingOpt.Then(vdn => vending = vdn);
        vending.Should().NotBeNull();
        vending!.Name.Should().Be(expected: vendingStore.Name);
        vending!.VendingStoreItems[0].Name.Should().Be(redPotion.Name);
        vending!.Character.Should().NotBeNull();
        vending!.Character!.Name.Should().Be(MyCharacterSeed.Seed()[0].Name);
    }
}
