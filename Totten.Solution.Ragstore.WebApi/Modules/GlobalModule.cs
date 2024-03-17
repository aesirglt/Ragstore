namespace Totten.Solution.Ragstore.WebApi.Modules;

using Autofac;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Totten.Solution.Ragstore.ApplicationService;
using Totten.Solution.Ragstore.Domain.Features.Items;
using Totten.Solution.Ragstore.Domain.Features.Stores;
using Totten.Solution.Ragstore.Infra.Data.Features.Items;
using Totten.Solution.Ragstore.Infra.Data.Features.Stores;
using Totten.Solution.Ragstore.WebApi.AppSettings;

public class GlobalModule<TProgram> : Autofac.Module
{
    IConfigurationRoot Configuration { get; }
    public GlobalModule(IConfigurationRoot configuration)
    {
        Configuration = configuration;
    }
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