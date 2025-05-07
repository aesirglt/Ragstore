namespace Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;

internal class RagnaStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RagnaStoreContext>
{
    static string user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "admin";
    static string pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "Sup3rS3cr3t";

    public RagnaStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RagnaStoreContext>();
        optionsBuilder.UseNpgsql(
            $"Host={InfraConstants.MAIN_IP};Port=5432;Username={user};Password={pass};Database={InfraConstants.STORE_DB_NAME}"
            );

        return new RagnaStoreContext(optionsBuilder.Options);
    }
}
