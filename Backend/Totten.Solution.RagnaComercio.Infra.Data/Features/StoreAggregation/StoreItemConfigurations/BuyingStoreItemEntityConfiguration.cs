﻿namespace Totten.Solution.RagnaComercio.Infra.Data.Features.StoreAggregation.StoreItemConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Bases;
using Totten.Solution.RagnaComercio.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreItemEntityConfiguration : IEntityTypeConfiguration<BuyingStoreItem>
{
    const string TABLE_NAME = "BuyingStoreItems";
    public void Configure(EntityTypeBuilder<BuyingStoreItem> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.AccountId).IsRequired();
        builder.Property(e => e.CharacterId).IsRequired();
        builder.Property(e => e.ItemId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.Type).IsRequired();
        builder.Property(e => e.Refine).IsRequired();
        builder.Property(e => e.EnchantGrade);
        builder.Property(e => e.IsIdentified).IsRequired();
        builder.Property(e => e.IsDamaged).IsRequired();
        builder.Property(e => e.StoreName).IsRequired();
        builder.Property(e => e.CharacterName).IsRequired();
        builder.Property(e => e.Map).IsRequired();
        builder.Property(e => e.IsDamaged).IsRequired();
        builder.Property(e => e.Location);
        builder.Property(e => e.SpriteId);
        builder.Property(e => e.Slots);

        builder.Property(e => e.InfoCards)
                   .HasConversion(
                    list => list.Count() == 0
                        ? string.Empty
                        : string.Join('#', list.Select(item => $"{item.Id}:{item.Name}")),
                    str => string.IsNullOrEmpty(str)
                        ? Array.Empty<StoreItemCardInfo>()
                        : str.Split('#', StringSplitOptions.RemoveEmptyEntries)
                             .Select(item => new StoreItemCardInfo(item))
                             .ToArray());

        builder.Property(e => e.InfoOptions)
               .HasConversion(
                list => list.Count() == 0
                    ? string.Empty
                    : string.Join('#', list.Select(item => $"{item.Id}:{item.Val}:{item.Param}:{item.Name}")),
                str => string.IsNullOrEmpty(str)
                    ? Array.Empty<StoreItemOptionInfo>()
                    : str.Split('#', StringSplitOptions.RemoveEmptyEntries)
                         .Select(item => new StoreItemOptionInfo(item))
                         .ToArray());

        builder.Property(e => e.CrafterId);
        builder.Property(e => e.CrafterName);

        builder.HasOne(bi => bi.BuyingStore)
            .WithOne(b => b.BuyingStoreItem)
            .HasForeignKey<BuyingStoreItem>(bi => bi.StoreId);

        builder.HasIndex(e => e.CharacterId);
        builder.HasIndex(e => e.Name);
    }
}
