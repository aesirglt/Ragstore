namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAggregation.BuyingStores.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.StoresAggregation.Buyings;

public class BuyingStoreEntityConfiguration : IEntityTypeConfiguration<BuyingStore>
{
    const string TABLE_NAME = "BuyingStores";
    public void Configure(EntityTypeBuilder<BuyingStore> builder)
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
        builder.Property(e => e.PriceLimit).IsRequired();

        builder.HasIndex(e => e.CharacterId).IsUnique();
    }
}
