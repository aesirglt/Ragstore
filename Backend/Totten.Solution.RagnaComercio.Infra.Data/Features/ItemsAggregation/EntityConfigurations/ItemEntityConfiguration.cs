﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Features.ItemsAggregation.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.ItemsAggregation;
using Totten.Solution.RagnaComercio.Infra.Data.Seeds;

public class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
{
    const string TABLE_NAME = "Items";
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.Type);
        builder.Property(e => e.SubType);
        builder.Property(e => e.Slots);
        builder.Property(e => e.Description);

        builder.HasMany(e => e.BuyingStoreItems)
               .WithOne()
               .HasForeignKey(e => e.ItemId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne()
               .HasForeignKey(e => e.ItemId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne()
               .HasForeignKey(e => e.ItemId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasData(MyItemSeed.Seed());

        builder.HasIndex(e => e.Name);
    }
}
