namespace Totten.Solution.Ragstore.Infra.Data.Features.ItemsAggregation.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;

internal class EquipmentItemEntityConfiguration : IEntityTypeConfiguration<EquipmentItem>
{
    const string TABLE_NAME = "EquipmentItems";
    public void Configure(EntityTypeBuilder<EquipmentItem> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
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
        builder.Property(e => e.IsIdentified).IsRequired();
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
                        ? Array.Empty<EquipmentItemCardInfo>()
                        : str.Split('#', StringSplitOptions.RemoveEmptyEntries)
                             .Select(item => new EquipmentItemCardInfo(item))
                             .ToArray());

        builder.Property(e => e.InfoOptions)
               .HasConversion(
                list => list.Count() == 0
                    ? string.Empty
                    : string.Join('#', list.Select(item => $"{item.Id}:{item.Val}:{item.Param}:{item.Name}")),
                str => string.IsNullOrEmpty(str)
                    ? Array.Empty<EquipmentItemOptionInfo>()
                    : str.Split('#', StringSplitOptions.RemoveEmptyEntries)
                         .Select(item => new EquipmentItemOptionInfo(item))
                         .ToArray());
        builder.Property(e => e.CrafterId);
        builder.Property(e => e.CrafterName);

        builder.HasIndex(e => e.CharacterId);
        builder.HasIndex(e => e.AccountId);

        //builder.HasOne(e => e.Chat)
        //       .WithMany(e => e.EquipmentItems)
        //       .HasForeignKey(e => e.CharacterId)
        //       .OnDelete(DeleteBehavior.NoAction);

    }
}
