namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Cross.Statics;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.Ragstore.WebApi.SystemConstants;

/// <summary>
/// 
/// </summary>
public class TenantModule : Autofac.Module
{
    /// <summary>
    /// 
    /// </summary>
    public required string? Server { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        SysConstantDBConfig.DEFAULT_CONNECTION_STRING
                            .Replace("{dbName}", Server)
                            .Apply(strConnection => new DbContextOptionsBuilder<ServerStoreContext>().UseSqlServer(strConnection))
                            .Apply(dbBuilder => builder.Register(context => new ServerStoreContext(dbBuilder.Options)))
                            .Apply(registration => registration.AsSelf().InstancePerLifetimeScope());
    }
}