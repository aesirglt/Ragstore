namespace Totten.Solution.Ragstore.Infra.Data.Features.Chats;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totten.Solution.Ragstore.Domain.Features.Chats;

public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
{
    const string TABLE_NAME = "Chats";
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable(TABLE_NAME);
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();

        builder.Property(e => e.AccountId);
        builder.Property(e => e.CharacterId);
        builder.Property(e => e.Limit);
        builder.Property(e => e.IsPublic);
        builder.Property(e => e.Map);
        builder.Property(e => e.Location);
        builder.Property(e => e.QuantityUsers);

        builder.HasMany(e => e.EquipmentItems)
               .WithOne(e => e.Chat)
               .HasForeignKey(e => e.CharacterId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(e => e.CharacterId)
               .IsUnique();
    }
}
