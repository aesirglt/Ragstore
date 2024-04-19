namespace Totten.Solution.Ragstore.Infra.Data.Features.Characters.EntityConfigurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Characters;
using Totten.Solution.Ragstore.Infra.Data.Seeds;

public class CharacterEntityConfiguration : IEntityTypeConfiguration<Character>
{
    const string TABLE_NAME = "Characters";
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.Property(e => e.AccountId).IsRequired();
        builder.Property(e => e.JobId).IsRequired();
        builder.Property(e => e.BaseLevel).IsRequired();
        builder.Property(e => e.Sex).IsRequired();
        builder.Property(e => e.PartyName);
        builder.Property(e => e.GuildId);
        builder.Property(e => e.GuildName);
        builder.Property(e => e.GuildPosition);
        builder.Property(e => e.TitleId);
        builder.Property(e => e.TitleName);
        builder.Property(e => e.HairId);
        builder.Property(e => e.HairColorId);
        builder.Property(e => e.ClothesColorId);
        builder.Property(e => e.WeaponId);
        builder.Property(e => e.ShieldId);
        builder.Property(e => e.HeadTopId);
        builder.Property(e => e.HeadMiddleId);
        builder.Property(e => e.HeadBottomId);
        builder.Property(e => e.RobeId);
        builder.Property(e => e.Map).IsRequired();
        builder.Property(e => e.Location).IsRequired();

        builder.HasMany(e => e.VendingStores)
               .WithOne(v => v.Character)
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.VendingStoreItems)
               .WithOne()
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.BuyingStoreItems)
               .WithOne()
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.BuyingStores)
               .WithOne()
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.Chats)
               .WithOne()
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne()
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.AccountId);
        builder.HasIndex(e => e.Name).IsUnique();

        builder.HasData(MyCharacterSeed.Seed());
    }
}
