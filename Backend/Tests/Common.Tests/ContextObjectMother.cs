namespace Common.Tests;

using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Data.Contexts.EntityFrameworkIdentity;
using Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;

public static class ContextObjectMother
{
    public static RagnaStoreContext GetInMemoryRagnaStore()
    {
        var options = new DbContextOptionsBuilder<RagnaStoreContext>()
            .UseInMemoryDatabase(databaseName: "RagnaStoreContextInMemoryDb")
        .Options;

        return new(options);
    }

    public static ServerStoreContext GetInMemoryServerStore(string dbName)
    {
        var options = new DbContextOptionsBuilder<ServerStoreContext>()
            .UseInMemoryDatabase(databaseName: dbName)
        .Options;

        return new(options);
    }

    public static AppIdentityContext GetInMemoryAppIdentity()
    {
        var options = new DbContextOptionsBuilder<AppIdentityContext>()
            .UseInMemoryDatabase(databaseName: "AppIdentityContextInMemoryDb")
        .Options;

        return new(options);
    }
}
