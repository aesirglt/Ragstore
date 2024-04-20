namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal class RagnaStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RagnaStoreContext>
{
    public RagnaStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RagnaStoreContext>();
        optionsBuilder.UseSqlServer(
            "Server=192.168.2.102;Database=RagnaStoreContext;User Id=sa;Password=Sup3rS3cr3t;TrustServerCertificate=true;"
            );

        return new RagnaStoreContext(optionsBuilder.Options);
    }
}
