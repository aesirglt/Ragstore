namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.ApplicationService;
using Totten.Solution.Ragstore.ApplicationService.DTOs.Messages;
using Totten.Solution.Ragstore.ApplicationService.Interfaces;
using Totten.Solution.Ragstore.ApplicationService.Services;
using Totten.Solution.Ragstore.Domain.Features.AgentAggregation;
using Totten.Solution.Ragstore.Domain.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Domain.Features.Servers;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Bases;
using Totten.Solution.Ragstore.Infra.Data.Contexts.RagnaStoreContexts;
using Totten.Solution.Ragstore.Infra.Data.Features.Agents;
using Totten.Solution.Ragstore.Infra.Data.Features.CallbackAggregation;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemAggregation;
using Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation;
using Totten.Solution.Ragstore.Infra.Data.Features.Servers;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.VendingStores;
using Totten.Solution.Ragstore.WebApi.SystemConstants;

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

        builder.RegisterType<WhatsAPPService>()
               .As<IMessageService<NotificationMessageDto>>()
               .InstancePerLifetimeScope();

        builder.RegisterType<ServerRepository>()
               .As<IServerRepository>()
               .InstancePerLifetimeScope();

        builder.RegisterType<BuyingStoreRepository>()
               .As<IBuyingStoreRepository>()
               .InstancePerLifetimeScope();
        builder.RegisterType<BuyingStoreItemRepository>()
               .As<IBuyingStoreItemRepository>()
               .InstancePerLifetimeScope();

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

        builder.RegisterType<CallbackScheduleRepository>()
               .As<ICallbackScheduleRepository>()
               .InstancePerLifetimeScope();
        
        builder.RegisterType<AgentRepository>()
               .As<IAgentRepository>()
               .InstancePerLifetimeScope();
        
        builder.Register(_ => Configuration)
               .As<IConfigurationRoot>()
               .InstancePerLifetimeScope();

        builder.Register(ctx =>
        {
            var strConn = SysConstantDBConfig.DEFAULT_CONNECTION_STRING
                                             .Replace("{dbName}", InfraConstants.STORE_DB_NAME);
            var opt = new DbContextOptionsBuilder<RagnaStoreContext>()
                                             .UseSqlServer(strConn)
                                             .Options;
            return new RagnaStoreContext(opt);
        }).AsSelf()
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