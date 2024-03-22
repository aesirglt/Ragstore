namespace Totten.Solution.Ragstore.Infra.Data.Features.Items;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.ItemAgreggation;

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
               .WithOne(e => e.Item)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne(e => e.Item)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(e => e.EquipmentItems);
    }
}
