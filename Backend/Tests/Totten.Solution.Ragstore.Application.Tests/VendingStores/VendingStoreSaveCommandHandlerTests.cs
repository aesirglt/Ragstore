namespace Totten.Solution.Ragstore.Application.Tests.VendingStores;
using AutoMapper;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Options;
using MediatR;
using Moq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;

[TestFixture]
public class VendingStoreSaveCommandHandlerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IVendingStoreRepository> _repositoryMock;
    private readonly Mock<IVendingStoreItemRepository> _vendingStoreItemRepositoryMock;
    private readonly VendingStoreSaveCommandHandler _handler;

    public VendingStoreSaveCommandHandlerTests()
    {
        _mediatorMock = new();
        _mapperMock = new();
        _repositoryMock = new();
        _vendingStoreItemRepositoryMock = new();
        _handler = new(_mediatorMock.Object,
                       _mapperMock.Object,
                       _repositoryMock.Object,
                       _vendingStoreItemRepositoryMock.Object);
    }

    [Test]
    public async Task VendingStoreSaveCommandHandlerTests_Handle_Save_ShouldBeOk()
    {
        // Arrange 
        var cmd = ObjectMother.GetVendingStoreSaveCmd();

        var store = ObjectMother.GetVendingStore(new()
        {
            Id = 1,
            Name = "test"
        }, [ObjectMother.ItemRedPotion]);

        _repositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<VendingStore>(cmd))
                   .Returns(store);

        _repositoryMock.Setup(x => x.Save(store))
            .ReturnsAsync(default(Success));

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _repositoryMock.Verify(x => x.Save(store));
        _mapperMock.Verify(x => x.Map<VendingStore>(cmd));
        _mediatorMock.Verify(x => x.Publish(It.IsAny<NewStoreNotification>(),
                                            CancellationToken.None));

        _repositoryMock.VerifyNoOtherCalls();
        _vendingStoreItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task VendingStoreSaveCommandHandlerTests_Handle_SaveThrowExn_ShouldBeError()
    {
        // Arrange 
        var cmd = ObjectMother.GetVendingStoreSaveCmd();

        var store = ObjectMother.GetVendingStore(new()
        {
            Id = 1,
            Name = "test"
        }, [ObjectMother.ItemRedPotion]);

        _repositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<VendingStore>(cmd))
                   .Returns(store);

        _repositoryMock.Setup(x => x.Save(store))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        _repositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _repositoryMock.Verify(x => x.Save(store));
        _mapperMock.Verify(x => x.Map<VendingStore>(cmd));

        _repositoryMock.VerifyNoOtherCalls();
        _vendingStoreItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task VendingStoreSaveCommandHandlerTests_Handle_MapThrowExn_ShouldBeError()
    {
        // Arrange 
        var cmd = ObjectMother.GetVendingStoreSaveCmd();

        _repositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(NoneType.Value);

        _mapperMock.Setup(x => x.Map<VendingStore>(cmd))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        _repositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _mapperMock.Verify(x => x.Map<VendingStore>(cmd));

        _repositoryMock.VerifyNoOtherCalls();
        _vendingStoreItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task VendingStoreSaveCommandHandlerTests_Handle_GetByCharacterId_ReturnSome_ShouldBeError()
    {
        // Arrange 
        var cmd = ObjectMother.GetVendingStoreSaveCmd();

        _repositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsFail.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        _repositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));

        _repositoryMock.VerifyNoOtherCalls();
        _vendingStoreItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task VendingStoreSaveCommandHandlerTests_Handle_Update_ShouldBeOk()
    {
        // Arrange 
        var cmd = ObjectMother.GetVendingStoreSaveCmd();

        var store = ObjectMother.GetVendingStore(new()
        {
            Id = 1,
            Name = "test"
        }, [ObjectMother.ItemRedPotion]);

        _repositoryMock.Setup(x => x.GetByCharacterId(cmd.CharacterId))
            .Returns(store);

        _mapperMock.Setup(x => x.Map(cmd, store))
                   .Returns(store);

        _repositoryMock.Setup(x => x.Save(store))
            .ReturnsAsync(default(Success));

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsFail.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        _repositoryMock.Verify(x => x.GetByCharacterId(cmd.CharacterId));
        _repositoryMock.Verify(x => x.Update(store));
        _mapperMock.Verify(x => x.Map(cmd, store));
        _mediatorMock.Verify(x => x.Publish(It.IsAny<NewStoreNotification>(),
                                            CancellationToken.None));

        _vendingStoreItemRepositoryMock.Verify(x => x.DeleteAll(store.Id));
        _vendingStoreItemRepositoryMock.Verify(x => x.Save(It.IsAny<VendingStoreItem>()));
        _repositoryMock.VerifyNoOtherCalls();
        _vendingStoreItemRepositoryMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }
}
