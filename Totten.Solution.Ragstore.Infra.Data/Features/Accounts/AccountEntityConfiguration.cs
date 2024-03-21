namespace Totten.Solution.Ragstore.Infra.Data.Features.StoreAgregattion;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Accounts;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    const string TABLE_NAME = "Accounts";
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.AccountId).IsRequired();
        builder.Property(e => e.UserId);
        builder.Property(e => e.IsReported).IsRequired();

        builder.HasMany(e => e.VendingStores)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId);

        builder.HasMany(e => e.BuyingStores)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId);

        builder.HasMany(e => e.BuyingStoreItems)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId);
    }
}
