namespace Totten.Solution.RagnaComercio.Infra.Data.Contexts.StoreServerContext;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.RagnaComercio.Domain.Features.Accounts;
using Totten.Solution.RagnaComercio.Domain.Features.Characters;
using Totten.Solution.RagnaComercio.Domain.Features.Chats;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Accounts.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Characters.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.Chats.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.ItemsAggregation.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.BuyingStores.EntityConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.SearchedItems;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.StoreItemConfigurations;
using Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.VendingStores.EntityConfigurations;

public class ServerStoreContext : DbContext
{
    public virtual DbSet<Item> Items { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<VendingStore> VendingStores { get; set; }
    public virtual DbSet<VendingStoreItem> VendingStoreItems { get; set; }
    public virtual DbSet<BuyingStore> BuyingStores { get; set; }
    public virtual DbSet<BuyingStoreItem> BuyingStoreItems { get; set; }
    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<SearchedItem> SearchedItems { get; set; }

    public ServerStoreContext(DbContextOptions<ServerStoreContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VendingStoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VendingStoreItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BuyingStoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BuyingStoreItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new SearchedItemsEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }
}
