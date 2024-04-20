namespace Totten.Solution.Ragstore.Infra.Data.Features.Accounts.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Accounts;
using Totten.Solution.Ragstore.Infra.Data.Seeds;

public class AccountEntityConfiguration : IEntityTypeConfiguration<Account>
{
    const string TABLE_NAME = "Accounts";
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.UserId);
        builder.Property(e => e.IsReported).IsRequired();

        builder.HasMany(e => e.VendingStores)
               .WithOne()
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.BuyingStores)
               .WithOne()
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(e => e.Chats)
               .WithOne()
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne()
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Characters)
               .WithOne()
               .HasForeignKey(e => e.AccountId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(e => e.User);
        builder.HasIndex(x => x.UserId);

        builder.HasData(MyAccountSeed.Seed());
    }
}
