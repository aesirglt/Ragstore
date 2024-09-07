namespace Totten.Solution.Ragstore.Infra.Data.Tests.Servers;

using Common.Tests;
using Common.Tests.ObjectMothers;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Infra.Data.Features.Servers;

[TestFixture]
public class ServerRepositoryTests
{
    private ServerRepository _serverRepository;

    [SetUp]
    public void Setup()
    {
        _serverRepository = new ServerRepository(ContextObjectMother.GetInMemoryRagnaStore());
    }

    [Test]
    public async Task ServerRepositoryTests_Save_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server;

        // Act
        var act = async () => await _serverRepository.Save(server);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task ServerRepositoryTests_Remove_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server with { Id = 2 };
        _ = await _serverRepository.Save(server);
        // Act
        var act = async () => await _serverRepository.Remove(server);

        // Assert
        await act.Should().NotThrowAsync();
    }

    [Test]
    public async Task ServerRepositoryTests_Update_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server with { Id = 2 };
        _ = await _serverRepository.Save(server);
        var newServerName = "bRO - Valhalla";
        var serverUpdated = server with { Name = newServerName };

        // Act
        var act = async () => await _serverRepository.Update(serverUpdated);

        // Assert
        await act.Should().NotThrowAsync();
        var serverInDbOpt = await _serverRepository.GetById(server.Id);
        Server? serverInDb = null;
        serverInDbOpt.Then(srv => serverInDb = srv);
        serverInDb.Should().NotBeNull();
        serverInDb!.Name.Should().Be(newServerName);
    }

    [Test]
    public async Task ServerRepositoryTests_GetAll_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server with { Id = 4 };

        _ = await _serverRepository.Save(server);

        // Act
        var servers = _serverRepository.GetAll().ToList();

        // Assert
        servers.Should().NotBeNullOrEmpty();
        servers.FirstOrDefault(x => x.Id == server.Id).Should().NotBeNull();
    }

    [Test]
    public async Task ServerRepositoryTests_GetById_ShouldBeOk()
    {
        // Arrange
        var server = ObjectMother.Server with { Id = 5 };

        _ = await _serverRepository.Save(server);

        // Act
        var serverInDbOpt = await _serverRepository.GetById(server.Id);

        // Assert
        serverInDbOpt.IsNone.Should().BeFalse();
        serverInDbOpt.IsSome.Should().BeTrue();
        Server? serverInDb = null;
        serverInDbOpt.Then(srv => serverInDb = srv);
        serverInDb.Should().NotBeNull();
    }
}
