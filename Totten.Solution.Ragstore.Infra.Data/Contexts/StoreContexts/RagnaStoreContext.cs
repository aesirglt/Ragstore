namespace Totten.Solution.Ragstore.Infra.Data.Contexts.StoreContexts;
using Microsoft.EntityFrameworkCore;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Domain.Features.Agents;
using Totten.Solution.Ragstore.Domain.Features.Callbacks;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Domain.Features.Chats;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Buyings;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;
using Totten.Solution.Ragstore.Infra.Data.Features.Accounts;
using Totten.Solution.Ragstore.Infra.Data.Features.Items;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion.BuyingStores;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion.StoreItems;
using Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion.VendingStores;

public class RagnaStoreContext : DbContext
{
    public virtual DbSet<VendingStore> VendingStores { get; set; }
    //public virtual DbSet<BuyingStore> BuyingStores { get; set; }
    //public virtual DbSet<BuyingStoreItem> BuyingStoreItems { get; set; }
    public virtual DbSet<VendingStoreItem> VendingStoreItems { get; set; }
    //public virtual DbSet<Item> Items { get; set; }
    //public virtual DbSet<Chat> Chats { get; set; }
    //public virtual DbSet<Character> Characters { get; set; }
    //public virtual DbSet<Callback> Callbacks { get; set; }
    //public virtual DbSet<UpdateTime> UpdateTime { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }

    public RagnaStoreContext(DbContextOptions<RagnaStoreContext> options) : base(options)
    {
        Database?.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VendingStoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new VendingStoreItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BuyingStoreEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BuyingStoreItemEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AccountEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public virtual IQueryable<T> AsNoTracking<T>(IQueryable<T> query) where T : class
        => EntityFrameworkQueryableExtensions.AsNoTracking<T>(query);
}
