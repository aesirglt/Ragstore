namespace Totten.Solution.RagnaComercio.WebApi.Modules;

using Autofac;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Infra.Cross.Statics;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.RagnaComercio.WebApi.SystemConstants;

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
        ( Environment.GetEnvironmentVariable($"POSTGRES_DB_{Server}") ?? SysConstantDBConfig.DEFAULT_CONNECTION_STRING )
                            .Replace("{dbName}", Server)
                            .Apply(strConnection => new DbContextOptionsBuilder<ServerStoreContext>().UseNpgsql(strConnection))
                            .Apply(dbBuilder => builder.Register(context => new ServerStoreContext(dbBuilder.Options)))
                            .Apply(registration => registration.AsSelf().InstancePerLifetimeScope());
    }
}