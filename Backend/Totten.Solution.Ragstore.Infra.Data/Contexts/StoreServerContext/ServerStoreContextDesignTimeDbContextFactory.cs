namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Totten.Solution.Ragstore.Infra.Data.Bases;

internal class ServerStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ServerStoreContext>
{
    static string user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "admin";
    static string pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "Sup3rS3cr3t";
    public ServerStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ServerStoreContext>();
        optionsBuilder.UseNpgsql(
            $"Host={InfraConstants.MAIN_IP};Port=5432;Username={user};Password={pass};Database=broTHOR"
            );

        return new ServerStoreContext(optionsBuilder.Options);
    }
}
