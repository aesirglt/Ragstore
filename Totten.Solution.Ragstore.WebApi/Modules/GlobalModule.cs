namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Totten.Solution.Ragstore.ApplicationService;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation;
using Totten.Solution.Ragstore.Infra.Data.Features.Callbacks;
using Totten.Solution.Ragstore.Infra.Data.Features.Items;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;
using Totten.Solution.Ragstore.WebApi.AppSettings;

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
        builder.Register(ctx =>
        {
            var settings = ctx.Resolve<IOptions<StoreDatabaseSettings>>().Value;
            var client = new MongoClient(MongoClientSettings.FromConnectionString(settings.ConnectionString));
            return client.GetDatabase(settings.DatabaseName);
        }).As<IMongoDatabase>()
          .InstancePerLifetimeScope();

        builder.RegisterType<MongoClient>()
               .AsSelf()
               .InstancePerLifetimeScope();

        builder.Register(ctx => new StoreRepository(ctx.Resolve<IMongoDatabase>(), ctx.Resolve<IOptions<StoreDatabaseSettings>>().Value.StoreCollectionName))
               .As<IStoreRepository>()
               .InstancePerLifetimeScope();

        builder.Register(ctx => new ItemRepository(ctx.Resolve<IMongoDatabase>(), ctx.Resolve<IOptions<StoreDatabaseSettings>>().Value.ItemCollectionName))
               .As<IItemRepository>()
               .InstancePerLifetimeScope();

        builder.Register(ctx => new CallbackRepository(ctx.Resolve<IMongoDatabase>(), ctx.Resolve<IOptions<StoreDatabaseSettings>>().Value.CallbackCollectionName))
               .As<ICallbackRepository>()
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