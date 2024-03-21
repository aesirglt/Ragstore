namespace Totten.Solution.Ragstore.Infra.Data.Contexts.EntityFrameworkIdentity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

public class AppIdentityDesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppIdentityContext>
{
    public AppIdentityContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppIdentityContext>();
        optionsBuilder.UseSqlite("Data Source=app.db");

        return new AppIdentityContext(optionsBuilder.Options);
    }
}
