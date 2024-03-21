namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.StoresAgreggation.Vendings;

public class VendingStoreEntityConfiguration : IEntityTypeConfiguration<VendingStore>
{
    const string TABLE_NAME = "Books";
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

        builder.HasMany(e => e.StoreItems)
               .WithOne(s => s.VendingStore);
    }
}
