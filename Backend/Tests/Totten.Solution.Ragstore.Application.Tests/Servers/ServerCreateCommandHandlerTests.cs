namespace Totten.Solution.Ragstore.Application.Tests.Servers;

using AutoMapper;
using Common.Tests.ObjectMothers;
using Moq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.ApplicationService.Features.Servers.CommandsHandler;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using FunctionalConcepts.Results;
using FluentAssertions;
using FunctionalConcepts.Errors;

[TestFixture]
public class ServerCreateCommandHandlerTests
{
    private Mock<IServerRepository> _serverRepositoryMock;
    private Mock<IMapper> _mapperMock;
    private ServerCreateCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _serverRepositoryMock = new();
        _mapperMock = new();
        _handler = new(_serverRepositoryMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task ServerCreateCommandHandlerTests_Handle_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server;
        var cmd = ObjectMother.GetServerCreate();

        _mapperMock.Setup(x => x.Map<Server>(cmd))
                   .Returns(server);

        _serverRepositoryMock.Setup(x => x.Save(server))
            .ReturnsAsync(Result.Success);

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsSuccess.Should().BeTrue();
        result.IsFail.Should().BeFalse();
        _mapperMock.Verify(x => x.Map<Server>(cmd));
        _mapperMock.VerifyNoOtherCalls();
        _serverRepositoryMock.Verify(x => x.Save(server));
        _serverRepositoryMock.VerifyNoOtherCalls();
    }

    [Test]
    public async Task ServerCreateCommandHandlerTests_Handle_SaveThrowsExnBut_ShouldBeError()
    {
        // Arrange
        var server = ObjectMother.Server;
        var cmd = ObjectMother.GetServerCreate();

        _mapperMock.Setup(x => x.Map<Server>(cmd))
                   .Returns(server);
        var exn = new InvalidDataException();
        _serverRepositoryMock.Setup(x => x.Save(server))
            .Throws(exn);

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        _mapperMock.Verify(x => x.Map<Server>(cmd));
        _mapperMock.VerifyNoOtherCalls();
        _serverRepositoryMock.Verify(x => x.Save(server));
        _serverRepositoryMock.VerifyNoOtherCalls();
        BaseError? baseError = null;
        result.Else(err => baseError = err);
        baseError.Should().NotBeNull();
        baseError.Should()
            .BeOfType<UnhandledError>()
            .Subject.Message
            .Should()
            .Be(exn.Message);
    }
    [Test]
    public async Task ServerCreateCommandHandlerTests_Handle_MapperThrowsExnBut_ShouldBeError()
    {
        // Arrange
        var server = ObjectMother.Server;
        var cmd = ObjectMother.GetServerCreate();

        var exn = new InvalidDataException();
        _mapperMock.Setup(x => x.Map<Server>(cmd))
                   .Throws(exn);

        // Act
        var result = await _handler.Handle(cmd, CancellationToken.None);

        // Asserts
        result.IsSuccess.Should().BeFalse();
        result.IsFail.Should().BeTrue();
        _mapperMock.Verify(x => x.Map<Server>(cmd));
        _mapperMock.VerifyNoOtherCalls();
        _serverRepositoryMock.VerifyNoOtherCalls();
        BaseError? baseError = null;
        result.Else(err => baseError = err);
        baseError.Should().NotBeNull();
        baseError.Should()
            .BeOfType<UnhandledError>()
            .Subject.Message
            .Should()
            .Be(exn.Message);
    }
}
