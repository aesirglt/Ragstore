namespace Totten.Solution.Ragstore.Application.Tests.VendingStores;
using AutoMapper;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using FunctionalConcepts;
using FunctionalConcepts.Options;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.StoreAgregattion.CommandsHandler;
using Totten.Solution.Ragstore.ApplicationService.Notifications.Stores;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;

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
    public async Task VendingStoreSaveCommandHandlerTests_Handle_ShouldBeOk()
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
        _mapperMock.VerifyNoOtherCalls();
        _mediatorMock.VerifyNoOtherCalls();
    }
}
