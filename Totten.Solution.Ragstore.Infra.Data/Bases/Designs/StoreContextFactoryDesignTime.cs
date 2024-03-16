namespace Totten.Solution.Ragstore.Infra.Data.Bases.Designs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

public class BookStoreContextFactoryDesignTime : IDesignTimeDbContextFactory<StoreContext>
{
    private string _localConnectionString = @"Host=my_host;Database=my_db;Username=my_user;Password=my_pw;TrustServerCertificate=true;";
    public StoreContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<StoreContext>();
        builder.UseNpgsql(_localConnectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));

        return new StoreContext(builder.Options);
    }
}