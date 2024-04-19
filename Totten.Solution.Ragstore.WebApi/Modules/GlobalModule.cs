namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using AutoMapper;
using Totten.Solution.Ragstore.ApplicationService;
using Totten.Solution.Ragstore.Domain.Features.Agents;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Features.Agents;
using Totten.Solution.Ragstore.Infra.Data.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemAggregation;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.VendingStores;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TProgram"></typeparam>
public class GlobalModule<TProgram> : Autofac.Module
{
    IConfigurationRoot Configuration { get; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public GlobalModule(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<VendingStoreRepository>()
               .As<IVendingStoreRepository>()
               .InstancePerLifetimeScope();
        builder.RegisterType<VendingStoreItemRepository>()
               .As<IVendingStoreItemRepository>()
               .InstancePerLifetimeScope();
        builder.RegisterType<ItemRepository>()
               .As<IItemRepository>()
               .InstancePerLifetimeScope();
        builder.RegisterType<CallbackRepository>()
               .As<ICallbackRepository>()
               .InstancePerLifetimeScope();
        builder.RegisterType<UpdateTimeRepository>()
               .As<IUpdateTimeRepository>()
               .InstancePerLifetimeScope();

        builder.Register(_ => Configuration)
               .As<IConfigurationRoot>()
               .InstancePerLifetimeScope();

        builder.Register(ctx =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(TProgram));
                cfg.AddMaps(typeof(ApplicationAssembly));
            });

            return configuration.CreateMapper();
        }).As<IMapper>()
          .InstancePerLifetimeScope();
    }
}