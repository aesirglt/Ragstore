namespace Totten.Solution.Ragstore.Infra.Data.Features.Accounts;

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
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.BuyingStores)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(e => e.Chats)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Characters)
               .WithOne(s => s.Account)
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(e => e.User);

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(x => x.UserId);
    }
}
