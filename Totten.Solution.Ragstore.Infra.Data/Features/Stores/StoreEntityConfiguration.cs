namespace Totten.Solution.Ragstore.Infra.Data.Features.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Stores;

public class StoreEntityConfiguration : IEntityTypeConfiguration<Store>
{
    const string TABLE_NAME = "Stories";
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.Property(b => b.Name).IsRequired();
        builder.Property(b => b.Merchant).IsRequired();
        builder.Property(b => b.Id).IsRequired();
        builder.Property(b => b.Items).IsRequired();
    }
}
