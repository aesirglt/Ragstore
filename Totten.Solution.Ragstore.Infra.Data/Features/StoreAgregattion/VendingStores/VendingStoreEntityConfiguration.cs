namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion.VendingStores;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

public class VendingStoreEntityConfiguration : IEntityTypeConfiguration<VendingStore>
{
    const string TABLE_NAME = "VendingStores";
    public void Configure(EntityTypeBuilder<VendingStore> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.AccountId).IsRequired();
        builder.Property(e => e.CharacterId).IsRequired();
        builder.Property(e => e.Map).IsRequired();
        builder.Property(e => e.Location).IsRequired();
        builder.Property(e => e.ExpireDate);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne(s => s.VendingStore)
               .HasForeignKey(e => e.AccountId);

        builder.Ignore(e => e.Character);
        //builder.HasMany(e => e.BuyingStores)
        //       .WithOne(s => s.Account)
        //       .HasForeignKey(e => e.AccountId);
        //builder.HasMany(e => e.BuyingStoreItems)
        //       .WithOne(s => s.Account)
        //       .HasForeignKey(e => e.AccountId);
        //builder.HasMany(e => e.EquipmentItems)
        //       .WithOne(s => s.Account)
        //       .HasForeignKey(e => e.AccountId);
    }
}
