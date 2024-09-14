namespace Totten.Solution.Ragstore.Application.Tests.BuyingStores;
using AutoMapper;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Options;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

[TestFixture]
public class BuyingStoreSaveCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IBuyingStoreRepository> _storeRepositoryMock;
    private readonly Mock<IBuyingStoreItemRepository> _storeItemRepositoryMock;
    private readonly BuyingStoreSaveCommandHandler _handler;

    public BuyingStoreSaveCommandHandlerTests()
    {
        _mediatorMock = new();
        _mapperMock = new();
        _storeRepositoryMock = new();
        _storeItemRepositoryMock = new();
        _handler = new(_mediatorMock.Object,
                       _mapperMock.Object,
                       _storeRepositoryMock.Object,
                       _storeItemRepositoryMock.Object);
    }

    [Test]
    public async Task BuyingStoreSaveCommandHandlerTests_Handle_Save_ShouldBeOk()
    {
        // Arrange
        var cmd = ObjectMother.GetBuyingStoreSaveCmd();
        var store = ObjectMother.GetBuyingStore(new()
        {
            Id = 1,
            Name = "test"
        }, ObjectMother.ItemRedPotion);

        var storeItem = store.BuyingStoreItem;

        _storeRepositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<BuyingStore>(cmd))
                   .Returns(store);
        _mapperMock.Setup(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()))
                   .Returns(storeItem!);

        _storeRepositoryMock.Setup(x => x.Save(store))
            .ReturnsAsync(default(Success));

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        _storeRepositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _storeRepositoryMock.Verify(x => x.Save(store));
        _mapperMock.Verify(x => x.Map<BuyingStore>(cmd));
        _mapperMock.Verify(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()));
        _mediatorMock.Verify(x => x.Publish(It.IsAny<NewStoreNotification>(), CancellationToken.None));

        _storeRepositoryMock.VerifyNoOtherCalls();
        _storeItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task BuyingStoreSaveCommandHandlerTests_Handle_SaveThrowExn_ShouldBeError()
    {
        // Arrange
        var cmd = ObjectMother.GetBuyingStoreSaveCmd();
        var store = ObjectMother.GetBuyingStore(new()
        {
            Id = 1,
            Name = "test"
        }, ObjectMother.ItemRedPotion);
        var storeItem = store.BuyingStoreItem;

        _storeRepositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<BuyingStore>(cmd))
                   .Returns(store);
        _mapperMock.Setup(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()))
                   .Returns(storeItem!);

        _storeRepositoryMock.Setup(x => x.Save(store))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        _storeRepositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _storeRepositoryMock.Verify(x => x.Save(store));
        _mapperMock.Verify(x => x.Map<BuyingStore>(cmd));
        _mapperMock.Verify(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()));

        _storeRepositoryMock.VerifyNoOtherCalls();
        _storeItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task BuyingStoreSaveCommandHandlerTests_Handle_MapThrowExn_ShouldBeError()
    {
        // Arrange
        var cmd = ObjectMother.GetBuyingStoreSaveCmd();

        _storeRepositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<BuyingStore>(cmd))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        _storeRepositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _mapperMock.Verify(x => x.Map<BuyingStore>(cmd));

        _storeRepositoryMock.VerifyNoOtherCalls();
        _storeItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task BuyingStoreSaveCommandHandlerTests_Handle_Update_ShouldBeOk()
    {
        // Arrange
        var cmd = ObjectMother.GetBuyingStoreSaveCmd();
        var store = ObjectMother.GetBuyingStore(new()
        {
            Id = 1,
            Name = "test"
        }, ObjectMother.ItemRedPotion);
        var storeItem = store.BuyingStoreItem;

        _storeRepositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(store);

        _mapperMock.Setup(x => x.Map<BuyingStore>(cmd))
                   .Returns(store);
        _mapperMock.Setup(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()))
                   .Returns(storeItem!);

        _storeRepositoryMock.Setup(x => x.Save(store))
            .ReturnsAsync(default(Success));

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Assert
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        _storeRepositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _mapperMock.Verify(x => x.Map<BuyingStore>(cmd));
        _mapperMock.Verify(x => x.Map<BuyingStoreItem>(cmd.StoreItems.FirstOrDefault()));
        _mediatorMock.Verify(x => x.Publish(It.IsAny<NewStoreNotification>(), CancellationToken.None));

        _storeItemRepositoryMock.Verify(x => x.DeleteAll(store.Id));
        _storeRepositoryMock.Verify(x => x.Remove(It.IsAny<BuyingStore>()));
        _storeRepositoryMock.Verify(x => x.Save(store));

        _storeRepositoryMock.VerifyNoOtherCalls();
        _storeItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }
}
