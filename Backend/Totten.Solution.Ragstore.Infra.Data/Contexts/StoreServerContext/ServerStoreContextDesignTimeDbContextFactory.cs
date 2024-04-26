namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal class ServerStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ServerStoreContext>
{
    public ServerStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ServerStoreContext>();
        optionsBuilder.UseSqlServer(
            "Server=192.168.2.102;Database=ServerStore;User Id=sa;Password=Sup3rS3cr3t;TrustServerCertificate=true;"
            );

        return new ServerStoreContext(optionsBuilder.Options);
    }
}
