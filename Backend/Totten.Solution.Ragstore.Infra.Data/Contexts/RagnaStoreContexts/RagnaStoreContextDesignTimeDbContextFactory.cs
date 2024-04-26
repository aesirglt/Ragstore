namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;

internal class RagnaStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RagnaStoreContext>
{
    public RagnaStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RagnaStoreContext>();
        optionsBuilder.UseSqlServer(
            $"Server=192.168.2.102;Database={InfraConstants.STORE_DB_NAME};User Id=sa;Password=Sup3rS3cr3t;TrustServerCertificate=true;"
            );

        return new RagnaStoreContext(optionsBuilder.Options);
    }
}
