namespace Totten.Solution.RagnaComercio.WebApi.Modules;

using Autofac;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.ApplicationService;
using Totten.Solution.RagnaComercio.ApplicationService.DTOs.Messages;
using Totten.Solution.RagnaComercio.ApplicationService.Interfaces;
using Totten.Solution.RagnaComercio.ApplicationService.Services;
using Totten.Solution.RagnaComercio.Domain.Features.AgentAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.Servers;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Domain.Features.Users;
using Totten.Solution.RagnaComercio.Infra.Data.Bases;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.RagnaStoreContexts;
using Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Agents;
using Totten.Solution.RagnaComercio.Infra.Data.Features.CallbackAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Features.ItemAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Servers;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.BuyingStores;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.SearchedItems;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.VendingStores;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Users;
using Totten.Solution.RagnaComercio.WebApi.SystemConstants;

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

        builder.RegisterType<SearchedItemRepository>()
               .As<ISearchedItemRepository>()
               .InstancePerLifetimeScope();

        builder.RegisterType<CallbackRepository>()
               .As<ICallbackRepository>()
               .InstancePerLifetimeScope();

        builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
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
                                             .UseNpgsql(strConn)
                                             .Options;
            return new RagnaStoreContext(opt);
        }).AsSelf()
        .InstancePerLifetimeScope();

        builder.Register(ctx =>
        {
            var strConn = SysConstantDBConfig.DEFAULT_CONNECTION_STRING
                                             .Replace("{dbName}", InfraConstants.PRINCIPAL_SERVER_DB_NAME);
            var opt = new DbContextOptionsBuilder<ServerStoreContext>()
                                             .UseNpgsql(strConn)
                                             .Options;
            return new ServerStoreContext(opt);
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