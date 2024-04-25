namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Infra.Cross.Functionals;
using Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Totten.Solution.Ragstore.WebApi.SystemConstants;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TProgram"></typeparam>
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
                            .Apply(strConnection => new DbContextOptionsBuilder<RagnaStoreContext>().UseSqlServer(strConnection))
                            .Apply(dbBuilder => builder.Register(context => new RagnaStoreContext(dbBuilder.Options)))
                            .Apply(registration => registration.AsSelf().InstancePerLifetimeScope());
    }
}