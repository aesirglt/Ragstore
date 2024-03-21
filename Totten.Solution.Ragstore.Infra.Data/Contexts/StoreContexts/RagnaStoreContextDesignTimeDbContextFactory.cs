namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

internal class RagnaStoreContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RagnaStoreContext>
{
    public RagnaStoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RagnaStoreContext>();
        optionsBuilder.UseSqlite("Data Source=RagnaStore.db");

        return new RagnaStoreContext(optionsBuilder.Options);
    }
}
