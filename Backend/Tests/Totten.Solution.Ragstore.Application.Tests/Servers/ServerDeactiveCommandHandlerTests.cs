namespace Totten.Solution.Ragstore.Application.Tests.Servers;

using Common.Tests.ObjectMothers;
using FluentAssertions;
using FunctionalConcepts.Errors;
using FunctionalConcepts.Options;
using Moq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.Commands;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using FunctionalConcepts.Results;

[TestFixture]
public class ServerDeactiveCommandHandlerTests
{
    private Mock<IServerRepository> _serverRepositoryMock;
    private ServerDeactiveCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _serverRepositoryMock = new Mock<IServerRepository>();
        _handler = new ServerDeactiveCommandHandler(_serverRepositoryMock.Object);
    }

    [Test]
    public async Task ServerDeactiveCommandHandler_Handle_Deactive_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server with { IsActive = true };
        var cmd = new ServerDeactiveCommand
        {
            ServerId = server.Id
        };
        CancellationToken cancellationToken = default;
        _serverRepositoryMock.Setup(x => x.GetById(server.Id))
            .ReturnsAsync(server);
        _serverRepositoryMock.Setup(x => x.Update(server))
            .ReturnsAsync(Result.Success);

        // Act
        var result = await _handler.Handle(cmd, cancellationToken);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.IsFail.Should().BeFalse();
        server.IsActive.Should().BeFalse();
        _serverRepositoryMock.Verify(x => x.GetById(cmd.ServerId));
        _serverRepositoryMock.Verify(x => x.Update(server));
        _serverRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task ServerDeactiveCommandHandler_Handle_Deactive_ServerNotFound_ShouldBeError()
    {
        // Arrange
        var cmd = new ServerDeactiveCommand
        {
            ServerId = 1
        };
        CancellationToken cancellationToken = default;
        _serverRepositoryMock.Setup(x => x.GetById(cmd.ServerId))
            .ReturnsAsync(NoneType.Value);

        // Act
        var result = await _handler.Handle(cmd, cancellationToken);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        BaseError? baseError = null;
        result.Else(err => baseError = err);
        baseError.Should().NotBeNull();
        baseError.Should()
            .BeOfType<NotFoundError>()
            .Subject.Message
            .Should()
            .Be("server not found");
        _serverRepositoryMock.Verify(x => x.GetById(cmd.ServerId));
        _serverRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task ServerDeactiveCommandHandler_Handle_RepositoryThrowExn_But_ShouldBeError()
    {
        // Arrange
        var cmd = new ServerDeactiveCommand
        {
            ServerId = 1
        };
        CancellationToken cancellationToken = default;
        _serverRepositoryMock.Setup(x => x.GetById(cmd.ServerId))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, cancellationToken);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        BaseError? baseError = null;
        result.Else(err => baseError = err);
        baseError.Should().NotBeNull();
        baseError.Should()
            .BeOfType<UnhandledError>();
        _serverRepositoryMock.Verify(x => x.GetById(cmd.ServerId));
        _serverRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task ServerDeactiveCommandHandler_Handle_RepositoryUpdateThrowExn_But_ShouldBeError()
    {
        // Arrange
        var server = ObjectMother.Server with { IsActive = true };
        var cmd = new ServerDeactiveCommand
        {
            ServerId = server.Id
        };
        CancellationToken cancellationToken = default;
        _serverRepositoryMock.Setup(x => x.GetById(server.Id))
            .ReturnsAsync(server);

        _serverRepositoryMock.Setup(x => x.Update(server))
            .Throws<InvalidDataException>();

        // Act
        var result = await _handler.Handle(cmd, cancellationToken);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        BaseError? baseError = null;
        result.Else(err => baseError = err);
        baseError.Should().NotBeNull();
        baseError.Should()
            .BeOfType<UnhandledError>()
            .Subject.Message.Should()
            .Be("Error for updating server, contact the admin.");
        _serverRepositoryMock.Verify(x => x.GetById(cmd.ServerId));
        _serverRepositoryMock.Verify(x => x.Update(server));
        _serverRepositoryMock.VerifyNoOtherCalls();
    }
}
