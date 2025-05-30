﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.VendingStores.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Vendings;
using Totten.Solution.RagnaComercio.Infra.Data.Seeds;

public class VendingStoreEntityConfiguration : IEntityTypeConfiguration<VendingStore>
{
    const string TABLE_NAME = "VendingStores";
    public void Configure(EntityTypeBuilder<VendingStore> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.AccountId).IsRequired();
        builder.Property(e => e.CharacterId).IsRequired();
        builder.Property(e => e.Map).IsRequired();
        builder.Property(e => e.Location).IsRequired();
        builder.Property(e => e.ExpireDate);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne()
               .HasForeignKey(e => e.StoreId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.CharacterId).IsUnique();

        builder.HasData(MyVendingStoreSeed.Seed());
    }
}
