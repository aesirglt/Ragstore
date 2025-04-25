namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.SearchedItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.ItemsAggregation;

internal class SearchedItemsEntityConfiguration : IEntityTypeConfiguration<SearchedItem>
{
    const string TABLE_NAME = "SearchedItems";
    public void Configure(EntityTypeBuilder<SearchedItem> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.ItemId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.Average).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
    }
}
